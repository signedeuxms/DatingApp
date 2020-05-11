import { Component, OnInit } from '@angular/core';

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

  model: any = {};


  constructor() {   }

  ngOnInit() {
  }

  submitLogin() {
    console.log('\n' + this.model);
  }

}
