import { HttpErrorResponse } from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';
import { LoginResponse, ProblemDetails } from 'src/types';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  constructor(private authService: AuthService, private router: Router, private notificationsService: NotificationsService){
  }
  submitted: boolean = false;
  failed: boolean = false;
  failedMessage: string;
  loginForm: FormGroup = new FormGroup({
    mailAddress: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
  }

  submit() {
    this.submitted = true;
    if (this.loginForm.valid) {
      this.authService.login({
        mailAddress: this.loginForm.controls['mailAddress'].value,
        password: this.loginForm.controls['password'].value
      }).subscribe({
        next: () => {
          this.router.navigate(["../home"])
          this.authService.refreshAuthenticated()
          window.location.reload()
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.log(errorResponse)
          let error: ProblemDetails = errorResponse.error;
          this.failed = true;
          if (error.errors[0] != null){
            this.failedMessage = error.errors[0].reason;
          } else {
            this.failedMessage = "Login failed due to unknown reasons, please contact an administrator or try again later."
          }
        }
      })
      this.submitted = false;
    }
  }
  get mailAddress(){
    return this.loginForm.get('mailAddress')!;
  }
  get password(){
    return this.loginForm.get('password')!;
  }
}
