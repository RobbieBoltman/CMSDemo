import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ItemDetailComponent } from './item-detail/item-detail.component';

export const routes: Routes = [
    {path: '', component: DashboardComponent},
    {path: 'item-detail/:id', component: ItemDetailComponent}
];
