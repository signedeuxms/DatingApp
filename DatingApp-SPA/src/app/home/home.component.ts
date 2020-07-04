import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ValueService } from '../_services/value.service';
import { Value } from '../models/value';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  findYourMatch = 'Find your match';
  register = 'Register';
  learnMore = 'Learn more';

  registerMode = false;
  values: any = [];

  valuesByObservable: any = {};
  valuesByPromise: any = {};

  constructor(private http: HttpClient, private valueService: ValueService) { }

  ngOnInit() {
    //this.getValues();

    this.valueService.getValuesByObservable().subscribe(
      (response: Value[]) => {

        console.log('getValuesByObservable()');
        this.valuesByObservable = response;
        console.log(response);
        this.values = response;
      },
      error => {
        console.log('ValueComponent.ts => getValues() => error \n');
        console.log(error);
      }
    );

    /*this.valueService.getValuesByPromise().then( result => {
      console.log('getValuesByPromise()');
      console.log(result);
      this.valuesByPromise = result;
      this.values = result;
    })
    .catch(error => {
        console.log(error);
    });*/
  }

  registerToggle() {
    this.registerMode = true;
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.values = response;
      },
      error => {
        console.log('ValueComponent.ts => getValues() => error \n' + error);
      }
    );
  }


  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}



/*

*/