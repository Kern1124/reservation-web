import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { OfferedServicesService } from 'src/app/services/offered-services.service';
import { OfferedService } from 'src/types';

@Component({
  selector: 'app-service-detail',
  templateUrl: './service-detail.component.html',
  styleUrls: ['./service-detail.component.scss']
})
export class ServiceDetailComponent {
  selectedTabIndex: number = 0;
  constructor(
    public dialogRef: MatDialogRef<ServiceDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OfferedService) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
  onTabChange(eventIndex: number): void {
    this.selectedTabIndex = eventIndex;
  }
}
