import { Component, EventEmitter, Input, OnInit, Output, output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Advisor } from '../../models/advisor.model';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-advisor-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterLink, MatButtonModule, MatCardModule, MatInputModule],
  templateUrl: './advisor-form.component.html',
  styleUrl: './advisor-form.component.scss'
})
export class AdvisorFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<Advisor>
  @Input() pageTitle!: string;
  @Input() dataAdvisor: Advisor | null = null;

  advisorForm!: FormGroup;

  constructor() {
  }

  ngOnInit(): void {
    // init data form
    this.initForm();
  }

  initForm() {
    this.advisorForm = new FormGroup({
      name: new FormControl(this.dataAdvisor ? this.dataAdvisor.name : '', [Validators.required, Validators.maxLength(255)]),
      sinNumber: new FormControl(this.dataAdvisor ? this.dataAdvisor.sinNumber : '', [Validators.required]),
      address: new FormControl(this.dataAdvisor ? this.dataAdvisor.address : '', Validators.maxLength(255)),
      phone: new FormControl(this.dataAdvisor ? this.dataAdvisor.phone : ''),
    });
  }

  submit() {
    this.onSubmit.emit(this.advisorForm.value);
  }
}
