import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';
import { GetMyNotificationsResponse, NotificationDto, UserDto } from 'src/types';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  currentUser: UserDto;
  notifications: NotificationDto[] | undefined
  unreadNotifications: NotificationDto[]
  constructor(
    public notificationsService: NotificationsService, 
    public authService: AuthService, 
    public router: Router){
    }

  ngOnInit(): void {
    this.fetchNotifications()
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

  setAsRead = (notification: NotificationDto) => {
    this.notificationsService.setNotificationAsRead(notification.id).subscribe(() => {
      this.fetchNotifications()
    })
  }

  setAllAsRead = () => {
    let index: number = 0
    this.notifications!.forEach((n) => {
      this.notificationsService.setNotificationAsRead(n.id).subscribe(v => {
        if (index == (this.notifications!.length - 1)){
          this.fetchNotifications();
        }
        index++;
      })
    })
  }

  fetchNotifications = () => {
    this.notificationsService.getMyNotifications().subscribe(
      v => this.notifications = v.notifications
    )
  }
  
}
