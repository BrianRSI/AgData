import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from './customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})

export class CustomersComponent implements OnInit {

  // Declare the customerData property to hold the API response
  customerData: { customers: any[], addresses: any[] } = { customers: [], addresses: [] };

  constructor(private customerService: CustomerService, private router: Router) { }

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe(
      data => {
        console.log('Fetched Data:', data);
        this.customerData = data;  // Assign the API response to customerData
        console.log('Customers: ', this.customerData.customers);
      },
      error => {
        console.error('Error fetching customer data:', error);
      }
    );
  }

  // Method to navigate to the Add New Customer form
  addNewCustomer(): void {
    this.router.navigate(['/customer']);  // Navigate to the customer form page
  }

  // Helper function to get addresses for a specific customer
  getCustomerAddresses(CustomerId: number): any[] {
    console.log('getCustomerAddresses CustId:', CustomerId);    
    return this.customerData.addresses.filter(address => address.customerId === CustomerId);
  }
}
