import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-advisor-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './advisor-form.component.html',
  styleUrl: './advisor-form.component.scss'
})
export class AdvisorFormComponent implements OnInit{
  
  advisorForm!: FormGroup;

  constructor(){
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(){
    this.advisorForm = new FormGroup({
      name: new FormControl(''),
      sinNumber: new FormControl(''),
      address: new FormControl(''),
      phone: new FormControl(''),
    });
  }

  submit(){

  }
}
