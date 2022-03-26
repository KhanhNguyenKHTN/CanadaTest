import { Constants } from 'src/app/helper/constants.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HelloService {

  constructor(private http: HttpClient) { }

  SayHello() {
    let token = localStorage.getItem(Constants.USER_TOKEN);
    console.log(token);

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'text/plain'
    });

    console.log(headers);
    return this.http.get(Constants.HOME_URL, {headers:headers, responseType: 'text'});
  }
}
