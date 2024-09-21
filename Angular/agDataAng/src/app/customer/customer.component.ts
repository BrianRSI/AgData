import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from '../customer/customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})


export class CustomerComponent {
  customer = {
    firstName: '',
    lastName: ''
  };

  address = {
    address1: '',
    address2: '',
    city: '',
    state: '',
    zip: ''
  };

  constructor(private customerService: CustomerService, private router: Router) { }

  onSubmit() {
    //cCheck if customer last name data is valid before submitting - overkill we already checked on the front-end
    if (this.customer.lastName) {
      // call the service to submit the data
      this.customerService.submitCustomerData(this.customer, this.address).subscribe(
        (response) => {
          // Handle success
          console.log('Customer data submitted successfully:', response);
          alert('Customer data submitted successfully!');
          // redirect to the 'customers' page after successful submission - shows json data
          this.router.navigate(['/customers']);
        },
        (error) => {
          // handle error
          console.error('Error submitting customer data:', error);
          alert('Error submitting customer data.');
        }
      );
    } else {
      // oops - form validation failed 
      alert('Please fill out all required fields.');
    }
  }

  validateZip(event: KeyboardEvent): void {
    const inputChar = String.fromCharCode(event.charCode);
    const zip = this.address.zip ? this.address.zip.toString() : '';

    // Only allow numbers (0-9) and prevent input if the length is greater than 5
    if (!/^[0-9]$/.test(inputChar) || zip.length >= 5) {
      event.preventDefault();
    }
  }

}
