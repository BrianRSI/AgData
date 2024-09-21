import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class CustomerService {

  // API URL (change this to match the API endpoint in my backend)
  private apiUrl = `${environment.apiUrl}/customer`;

  constructor(private http: HttpClient) { }
  
  // POST customer data to the API
  submitCustomerData(customer: any, address: any): Observable<any> {    
    // combine customer and address into a single object that matches the CustomerEntity model
    const customerData = {
        FirstName: customer.firstName,
        LastName: customer.lastName,
        Address1: address.address1,
        Address2: address.address2,
        City: address.city,
        State: address.state,
        Zip: address.zip
      };
      return this.http.post(`${this.apiUrl}/create-customer`, customerData);
  }
  
    // GET all customer data from the API (customers with addresses) - we will redirect to the page with the bootstrap grid
    getCustomers(): Observable < any > {
      return this.http.get(`${this.apiUrl}/get-customers`);
    }
    
  }
