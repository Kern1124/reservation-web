import { Component, OnInit } from '@angular/core';
import {
  CountryDto,
  LocationDto,
  OfferedService,
  ProblemDetails,
  TimeSlotDto,
} from 'src/types';
import { ServiceManagementModalComponent } from '../service-management-modal/service-management-modal.component';
import { OfferedServicesService } from 'src/app/services/offered-services.service';
import { AuthService } from 'src/app/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WhenDirtyStateMatcher } from 'src/app/utils/error-state-matcher';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { HttpErrorResponse } from '@angular/common/http';
import { LocationService } from 'src/app/services/location.service';
import { ServiceReservationsComponent } from '../service-reservations/service-reservations.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TimeSlotPickerComponent } from '../time-slot-picker/time-slot-picker.component';

@Component({
  selector: 'app-service-manager',
  templateUrl: './service-manager.component.html',
  styleUrls: ['./service-manager.component.scss'],
})
export class ServiceManagerComponent implements OnInit {
  services: OfferedService[];
  countries: CountryDto[];
  selectedFile: any = null;
  selectedCountry: CountryDto = { name: '', cities: [] };
  selectedTimeSlots: TimeSlotDto[] = [];
  errorStateMatcher = new WhenDirtyStateMatcher();
  selectedService: OfferedService | null = null;

  createServiceForm: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.minLength(5), Validators.required]),
    desc: new FormControl('', [Validators.minLength(24), Validators.required]),
    country: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
  });

  creationFailed: boolean = false;
  creationFailedMessage: string;
  editFailed: boolean = false;
  editFailedMessage: string;

  constructor(
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private OSService: OfferedServicesService,
    private locationService: LocationService
  ) {}

  ngOnInit(): void {
    this.OSService.getOwnedServices().subscribe((v) => {
      this.services = v.services;
      this.locationService.getAllCountries().subscribe((v) => {
        this.countries = v.countries;
      });
    });
  }

  pickTimeSlots(): void {
    this.dialog.open(TimeSlotPickerComponent, {
      width: '50%',
      height: '50%',
      data: this.selectedTimeSlots //TimeSlotPickerComponent directly mutates this array
    });
  }

  openService(service: OfferedService): void {
    this.creationFailed = false;
    this.editFailed = false;
    this.selectedService = service;
    console.log(service);
    let dialogRef = this.dialog.open(ServiceManagementModalComponent, {
      width: '50%',
      height: '50%',
      data: this.selectedService,
    });

    dialogRef
      .afterClosed()
      .subscribe((newValues: { name: string; desc: string }) => {
        if (newValues.name || newValues.desc) {
          let newName = newValues.name == '' ? null : newValues.name;
          let newDesc = newValues.desc == '' ? null : newValues.desc;

          this.OSService.updateService(
            this.selectedService?.id!,
            newName,
            newDesc
          ).subscribe({
            next: () => {
              this.OSService.getOwnedServices().subscribe(
                (v) => (this.services = v.services)
              );
              this.editFailed = false;
            },
            error: (e: HttpErrorResponse) => {
              let err: ProblemDetails = e.error;
              this.editFailed = true;
              this.editFailedMessage = err.errors[0].reason;
            },
          });
        }
      });
  }

  openReservations(service: OfferedService): void {
    this.creationFailed = false;
    this.editFailed = false;
    let dialogRef = this.dialog.open(ServiceReservationsComponent, {
      width: '75%',
      height: '75%',
      data: service,
    });
  }

  removeService(service: OfferedService): void {
    this.creationFailed = false;
    this.editFailed = false;
    let dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '35%',
      height: '35%',
      data: {
        ser: 'Are you sure you want to remove this service?',
      },
    });
    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.OSService.removeService(service.id).subscribe({
          next: () =>
            this.OSService.getOwnedServices().subscribe(
              (v) => (this.services = v.services)
            ),
        });
      }
    });
  }
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0] ?? null;
  }
  createService(): void {
    this.creationFailed = false;
    this.editFailed = false;
    if (this.createServiceForm.valid && this.selectedFile) {
      let dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '35%',
        height: '35%',
        data: {
          message: 'Are you sure you want to create this service?',
        },
      });
      dialogRef.afterClosed().subscribe((confirmed) => {
        if (confirmed) {
          let location: LocationDto = {
            country: this.createServiceForm.controls['country'].value.name, // <- this is of type CountryDto!
            city: this.createServiceForm.controls['city'].value,
            address: this.createServiceForm.controls['address'].value,
          };

          this.OSService.createService(
            this.createServiceForm.controls['name'].value,
            this.createServiceForm.controls['desc'].value,
            location,
            this.selectedTimeSlots
          ).subscribe({
            next: (v) => {
              this.creationFailed = false;

              this.snackBar.open('Service successfully created', 'Dismiss', {
                duration: 3000,
              });

              this.OSService.updateServiceImage(
                v.service.id,
                this.selectedFile
              ).subscribe(() =>
                this.OSService.getOwnedServices().subscribe(
                  (v) => (this.services = v.services)
                )
              );
            },
            error: (e: HttpErrorResponse) => {
              let error: ProblemDetails = e.error;
              this.creationFailed = true;
              this.creationFailedMessage = error.errors[0].reason;

              this.snackBar.open(this.creationFailedMessage, 'Dismiss', {
                duration: 3000,
              });
            },
          });
        }
      });
    }
  }
}
