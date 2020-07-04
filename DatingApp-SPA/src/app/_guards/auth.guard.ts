import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private routerService: Router,
              private alertifyservice: AlertifyService) {

  }

  canActivate(): boolean {

    if ( this.authService.loggedIn() ) {
      return true;
    }

    this.alertifyservice.error( 'you shall not pass !!!' );
    this.routerService.navigate( ['/home'] );
    
    return false;
    
  }
  
}
