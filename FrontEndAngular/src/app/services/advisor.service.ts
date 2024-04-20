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
    const dataTransf: Advisor = this.dataTransformSend(advisor);
    return this.http.post<Advisor>(this.apiURL, dataTransf);
  }

  // update advisor
  update(id: string, advisor: Advisor): Observable<Advisor> {
    const dataTransf: Advisor = this.dataTransformSend(advisor);
    return this.http.put<Advisor>(`${this.apiURL}/${id}`, dataTransf);
  }

  // delete advisor
  deleteAdvisor(id: string): Observable<any> {
    return this.http.delete<Advisor>(`${this.apiURL}/${id}`);
  }

  // data transformation
  // this is a workaround to send Phone field as empty to endpoint.
  // endpoint doesn't accept Phone = "" since this field is numeric.
  // other fields can be treated in this method
  dataTransformSend(advisor: Advisor): Advisor {
    let dataTransf: Advisor = { ...advisor };
    if (dataTransf.hasOwnProperty('phone')) {
      if (dataTransf.phone == "") {
        // dataTransf.phone = null;
      }
    }
    return dataTransf;
  }


  //
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
