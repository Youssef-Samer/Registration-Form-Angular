import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

import { CitizensDetailsFormComponent } from '../citizens-details-form/citizens-details-form.component';
import { CitizenDetails } from '../../../../core/models/citizen-details.model';
import { CitizensDetailsService } from '../../../../core/use-cases/citizens-details.service';

@Component({
  selector: 'app-citizens-details',
  standalone: true,
  imports: [CitizensDetailsComponent, CitizensDetailsFormComponent, CommonModule],
  templateUrl: './citizens-details.component.html',
  styles: ``
})
export class CitizensDetailsComponent implements OnInit{

    constructor(public citizenService : CitizensDetailsService, private toastr: ToastrService) {
    }

  ngOnInit(): void {
    this.citizenService.refreshList();
  }

  populateForm(selectedRecord: CitizenDetails) {
    this.citizenService.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: string) {
    if (confirm('Are you sure to delete this record?'))
      this.citizenService.deleteCitizen(id)
        .subscribe({
          next: res => {
            this.citizenService.list = res as CitizenDetails[]
            this.citizenService.refreshList()
            this.toastr.error('Deleted successfully', 'Citizen Register')
          },
          error: err => { console.log(err) }
        })
  }
}
