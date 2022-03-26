import { HelloService } from './../../services/hello.service';
import { NavbarService } from './../../services/navbar.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hello',
  templateUrl: './hello.component.html',
  styleUrls: ['./hello.component.css']
})
export class HelloComponent implements OnInit {
  message: string = '';
  constructor(private helloService: HelloService, private nav: NavbarService) { }

  ngOnInit(): void {
    this.nav.show();
    this.nav.SetPage('hello');
    this.getMessage();
  }

  getMessage() {
    this.helloService.SayHello().subscribe((data:any) => {
      this.message = data;
    }, (error) => {
      alert(error.message);
    });
  }
}
