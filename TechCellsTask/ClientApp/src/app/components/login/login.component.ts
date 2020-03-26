import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { DataService } from 'src/app/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public username: string = '';
  public returnUrl: string = '/';

  constructor(
    private router: Router,
    private dataService: DataService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.activatedRoute.queryParamMap.subscribe(s => {
      this.returnUrl = s.get('returnUrl');
    });
  }

  signIn() {
    if (!this.username) {
      alert('Please enter the username');
    }
    else {
      this.dataService.get<any>('/api/user/login', { username: this.username })
        .subscribe(s => {
          localStorage.setItem('username', s.userName);
          this.redirect();
        }, error => console.log(error));
    }
  }

  redirect() {
    if (this.returnUrl) {
      this.router.navigate([this.returnUrl], { replaceUrl: true });
    }
    else {
      this.router.navigate(['/'], { replaceUrl: true });
    }
  }

}
