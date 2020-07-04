import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

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


  constructor(public authService: AuthService,
              private alertifyService: AlertifyService) {   }

  ngOnInit() {
  }

  submitLogin() {
    this.authService.loginUser(this.model).subscribe(
      next => {

        if (!this.welcomeUser.includes(this.model.username)) {
          this.welcomeUser = this.welcomeUser + this.model.username;
        }

        console.log('logged in successfully');
        this.alertifyService.success('logged in successfully');
      },
      error => {
        console.log('\n NavbarComponent => failed to login => ' + error);
        this.alertifyService.error(error);
      }
    );
  }


  loggedIn() {
    return this.authService.loggedIn();
  }


  logout() {
    localStorage.removeItem('token');
    console.log('\n logged out');
    this.alertifyService.message('logged out');
  }

}
