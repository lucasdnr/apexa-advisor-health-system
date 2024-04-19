import { Component } from '@angular/core';
import { AdvisorFormComponent } from '../../../components/advisor-form/advisor-form.component';
import { AdvisorService } from '../../../services/advisor.service';
import { Advisor } from '../../../models/advisor.model';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-new-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './new-advisor.component.html',
  styleUrl: './new-advisor.component.scss'
})
export class NewAdvisorComponent {
  pageTitle: string = "New Advisor";

  constructor(private advisorService: AdvisorService, private router: Router) { }

  async createAdvisor(advisor: Advisor) {
    try {
      await lastValueFrom(this.advisorService.createAdvisor(advisor));

      this.router.navigate(['/']);

    } catch (e: any) {
      console.error("Error to create advisor", e.message);
    }
  }
}
