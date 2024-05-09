import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(public authService: AuthService, private router: Router){


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
