import { Component, EventEmitter, OnInit, Output, output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Advisor } from '../../models/advisor.model';

@Component({
  selector: 'app-advisor-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './advisor-form.component.html',
  styleUrl: './advisor-form.component.scss'
})
export class AdvisorFormComponent implements OnInit{
  @Output() onSubmit = new EventEmitter<Advisor>

  advisorForm!: FormGroup;

  constructor(){
  }

  ngOnInit(): void {
    // init data form
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
    this.onSubmit.emit(this.advisorForm.value);
  }
}
