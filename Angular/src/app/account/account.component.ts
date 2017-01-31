import { Component, OnInit } from '@angular/core';

import { TdDialogService } from '@covalent/core';

import { User, Password } from '../_models';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
  providers: [ UserService ]
})
export class AccountComponent implements OnInit {
  loading = true;

  usuario: User = {
    UserName: null,
    Email: null,
    FullName: null
  };

  newPassword = new Password();

  constructor(
    private userSvc: UserService,
    private dialogSvc: TdDialogService
  ) { }

  ngOnInit() {
    this.fetchUser();
  }

  fetchUser() {
    this.userSvc.myUser().then(user => this.usuario = user)
      .then(() => this.loading = false);
  }

  cambiarClave() {
    this.userSvc.changePassword(this.newPassword)
      .then((response) => this.showMessage(response.json().Message, false))
      .catch((response) => this.showMessage(response.json().Message, true))
  }

  showMessage(message: string, err: boolean) {
    this.dialogSvc.openAlert({
      title: err? 'Error' : '¡Éxito!',
      message: message || 
        (err? 'Ha ocurrido un error, por favor intente nuevamente.' :
        'Contraseña cambiada con éxito.'),
      closeButton: 'Aceptar'
    })
  }
}
