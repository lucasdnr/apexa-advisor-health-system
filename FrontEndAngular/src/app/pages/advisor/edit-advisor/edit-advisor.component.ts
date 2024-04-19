import { Component } from '@angular/core';
import { AdvisorFormComponent } from '../../../components/advisor-form/advisor-form.component';
import { Advisor } from '../../../models/advisor.model';

@Component({
  selector: 'app-edit-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './edit-advisor.component.html',
  styleUrl: './edit-advisor.component.scss'
})
export class EditAdvisorComponent {
  pageTitle = "Edit Advisor";

  async editAdvisor(advisor: Advisor) {
  }
}
