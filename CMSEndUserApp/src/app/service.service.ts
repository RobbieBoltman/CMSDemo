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

  getStockItemDetail(id: any): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.http.get(`${this.apiUrl}GetStockItemById/`+id, {headers: header});
  }

  upsertStockItemDetail(stockItemDetail: any): Observable<any>{
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<boolean>(`${this.apiUrl}UpsertStockItem`, stockItemDetail, { headers });
  }  

  register(user: { email: string, password: string, confirmPassword: string }): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(`${this.apiUrl}auth/register`, user, { headers });
  }
}
