import { Component, OnInit } from '@angular/core';
import { AdvisorFormComponent } from '../../../components/advisor-form/advisor-form.component';
import { Advisor } from '../../../models/advisor.model';
import { AdvisorService } from '../../../services/advisor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-edit-advisor',
  standalone: true,
  imports: [AdvisorFormComponent],
  templateUrl: './edit-advisor.component.html',
  styleUrl: './edit-advisor.component.scss'
})
export class EditAdvisorComponent implements OnInit {
  pageTitle: string = "Edit Advisor";
  dataAdvisor!: Advisor;

  constructor(private advisorService: AdvisorService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // fetch advisor data
    this.loadData();
  }

  async loadData() {
    try {
      const id = String(this.route.snapshot.paramMap.get('id'));
      const data = await lastValueFrom(this.advisorService.getOneById(id));

      this.dataAdvisor = data;

    } catch (e: any) {
      console.error("Error to get advisor data", e.message);
    }
  }

  async editAdvisor(advisor: Advisor) {
  }
}
