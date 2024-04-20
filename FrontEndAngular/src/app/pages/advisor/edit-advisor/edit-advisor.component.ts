import { Component, OnInit } from '@angular/core';
import { AdvisorFormComponent } from '../../../components/advisor-form/advisor-form.component';
import { Advisor } from '../../../models/advisor.model';
import { AdvisorService } from '../../../services/advisor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { NgIf } from '@angular/common';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-edit-advisor',
  standalone: true,
  imports: [AdvisorFormComponent, NgIf],
  templateUrl: './edit-advisor.component.html',
  styleUrl: './edit-advisor.component.scss'
})
export class EditAdvisorComponent implements OnInit {
  pageTitle: string = "Edit Advisor";
  dataAdvisor!: Advisor;
  id!: string;

  constructor(
    private advisorService: AdvisorService,
    private router: Router,
    private route: ActivatedRoute,
    private toast: ToastService
  ) { }

  ngOnInit(): void {
    // fetch advisor data
    this.loadData();
  }

  async loadData() {
    try {
      this.id = String(this.route.snapshot.paramMap.get('id'));
      const data = await lastValueFrom(this.advisorService.getOneById(this.id));

      this.dataAdvisor = data;

    } catch (e: any) {
      this.toast.errorGeneric();
    }
  }

  async editAdvisor(advisor: Advisor) {
    try {
      await lastValueFrom(this.advisorService.update(this.id, advisor));

      // success message
      this.toast.success('Advisor Updated');
      
      // redirect to home
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
