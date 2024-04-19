import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Advisor } from '../models/advisor.model';

@Injectable({
  providedIn: 'root'
})
export class AdvisorService {
  headers: any = false;

  private apiURL = environment.apiURL;

  constructor(private http: HttpClient) { 
  }

  getAll(): Observable<Advisor[]> {
    return this.http.get<Advisor[]>(`${this.apiURL}advisors`);
  }
}
