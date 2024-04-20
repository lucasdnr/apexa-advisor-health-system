import { Component } from '@angular/core';
import { AdvisorFormComponent } from '../../../components/advisor-form/advisor-form.component';
import { AdvisorService } from '../../../services/advisor.service';
import { Advisor } from '../../../models/advisor.model';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-new-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './new-advisor.component.html',
  styleUrl: './new-advisor.component.scss'
})
export class NewAdvisorComponent {
  pageTitle: string = "New Advisor";

  constructor(
    private advisorService: AdvisorService, 
    private router: Router,
    private toast: ToastService) { }

  async createAdvisor(advisor: Advisor) {
    try {
      // create new advisor and redirect to home page
      await lastValueFrom(this.advisorService.create(advisor));

      this.router.navigate(['/']);

    } catch (e: any) {
      if(e.status === 409){
        this.toast.error(e.error, '');
      }else{
        this.toast.errorGeneric();
      }
    }
  }
}
