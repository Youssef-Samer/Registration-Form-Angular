import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';

import { CitizensDetailsService } from '../../../../core/use-cases/citizens-details.service';
import { CitizenDetails } from '../../../../core/models/citizen-details.model';

@Component({
  selector: 'app-citizens-details-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './citizens-details-form.component.html',
  styles: ``
})
export class CitizensDetailsFormComponent {
    constructor(public service : CitizensDetailsService, private toastr: ToastrService) {
    }
    onSubmit(form: NgForm) {
      this.service.formSubmitted = true;
      if(form.valid)
      {
        if (this.service.formData.citizenId == '')
        {this.service.formData.citizenId = Guid.create().toString();;
          this.insertRecord(form)
        }
        else
          this.updateRecord(form)
      }
    }
  
    insertRecord(form: NgForm) {
      this.service.postCitizen()
        .subscribe({
          next: res => {
            this.service.list = res as CitizenDetails[]
            this.service.resetForm(form)
            this.toastr.success('Inserted successfully', 'Citizen Register')
          },
          error: err => { console.log(err) }
        })
    }
    updateRecord(form: NgForm) {
      this.service.updateCitizen()
        .subscribe({
          next: res => {
            this.service.list = res as CitizenDetails[]
            this.service.resetForm(form)
            this.toastr.info('Updated successfully', 'Citizen Register')
          },
          error: err => { console.log(err) }
        })
     }
}
