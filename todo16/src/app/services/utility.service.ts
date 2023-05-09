import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  uniqueInArrayCheck(value: any, idx: number, arry: any[]) {
    return arry.indexOf(value) == idx;
  }
}
