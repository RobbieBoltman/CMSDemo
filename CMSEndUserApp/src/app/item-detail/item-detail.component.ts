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
  id: any;

  constructor(private fb: FormBuilder, private service: ServiceService, private route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get('id');
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

  addImage(id: any, name: any, binary: any): void {
    this.images.push(this.fb.group({
      id: id,
      name: name,
      imageBinary: binary,
      stockItemId: this.id
    }));
  }

  addStockAccessory(): void {
    this.stockAccessories.push(this.fb.group({
      id: [''],
      description: ['', Validators.required],
      stockItemId: ['']
    }));
  }

  removeImage(index: number): void {
    this.images.removeAt(index);
  }

  removeStockAccessory(index: number): void {
    this.stockAccessories.removeAt(index);
  }

  populateForm(): void {
    const stockItem = {
      id: this.itemDetails.id,
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

    this.itemDetails.images.forEach((i:any) => this.addImage(i.id, i.name, i.binary));
    stockItem.stockAccessories.forEach(accessory => this.addStockAccessory());

    this.images.patchValue(stockItem.images);
    this.stockAccessories.patchValue(stockItem.stockAccessories);
  }

  onSubmit(): void {
    if (this.stockItemForm.valid) {
      console.log(this.stockItemForm.value);
    }
  }
}