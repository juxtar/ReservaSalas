import { Component, OnInit } from '@angular/core';

import { User } from '../_models';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
  providers: [ UserService ]
})
export class AccountComponent implements OnInit {

  usuario: User = {
    userName: null,
    email: null,
    fullName: null
  };

  constructor(
    private userSvc: UserService
  ) { }

  ngOnInit() {
    this.fetchUser();
  }

  fetchUser() {
    this.userSvc.myUser().then(user => this.usuario = user);
  }

  cambiarClave() {

  }
}
