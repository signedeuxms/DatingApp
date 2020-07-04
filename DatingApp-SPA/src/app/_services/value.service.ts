import { Injectable } from '@angular/core';
import { Observable, from, of, throwError } from 'rxjs';
import { map, catchError  } from 'rxjs/operators';
import { Value } from '../models/value';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { promise } from 'protractor';
import * as Rx from 'rxjs/Rx';


@Injectable({
  providedIn: 'root'
})
export class ValueService {

  baseUrl = 'http://localhost:5000/api/values';

  constructor(private http: HttpClient) { }


  getValuesByObservable(): Observable<any> {
    
    /*return this.http.get(this.baseUrl).pipe(
      map((data: Value[]) => {
        console.log(data);
        return data;
      }), catchError( error => {
        console.log(error);
        return throwError( 'value json not found!' );
      })
      
    );*/

    return this.http.get(this.baseUrl);
  }


  // Les différentes méthodes du service http retournent des Observable<any>
  // The return type of http.get(…​) is Observable<Response>
  // Basically we still need to convert the Response to an array of Value
  // We can do that with our Observable by running a map operation where we convert 
  // each Response to an array of Value
  //getValues(): Observable<Response> {

  getValuesByPromise(): Promise<any> {

    const promise = new Promise((resolve, reject) => {

      this.http.get(this.baseUrl).toPromise()
      .then( response => {

        //console.log(response);

        resolve(response);
      });
    });

    return promise;
  }
 
}


// https://codecraft.tv/courses/angular/http/http-with-observables/
// https://codecraft.tv/courses/angular/http/http-with-observables/
