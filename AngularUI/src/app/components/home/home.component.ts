import { HelloService } from './../../services/hello.service';
import { NavbarService } from './../../services/navbar.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  message: string = '';

  constructor(private helloService: HelloService, private nav: NavbarService) { }

  ngOnInit(): void {
    this.nav.show();
    this.nav.SetPage('home');
    this.getMessage();
  }
  getMessage() {
    this.helloService.SayHello().subscribe((data: any) => {
      this.message = data;
    }, (error) => {
      alert(error.message);
    });
  }
}
