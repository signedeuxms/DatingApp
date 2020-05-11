import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  title = 'Dating App';
  matches = 'Matches';
  lists = 'Lists';
  messages = 'Messages';
  login = 'Login';
  welcomeUser = 'Welcome ';
  editProfile = 'Edit Profile';
  logOut = 'Logout';

  model: any = {};


  constructor(private authService: AuthService) {   }

  ngOnInit() {
  }

  submitLogin() {
    this.authService.loginUser(this.model).subscribe(
      next => {
        if (!this.welcomeUser.includes(this.model.username)) {
          this.welcomeUser = this.welcomeUser + this.model.username;
        }
        console.log('logged in successfully');
      },
      error => {
        console.log('failed to login');
      }
    );
  }


  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }


  logout() {
    localStorage.removeItem('token');
    console.log('\n logged out');
  }

}
