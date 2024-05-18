import { HttpErrorResponse } from '@angular/common/http';
import {Component} from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { WhenDirtyStateMatcher } from 'src/app/utils/error-state-matcher';
import { ProblemDetails } from 'src/types';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  failed: boolean;
  failedMessage: string;
  submitted: boolean = false;
  registerMessage: string;
  errorStateMatcher: WhenDirtyStateMatcher = new WhenDirtyStateMatcher();
  constructor(private authService: AuthService, private router: Router){

  }
  registerForm: FormGroup = new FormGroup({
    mailAddress: new FormControl('', [Validators.email, Validators.required]),
    username: new FormControl('', [Validators.minLength(4), Validators.maxLength(16), Validators.required]),
    password: new FormControl('', [Validators.minLength(8), Validators.maxLength(32), Validators.required]),
  });

  submit() {
    this.submitted = true;
    if (this.registerForm.valid) {
      this.authService.register({
        mailAddress: this.registerForm.controls['mailAddress'].value,
        username: this.registerForm.controls['username'].value,
        password: this.registerForm.controls['password'].value
      }).subscribe({
        next: (response) => {
          this.registerMessage = response.message;
          this.router.navigate(["home"])
        },
        error: (errorResponse: HttpErrorResponse) => {
          let error: ProblemDetails = errorResponse.error;
          this.failed = true;
          if (error.errors[0] != null){
            this.failedMessage = error.errors[0].reason;
          } else {
            this.failedMessage = "Register failed due to unknown reasons, please contact an administrator or try again later."
          }
        }
      })
      this.submitted = false;
    }
  }
  get mailAddress(){
    return this.registerForm.get('mailAddress')!;
  }
  get password(){
    return this.registerForm.get('password')!;
  }
  get username(){
    return this.registerForm.get('username')!;
  }
}
