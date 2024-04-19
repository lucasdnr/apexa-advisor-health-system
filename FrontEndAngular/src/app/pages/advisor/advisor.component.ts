import { Component } from '@angular/core';
import { AdvisorFormComponent } from '../../components/advisor-form/advisor-form.component';
import { Advisor } from '../../models/advisor.model';
import { AdvisorService } from '../../services/advisor.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './advisor.component.html',
  styleUrl: './advisor.component.scss'
})
export class AdvisorComponent {
  constructor(private advisorService: AdvisorService) { }

  async createAdvisor(advisor: Advisor) {
    try {
      const response = await lastValueFrom(this.advisorService.createAdvisor(advisor));
      if(response){
        console.log("A", response);
      }
    } catch (e: any) {
      console.error("Error to create advisor", e.message);
    }
  }
}
