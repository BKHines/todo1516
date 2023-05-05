import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private baseApiUrl = 'http://localhost:15016';

  constructor() { }
}
