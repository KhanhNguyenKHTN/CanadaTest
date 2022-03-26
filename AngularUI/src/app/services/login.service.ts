import { Constants } from 'src/app/helper/constants.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private http: HttpClient) { }

  LogIn(username: string, password: string) { 
    let Data = JSON.stringify({UserName: username, Password: password});
    return this.http.post(Constants.LOGIN_URL, Data, {headers: {'Content-Type': 'application/json', encoding: 'utf8'}});
  }

  VerifyTwoFa(username: string, code: string){
    let Data = JSON.stringify({UserName: username, Code: code});
    return this.http.post(Constants.LOGIN_URL + '/twofapage', Data, {headers: {'Content-Type': 'application/json', encoding: 'utf8'}});
  }
}
