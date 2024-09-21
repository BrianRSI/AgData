import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  title = 'Agdata!';

  inputText: string = ''; // Declare the inputText variable

  onSubmit() {
    alert(`You entered: ${this.inputText}`); // Display an alert with the entered text
  }
}
