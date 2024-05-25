import { Component, OnInit } from '@angular/core';
import { OfferedServicesService } from 'src/app/services/offered-services.service';
import { GetServicesResponse, OfferedService } from 'src/types';
import { ServiceDetailComponent } from '../service-detail/service-detail.component';
import { MatDialog } from '@angular/material/dialog';
import { GridColsDirective } from 'src/app/utils/gridresize-directive';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';
import { env } from 'src/app/env';
@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss'],
  viewProviders: [GridColsDirective]
})

export class ServiceListComponent{
  lowValue: number = 0;
  highValue: number = 20;  
  imageStorage = env.imageStorageUrl;
  fetchedServices: OfferedService[];
  showcasedServices: OfferedService[];

  constructor(
    public router: Router, 
    public dialog: MatDialog, 
    public authService: AuthService, 
    private offeredServicesService: OfferedServicesService
  ) {
    offeredServicesService.getServices().subscribe(
      v => {
        this.fetchedServices = v.services;
        this.showcasedServices = v.services;
      }
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

  public getPaginatorData(event: PageEvent): PageEvent {
    this.lowValue = event.pageIndex * event.pageSize;
    this.highValue = this.lowValue + event.pageSize;
    return event;
  }

  public search(input:string): OfferedService[]{
    console.log(this.fetchedServices.filter(s => s.name.toLowerCase().includes(input)))
    return this.showcasedServices = this.fetchedServices.filter(s => s.name.toLowerCase().includes(input.toLowerCase()))
  }
  
}

