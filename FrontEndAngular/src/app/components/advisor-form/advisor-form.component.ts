import { Component, EventEmitter, Input, OnInit, Output, output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Advisor } from '../../models/advisor.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-advisor-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterLink],
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
      name: new FormControl('', [Validators.required]),
      sinNumber: new FormControl('', [Validators.required]),
      address: new FormControl(''),
      phone: new FormControl(''),
    });
  }

  submit() {
    this.onSubmit.emit(this.advisorForm.value);
  }
}
