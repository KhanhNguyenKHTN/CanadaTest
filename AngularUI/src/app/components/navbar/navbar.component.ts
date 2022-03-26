import { Router } from '@angular/router';
import { Constants } from 'src/app/helper/constants.model';
import { NavbarService } from './../../services/navbar.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public nav: NavbarService, private router: Router) { }

  ngOnInit(): void {
  }

  logOut(){ 
    localStorage.removeItem(Constants.USER_NAME);
    localStorage.removeItem(Constants.USER_TOKEN);
    localStorage.removeItem(Constants.USER_ROLES);
    this.router.navigate(['/login']);
  }
}
