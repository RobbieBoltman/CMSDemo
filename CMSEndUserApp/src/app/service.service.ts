import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  //TODO: Use environment instead
  private apiUrl = 'https://localhost:7240/Stock/'; 

  constructor(private http: HttpClient) { }

  listAllStockItemsDashboard(): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.http.get(`${this.apiUrl}ListAllStockItemsDashboard`, {headers: header});
  }
}
