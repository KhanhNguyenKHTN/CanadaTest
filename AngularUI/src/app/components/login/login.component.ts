import { NavbarService } from './../../services/navbar.service';
import { LoginService } from './../../services/login.service';
import { UserInfor } from './../../models/user-infor.model';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/app/helper/constants.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @Input() userInfor: UserInfor;
  code = '';
  imageData = '';
  ggKey = '';
  errorMessage = '';

  constructor(private loginService: LoginService, private router: Router, public nav: NavbarService) {
    this.userInfor = new UserInfor();
  }

  ngOnInit(): void {
    this.nav.hide();
  }

  login() {
    // Validate data
    if (this.userInfor.username === ''){
      this.errorMessage = '';
      this.userInfor.errUsername = 'Invalid Username';
      return;
    }
    this.userInfor.errUsername = '';

    if (this.userInfor.password === ''){
      this.errorMessage = '';
      this.userInfor.errPassword = 'Invalid Password';
      return;
    }
    this.userInfor.errPassword = '';

    // Send login request
    this.loginService.LogIn(this.userInfor.username, this.userInfor.password).subscribe((data:any) => {
      if (data){
        if (data.GenerateCode === false) {
          this.hiddenSetupGGAuth();
        }
        else{
          this.imageData = data.Image;
          this.ggKey = data.Key;
        }
        
        // Clear error
        this.errorMessage = '';
        this.showPopUp();
      }
    }, error => {
      console.log(error);
      if (error.error){
        this.errorMessage = error.error;
      }
    });
  }

  verifyCode() {
    // Send request
    this.loginService.VerifyTwoFa(this.userInfor.username, this.code).subscribe((data:any) => {
      if (data){
        localStorage.setItem(Constants.USER_NAME, data.UserName);
        localStorage.setItem(Constants.USER_ROLES, JSON.stringify(data.Roles));
        localStorage.setItem(Constants.USER_TOKEN, data.Token);
        this.router.navigate(["/home"]);
      }
    }, error => {
      console.log(error);
      if (error.error){
        this.errorMessage = error.error;
      }
    });
  }

  showPopUp(){
    let twoFaPage = document.getElementById("twoFaPage");
    if (twoFaPage) {
        twoFaPage.classList.remove("hidden");
    }
  }

  hiddenSetupGGAuth(){
    let setup = document.getElementById("setupGoogleAuth");
    if (setup) {
      setup.classList.add("hidden");
    }
  }
}
