import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {
  visible: boolean;
  page: string;
  constructor() { 
    this.visible = true;
    this.page = '';
  }

  hide() { this.visible = false; }

  show() { this.visible = true; }

  SetPage(name: string) { this.page = name;}
}
