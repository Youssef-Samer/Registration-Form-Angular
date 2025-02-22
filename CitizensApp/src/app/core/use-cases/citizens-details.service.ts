import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';

import { CitizenDetails } from '../models/citizen-details.model';
import { environment } from '../../../environments/environment.development';
import { CitizenRepository } from '../../data/repositories/citizen-repository';

@Injectable({
  providedIn: 'root'
})
export class CitizensDetailsService {

  url:string = environment.apiBaseUrl + '/CitizenDetail'
  list: CitizenDetails[] = [];
  formData: CitizenDetails = new CitizenDetails();
  formSubmitted: boolean = false;

  constructor(private http: HttpClient, private citizenRepository : CitizenRepository) { }

  refreshList() {
    this.citizenRepository.getCitizens().subscribe({
      next: (res) => (this.list = res),
      error: (err) => console.error(err)
    });
  }

  postCitizen(): Observable<any> {
    return this.citizenRepository.postCitizen(this.formData);
  }

  updateCitizen(): Observable<any> {
    return this.citizenRepository.updateCitizen(this.formData);
  }

  deleteCitizen(id: string): Observable<any> {
    return this.citizenRepository.deleteCitizen(id);
  }


  resetForm(form: NgForm) {
    form.form.reset()
    this.formData = new CitizenDetails()
    this.formSubmitted = false
    this.refreshList();
  }
}
