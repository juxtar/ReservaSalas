import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TdLoadingService } from '@covalent/core';

import { AuthenticationService } from '../_services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private loadingService: TdLoadingService
  ) { }

  ngOnInit() {
    // reset login status
    this.authenticationService.logout();
  }

  login(): void {
    this.registerLoading();
    this.authenticationService.login(this.username, this.password)
        .subscribe(result => {
          if (result === true) {
            // login successful
            this.router.navigate(['/salas']);
          } else {
            // login failed
            // TODO show error
            this.resolveLoading();
          }
        });
  }

  registerLoading(): void {
    this.loadingService.register('loggingIn');
  }

  resolveLoading(): void {
    this.loadingService.resolve('loggingIn');
  }
}
