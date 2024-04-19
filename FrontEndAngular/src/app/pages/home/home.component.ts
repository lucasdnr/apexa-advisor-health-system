import { Component, OnInit } from '@angular/core';
import { AdvisorService } from '../../services/advisor.service';
import { Advisor } from '../../models/advisor.model';
import { lastValueFrom } from 'rxjs';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  advisors: Advisor[] = [];
  advisorsList: Advisor[] = [];

  constructor(private advisorService: AdvisorService) { }

  async ngOnInit(): Promise<void> {
    // get all advisors from api
    this.getAllData();
  }

  // get All Data
  async getAllData() {

    try {
      const data = await lastValueFrom(this.advisorService.getAll());
      if (data) {
        console.log("AQUI", data);
        this.advisorsList = data;
        this.advisors = data;
      }
    } catch (e) {
      // console.error('Error to get Advisors');
    }
  }

  // search bar
  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();
    
    // filter data from the orignal data fetch advisorsList
    this.advisors = this.advisorsList.filter(advisor =>
      advisor.name.toLowerCase().includes(value)
    )
  }

  formartSIN(sinNumber: number): string {
    // Convert the SIN to a string
    let sinString = sinNumber.toString();

    // Check if the SIN length is valid
    if (sinString.length !== 9) {
      return "Invalid SIN length";
    }

    // Format the SIN with hyphens
    const formattedSIN = sinString.substring(0, 3) + '-' + sinString.substring(3, 6) + '-' + sinString.substring(6);

    return formattedSIN;
  }

  maskSIN(sinNumber: number): string {
    // Convert the SIN to a string
    let sinString = sinNumber.toString();

    // Check if the SIN length is valid
    if (sinString.length !== 9) {
      return "Invalid SIN length";
    }

    // Mask all but the last 4 digits
    let maskedSIN = '***-***-' + sinString.substring(sinString.length - 4);

    return maskedSIN;
  }

}
