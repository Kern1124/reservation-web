import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { OnInit } from '@angular/core';
import { LoginResponse } from 'src/types';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent{
  username?: string;
  mail?: string;
  constructor(){}
}
