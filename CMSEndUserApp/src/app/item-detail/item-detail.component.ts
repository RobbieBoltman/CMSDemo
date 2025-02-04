import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators} from '@angular/forms';
import { ReactiveFormsModule} from '@angular/forms';
import { ServiceService } from '../service.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-detail',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './item-detail.component.html',
  styleUrl: './item-detail.component.scss'
})
export class ItemDetailComponent {
  stockItemForm: FormGroup;
  itemDetails: any;
  id: number;

  constructor(private fb: FormBuilder, private service: ServiceService, private route: ActivatedRoute) {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.service.getStockItemDetail(this.id).subscribe({
      next: (res :any )=> {
        this.itemDetails = res
        console.log(this.itemDetails);
        this.populateForm();
      },
      error: (error:any)=> {
        console.log(error);
      }
    });

    this.stockItemForm = this.fb.group({
      id: [''],
      dtcreated: ['', Validators.required],
      regNo: ['', Validators.required],
      make: ['', Validators.required],
      model: ['', Validators.required],
      modelYear: ['', Validators.required],
      kms: ['', Validators.required],
      colour: ['', Validators.required],
      vin: ['', Validators.required],
      retailPrice: ['', Validators.required],
      costPrice: ['', Validators.required],
      images: this.fb.array([]),
      stockAccessories: this.fb.array([])
  })
  }

  get images(): FormArray {
    return this.stockItemForm.get('images') as FormArray;
  }

  get stockAccessories(): FormArray {
    return this.stockItemForm.get('stockAccessories') as FormArray;
  }

  addImage(id: number, name: any, binary: any): void {
    this.images.push(this.fb.group({
      id: id,
      name: name,
      imageBinary: 'data:{content/type};base64,' + binary,
      stockItemId: this.id
    }));
  }

  addStockAccessory(id: number, description: any): void {
    this.stockAccessories.push(this.fb.group({
      id: id,
      description: description,
      stockItemId: this.id
    }));
  }

  addStockAccessoryInput(): void {
    this.addStockAccessory(0, '');
  }

    
  uploadImage(e: Event): void {
    const fileInput = e.target as HTMLInputElement;
    const file = fileInput.files?.[0];
  
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
  
      reader.onload = () => {
        const base64 = reader.result as string; 
        const base64Data = base64.split(',')[1];
        this.addImage(0, file.name, base64Data);
      };
  

      reader.onerror = (error) => {
        console.error('Error reading file:', error);
      };
    }
  }

  removeImage(index: number): void {
    this.images.removeAt(index);
  }

  removeStockAccessory(index: number): void {
    this.stockAccessories.removeAt(index);
  }

  populateForm(): void {
    const stockItem = {      
      id: this.id,
      dtcreated: this.itemDetails.dtcreated,
      regNo: this.itemDetails.regNo,
      make: this.itemDetails.make,
      model: this.itemDetails.model,
      modelYear: this.itemDetails.modelYear,
      kms: this.itemDetails.kms,
      colour: this.itemDetails.colour,
      vin: this.itemDetails.vin,
      retailPrice: this.itemDetails.retailPrice,
      costPrice: this.itemDetails.costPrice,
      images: [],
      stockAccessories: []
    };

    this.stockItemForm.patchValue(stockItem);
    
    this.itemDetails.images.forEach((i:any) => this.addImage(i.id, i.name, i.imageBinary));
    this.itemDetails.stockAccessories.forEach((a:any) => this.addStockAccessory(a.id, a.description));

    this.images.patchValue(stockItem.images);
    this.stockAccessories.patchValue(stockItem.stockAccessories);
  }

  onSubmit(): void {
    if (this.stockItemForm.valid) {
      const formData = JSON.parse(JSON.stringify(this.stockItemForm.value));
  
      formData.images = formData.images.map((img: any) => ({
        ...img,
        imageBinary: img.imageBinary.split(',')[1] 
      }));
  
      this.service.upsertStockItemDetail(formData).subscribe({
        next: (response) => {
          console.log('Stock item saved successfully', response);
        },
        error: (error) => {
          console.error('Error saving stock item', error);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}