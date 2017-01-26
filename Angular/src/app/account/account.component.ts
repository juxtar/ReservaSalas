import { Component, OnInit } from '@angular/core';

import { User } from '../_models';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

  usuario: User;

  constructor() { }

  ngOnInit() {
  }

}
