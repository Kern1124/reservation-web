import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { UserDto } from 'src/types';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  currentUser: UserDto;
  constructor(public authService: AuthService, public router: Router){
  }
  
  onLogout = () => {
    this.authService.logout();
    this.router.navigate(["home"])
  }

  onRegister = () => {
    this.router.navigate(["register"])
  }

  onLogin = () => {
    this.router.navigate(["login"])
  }
}
