import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TdDialogService } from '@covalent/core';

import { User, Password } from '../_models';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.scss'],
  providers: [ UserService ]
})
export class RegistroComponent implements OnInit {
  usuario = new User();

  constructor(
    private userSvc: UserService,
    private dialogSvc: TdDialogService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.userSvc.createUser(this.usuario)
      .then(usuario => {
        this.showMessage(null, false);
        this.router.navigateByUrl('/login');
      })
      .catch((response) => {
        this.showMessage(response.json && response.json().Message, true);
      });
  }

  showMessage(message: string, err: boolean) {
    this.dialogSvc.openAlert({
      title: err? 'Error' : '¡Éxito!',
      message: message || 
        (err? 'Ha ocurrido un error, por favor intente nuevamente.' :
        '¡Usuario creado con éxito!'),
      closeButton: 'Continuar'
    })
  }
}
