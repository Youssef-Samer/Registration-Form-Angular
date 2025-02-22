// data/repositories/citizen.repository.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CitizenDetails } from '../../core/models/citizen-details.model';
import { environment } from '../../../environments/environment.development';

@Injectable({ providedIn: 'root' })
export class CitizenRepository {
  url:string = environment.apiBaseUrl + '/CitizenDetail';

  constructor(private http: HttpClient) {}

  getCitizens(): Observable<CitizenDetails[]> {
    return this.http.get<CitizenDetails[]>(this.url);
  }

  postCitizen(citizen: CitizenDetails): Observable<any> {
    return this.http.post(this.url, citizen);
  }

  updateCitizen(citizen: CitizenDetails): Observable<any> {
    return this.http.put(`${this.url}/${citizen.citizenId}`, citizen);
  }

  deleteCitizen(id: string): Observable<any> {
    return this.http.delete(`${this.url}/${id}`);
  }
}