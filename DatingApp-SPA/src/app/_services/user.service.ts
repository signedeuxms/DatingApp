import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../interfaces/user';
import { Observable } from 'rxjs';


const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem( 'token' )
  })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;

  constructor(private _httpService: HttpClient) { 

  }


  getUsers(): Observable<User> {
    return this._httpService.get<User>(this.baseUrl + 'users', httpOptions);
  }


  getUser(id: number): Observable<User> {
    return this._httpService.get<User>(this.baseUrl + 'users/' + id, httpOptions);
  }
}
