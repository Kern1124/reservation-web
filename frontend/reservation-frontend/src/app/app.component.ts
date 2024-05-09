import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  isAuthenticated = this.authService.isAuthenticated()
  title = 'reservation-frontend';
  constructor (private authService: AuthService){
  }
  
}
