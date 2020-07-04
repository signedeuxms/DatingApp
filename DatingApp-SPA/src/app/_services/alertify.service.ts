import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';
import { SelectControlValueAccessor } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }
  confirm(message: string, okCallBack: () => any) {
    alertify.confirm(message, (event: any) => {

      if (event) {
        okCallBack();
      } else {}

    });
  }


  success(message: string) {
    alertify.success(message);
  }


  error(error: string) {
    alertify.error(error);
  }


  warning(warn: string) {
    alertify.warning(warn);
  }


  message(message: string) {
    alertify.message(message);
  }

}



