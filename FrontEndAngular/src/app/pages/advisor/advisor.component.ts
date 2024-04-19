import { Component } from '@angular/core';
import { AdvisorFormComponent } from '../../components/advisor-form/advisor-form.component';

@Component({
  selector: 'app-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './advisor.component.html',
  styleUrl: './advisor.component.scss'
})
export class AdvisorComponent {

}
