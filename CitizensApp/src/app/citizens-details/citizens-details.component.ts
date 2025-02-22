import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

import { CitizensDetailsFormComponent } from './citizens-details-form/citizens-details-form.component';
import { CitizensDetailsService } from '../services/citizens-details.service';
import { CitizenDetails } from '../models/citizen-details.model';

@Component({
  selector: 'app-citizens-details',
  standalone: true,
  imports: [CitizensDetailsComponent, CitizensDetailsFormComponent, CommonModule],
  templateUrl: './citizens-details.component.html',
  styles: ``
})
export class CitizensDetailsComponent implements OnInit{

    constructor(public service : CitizensDetailsService, private toastr: ToastrService) {
    }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: CitizenDetails) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: string) {
    if (confirm('Are you sure to delete this record?'))
      this.service.deleteCitizen(id)
        .subscribe({
          next: res => {
            this.service.list = res as CitizenDetails[]
            this.service.refreshList()
            this.toastr.error('Deleted successfully', 'Citizen Register')
          },
          error: err => { console.log(err) }
        })
  }
}
