import { Component, OnInit, Input , Output, EventEmitter} from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  signUp = 'Sign up';
  register = 'Register';
  cancel = 'Cancel';

  model: any = {};

  @Output() cancelRegisterOut = new EventEmitter();

  constructor( private authService: AuthService) { }

  ngOnInit() {
  }

  registerUser() {
    this.authService.registerUser(this.model).subscribe(
      () => {
        console.log('\n registration successful');
      },
      error => {
        console.error(' \n RegisterComponent => failed to register => ' + error);
        console.log(' \n RegisterComponent => failed to register => ' + error);
      }
    );
    console.log(this.model);
  }

  cancelRegister() {
    this.cancelRegisterOut.emit(false);
    console.log('\n cancelled');
  }

}
