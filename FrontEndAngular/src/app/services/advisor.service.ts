import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Advisor } from '../models/advisor.model';

@Injectable({
  providedIn: 'root'
})
export class AdvisorService {
  private apiURL = environment.apiURL;

  constructor(private http: HttpClient) { 
  }

  // fetch all data
  getAll(): Observable<Advisor[]> {
    return this.http.get<Advisor[]>(`${this.apiURL}advisors`);
  }
}
