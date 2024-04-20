import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Advisor } from '../models/advisor.model';

@Injectable({
  providedIn: 'root'
})
export class AdvisorService {
  private apiURL = `${environment.apiURL}/advisors`;

  constructor(private http: HttpClient) {
  }

  // fetch all data
  getAll(): Observable<Advisor[]> {
    return this.http.get<Advisor[]>(this.apiURL);
  }

  // get one by id
  getOneById(id: string): Observable<Advisor> {
    return this.http.get<Advisor>(`${this.apiURL}/${id}`)
  }

  // create advisor
  create(advisor: Advisor): Observable<Advisor> {
    return this.http.post<Advisor>(this.apiURL, advisor);
  }

  // update advisor
  update(id: string, advisor: Advisor): Observable<Advisor> {
    return this.http.put<Advisor>(`${this.apiURL}/${id}`, advisor);
  }

  // delete advisor
  deleteAdvisor(id: string): Observable<any> {
    return this.http.delete<Advisor>(`${this.apiURL}/${id}`);
  }

  formartSIN(sinNumber: number): string {
    // Convert the SIN to a string
    let sinString = sinNumber.toString();

    // Check if the SIN length is valid
    if (sinString.length !== 9) {
      return "";
    }

    // Format the SIN with hyphens
    const formattedSIN = sinString.substring(0, 3) + '-' + sinString.substring(3, 6) + '-' + sinString.substring(6);

    return formattedSIN;
  }

  formartPhone(sinNumber: number): string {
    // Convert the SIN to a string
    let sinString = sinNumber.toString();

    // Check if the SIN length is valid
    if (sinString.length !== 8) {
      return "";
    }

    // Format the SIN with hyphens
    const formattedSIN = sinString.substring(0, 4) + '-' + sinString.substring(4, 8);

    return formattedSIN;
  }
}
