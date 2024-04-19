import { Component } from '@angular/core';
import { AdvisorFormComponent } from '../../components/advisor-form/advisor-form.component';
import { Advisor } from '../../models/advisor.model';
import { AdvisorService } from '../../services/advisor.service';
import { lastValueFrom } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './advisor.component.html',
  styleUrl: './advisor.component.scss'
})
export class AdvisorComponent {
  pageTitle = "New Advisor";

  constructor(private advisorService: AdvisorService, private router: Router) { }

  async createAdvisor(advisor: Advisor) {
    try {
      const response = await lastValueFrom(this.advisorService.createAdvisor(advisor));
      
      this.router.navigate(['/']);
      
    } catch (e: any) {
      console.error("Error to create advisor", e.message);
    }
  }
}
