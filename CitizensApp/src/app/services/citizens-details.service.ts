import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { environment } from '../../environments/environment.development';
import { CitizenDetails } from '../models/citizen-details.model';

@Injectable({
  providedIn: 'root'
})
export class CitizensDetailsService {

  url:string = environment.apiBaseUrl + '/CitizenDetail'
  list: CitizenDetails[] = [];
  formData: CitizenDetails = new CitizenDetails();
  formSubmitted: boolean = false;

  constructor(private http: HttpClient) { }

  refreshList () {
    this.http.get(this.url).subscribe({
      next: res => {this.list = res as CitizenDetails[]},
      error: err => {console.log(err)}
    })
  }

  
  postCitizen() {
    return this.http.post(this.url, this.formData)
  }

  updateCitizen() {
    return this.http.put(this.url + '/' + this.formData.citizenId, this.formData)
  }


  deleteCitizen(id: string) {
    return this.http.delete(this.url + '/' + id)
  }


  resetForm(form: NgForm) {
    form.form.reset()
    this.formData = new CitizenDetails()
    this.formSubmitted = false
    this.refreshList();
  }
}
