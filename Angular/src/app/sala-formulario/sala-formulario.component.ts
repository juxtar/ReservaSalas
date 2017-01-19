import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { TdDialogService } from '@covalent/core';

import { Sala } from '../_models';
import { SalasService } from '../_services';

@Component({
  selector: 'app-sala-formulario',
  templateUrl: './sala-formulario.component.html',
  styleUrls: ['./sala-formulario.component.scss']
})
export class SalaFormularioComponent implements OnInit {
  modificacion: boolean;

  sala: Sala = {
    Nombre: null,
    Capacidad: null,
    Ubicacion: null,
    Tipo: null
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private salasSvc: SalasService,
    private dialogSvc: TdDialogService
  ) { }

  ngOnInit() {
    if (this.route.snapshot.url.find((url) => url.path == 'nueva'))
      this.modificacion = false;
    else {
      this.route.params
            .subscribe((params: Params) => {
              this.salasSvc.getSala(+params['id'])
                .then((sala: Sala) => this.sala = sala);
            });
      this.modificacion = true;
    }
  }

  onSubmit() {
    if (this.modificacion) {
      this.salasSvc.updateSala(this.sala.ID, this.sala)
        .then(() => {
          this.dialogSvc.openAlert({
            message: '¡Sala actualizada con éxito!',
            closeButton: 'Volver'
          }).afterClosed().subscribe(() => {
            this.router.navigateByUrl('/salas');
          })
        })
        .catch((response) => this.handleError(response.Message));
    }
    else {
      this.salasSvc.newSala(this.sala)
        .then(() => {
          this.dialogSvc.openAlert({
            message: '¡Sala creada con éxito!',
            closeButton: 'Volver'
          }).afterClosed().subscribe(() => {
            this.router.navigateByUrl('/salas');
          })
        })
        .catch((response) => this.handleError(response.Message));
    }
  }

  handleError(message: string) {
    this.dialogSvc.openAlert({
      message: message || 'Ha ocurrido un error, por favor intente nuevamente.',
      closeButton: 'Aceptar'
    })
  }
}
