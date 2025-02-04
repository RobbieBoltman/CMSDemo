import { Component, OnInit, signal } from '@angular/core';
import { ServiceService } from '../service.service';
import { ButtonModule } from 'primeng/button';
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [ButtonModule, DataViewModule, TagModule, CommonModule, CardModule, NgbModule, RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  lstStock: any = [];
  loading = false;

  constructor(private service: ServiceService) {}

  ngOnInit(): void {
    this.service.listAllStockItemsDashboard().subscribe({
      next: (res :any )=> {
        this.lstStock = res
        console.log(this.lstStock);
      },
      error: (error:any)=> {
        console.log(error);
      }
    });
  }

  addStockItem(){
    //TODO: Implement new page
  }

  deleteStockItem(id:number) {
    //TODO: Implement Delete
    console.log(id);
  }
}
