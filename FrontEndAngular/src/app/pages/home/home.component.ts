import { Component, OnInit } from '@angular/core';
import { AdvisorService } from '../../services/advisor.service';
import { Advisor } from '../../models/advisor.model';
import { lastValueFrom } from 'rxjs';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgFor],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  advisors: Advisor[] = [];
  advisorList: Advisor[] = [];

  constructor(private advisorService: AdvisorService) { }

  async ngOnInit(): Promise<void> {
    // get all advisors from api
    this.getData();
  }

  async getData() {

    try {
      const data = await lastValueFrom(this.advisorService.getAll());
      if (data) {
        console.log("AQUI", data);
        this.advisorList = data;
        this.advisors = data;
      }
    } catch (e) {
      // console.log('Error to get Advisors');
    }
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
