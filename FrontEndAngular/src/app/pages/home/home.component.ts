import { Component, OnInit } from '@angular/core';
import { AdvisorService } from '../../services/advisor.service';
import { Advisor } from '../../models/advisor.model';
import { lastValueFrom } from 'rxjs';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DialogService } from '../../components/dialog/dialog.service';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    RouterLink,
    MatTooltipModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatIconModule,
    MatTableModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  advisors: Advisor[] = [];
  advisorsList: Advisor[] = [];

  displayedColumns = ['Health', 'Name', 'SIN', 'Address', 'Phone', 'Actions']

  constructor(
    private advisorService: AdvisorService,
    private dialog: DialogService,
    private toast: ToastService
  ) { }

  async ngOnInit(): Promise<void> {
    // get all advisors from api
    this.getAllData();
  }

  // get All Data
  async getAllData() {

    try {
      const data = await lastValueFrom(this.advisorService.getAll());
      data.map(e=>{
        // transformations
        e.sinNumber = this.advisorService.formartSIN(Number(e.sinNumber));
        e.phone = this.advisorService.formartPhone(Number(e.phone));
      })

      // move data
      this.advisorsList = data;
      this.advisors = data;

    } catch (e: any) {
      this.toast.errorGeneric();
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

  async deleteItem(id: string) {
    // get data from this.advisors to retrieve Name
    const itemData = this.advisors.find(e => e.id === id);
    if (itemData) {
      const confirmed = await lastValueFrom(this.dialog
        .showConfirmationdialog(
          "Warning!",
          `Are you sure you want to permanently delete ${itemData.name}?`,
          "OK",
          "Cancel"
        ));
      if (confirmed) {
        try {
          // delete item
          const response = await lastValueFrom(this.advisorService.deleteAdvisor(id));
          // load New Data
          this.getAllData();
        } catch (e: any) {
          this.toast.errorGeneric();
        }
      }
    }
  }
}
