import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  public isAuthorized: boolean;

  constructor(private router: Router) { }

  ngOnInit() {
    if (localStorage.getItem('username')) {
      this.isAuthorized = true;
    }
    else {
      this.isAuthorized = false;
    }
  }

  signout() {
    localStorage.removeItem('username');
    this.router.navigate(['/login'], { replaceUrl: true });
  }

}
