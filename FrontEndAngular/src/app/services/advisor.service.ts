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
  getOneById(id: string): Observable<Advisor>{
    return this.http.get<Advisor>(`${this.apiURL}/${id}`)
  }

  // create advisor
  createAdvisor(advisor: Advisor): Observable<Advisor> {
    return this.http.post<Advisor>(this.apiURL, advisor);
  }

  // update advisor
  updateAdvisor(id:string, advisor: Advisor): Observable<Advisor> {
    return this.http.put<Advisor>(`${this.apiURL}/${id}`, advisor);
  }
}
