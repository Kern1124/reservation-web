import { DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { OfferedService } from 'src/types';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: 
    {
      message: string, 
    },
    public dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {}

  onDismiss(): void {
    this.dialogRef.close(false)
  }

  onConfirm(): void {
    return this.dialogRef.close(true)
  }
}

