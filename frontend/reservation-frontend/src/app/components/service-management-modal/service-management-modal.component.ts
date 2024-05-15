import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { WhenDirtyStateMatcher } from 'src/app/utils/error-state-matcher';
import { OfferedService } from 'src/types';

@Component({
  selector: 'app-service-management-modal',
  templateUrl: './service-management-modal.component.html',
  styleUrls: ['./service-management-modal.component.scss']
})
export class ServiceManagementModalComponent {
  manageServiceForm: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.minLength(5)]),
    desc: new FormControl('', [Validators.minLength(24)]),
  });
  errorStateMatcher = new WhenDirtyStateMatcher()
  constructor(
    public dialogRef: MatDialogRef<ServiceManagementModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OfferedService | null) {}

    closeDialog(): void {
      this.dialogRef.close();
    }
    submit(): void {
      console.log(this.manageServiceForm.valid)
      if (this.manageServiceForm.valid){
        this.dialogRef.close(
          {
            name: this.manageServiceForm.controls['name'].value,
            desc: this.manageServiceForm.controls['desc'].value
          });
      }
    }
}
