import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { MaterialModule } from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { credentialsInterceptor } from './utils/credentials-interceptor';
import { provideRouter } from '@angular/router';
import { AuthService } from './services/auth.service';
import { UsersService } from './services/users.service';
import { NavbarComponent } from './components/navbar/navbar.component';
import { authGuard } from './guards/auth.guard';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { ServiceDetailComponent } from './components/service-detail/service-detail.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCalendar, MatDatepickerModule } from '@angular/material/datepicker';
import { BookingPickerComponent } from './components/booking-picker/booking-picker.component';
import { MatNativeDateModule } from '@angular/material/core';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { ServiceManagerComponent } from './components/service-manager/service-manager.component';
import { ServiceManagementModalComponent } from './components/service-management-modal/service-management-modal.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { ReservationListComponent } from './components/reservation-list/reservation-list.component';
import { MatSelectModule } from '@angular/material/select';
import { ServiceReservationsComponent } from './components/service-reservations/service-reservations.component';
import { TimeSlotPickerComponent } from './components/time-slot-picker/time-slot-picker.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    UserProfileComponent,
    ServiceListComponent,
    ServiceDetailComponent,
    BookingPickerComponent,
    ConfirmDialogComponent,
    ServiceManagerComponent,
    ServiceManagementModalComponent,
    ReservationListComponent,
    ServiceReservationsComponent,
    TimeSlotPickerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    MatTabsModule,
    MatGridListModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatExpansionModule,
    MatSelectModule,
    NgxMaterialTimepickerModule
  ],
  providers: [
    provideHttpClient(withInterceptors([credentialsInterceptor])),
    provideRouter(routes)
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
