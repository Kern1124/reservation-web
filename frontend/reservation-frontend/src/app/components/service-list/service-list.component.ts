import { Component, OnInit } from '@angular/core';
import { OfferedServicesService } from 'src/app/services/offered-services.service';
import { GetServicesResponse, OfferedService } from 'src/types';
import { ServiceDetailComponent } from '../service-detail/service-detail.component';
import { MatDialog } from '@angular/material/dialog';
import { GridColsDirective } from 'src/app/utils/gridresize-directive';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss'],
  viewProviders: [GridColsDirective]
})

export class ServiceListComponent{
  services: OfferedService[];
  constructor(public dialog: MatDialog, public authService: AuthService, private offeredServicesService: OfferedServicesService) {
    offeredServicesService.getServices().subscribe(
      v => {this.services = v.services;}
    )
  }

  openDialog(serviceDetails: OfferedService): void {
    const dialogRef = this.dialog.open(ServiceDetailComponent, {
      width: '80%',
      height: '80%',
      data: serviceDetails
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}

