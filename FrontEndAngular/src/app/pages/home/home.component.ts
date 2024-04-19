import { Component, OnInit } from '@angular/core';
import { AdvisorService } from '../../services/advisor.service';
import { Advisor } from '../../models/advisor.model';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit{

  advisors: Advisor[] = [];
  advisorList: Advisor[] = [];

  constructor(private advisorService: AdvisorService) {}

  async ngOnInit(): Promise<void> {
    // get all advisors from api
    const data = await lastValueFrom(this.advisorService.getAll());
    console.log("AQUI", data);
  }
}
