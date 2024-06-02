import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { authGuard } from './guards/auth.guard';
import { ServiceDetailComponent } from './components/service-detail/service-detail.component';
import { ServiceManagerComponent } from './components/service-manager/service-manager.component';
import { noAuthGuard } from './guards/no-auth.guard';
import { ReservationListComponent } from './components/reservation-list/reservation-list.component';

export const routes: Routes = [
  { 
    path: '',
    redirectTo: '/home', 
    pathMatch: 'full' },
  {
    path: "home",
    component: HomeComponent,
    canActivate: [],
    children: [
      { 
        path: 'service-detail',
         component: ServiceDetailComponent  
      }
    ],
  },
  {
    path: "login",
    component: LoginComponent,
    canActivate: [noAuthGuard()]
  },
  {
    path: "register",
    component: RegisterComponent,
    canActivate: [noAuthGuard()]
  },
  {
    path: "manage-services",
    component: ServiceManagerComponent,
    canActivate: [authGuard()]
  },
  {
    path: "manage-reservations",
    component: ReservationListComponent,
    canActivate: [authGuard()]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
