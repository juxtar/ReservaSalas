import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { TdDialogService } from '@covalent/core';

import { Encuesta } from '../_models';
import { ReservasService } from '../_services/reservas.service';

@Component({
  selector: 'app-encuesta-formulario',
  templateUrl: './encuesta-formulario.component.html',
  styleUrls: ['./encuesta-formulario.component.scss']
})
export class EncuestaFormularioComponent implements OnInit {

  encuesta: Encuesta = new Encuesta();

  idReserva: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reservasSvc: ReservasService,
    private dialogSvc: TdDialogService
  ) { }

  ngOnInit() {
    this.route.params
      .subscribe((params: Params) => {
        this.idReserva = +params['id'];
        this.checkReserva();
      });
  }

  checkReserva(){
    this.reservasSvc.getReserva(this.idReserva)
      .then(reserva => {
        if (reserva.Encuesta)
          this.handleError('Ya se realizó la encuesta para esta reserva.');
        if (reserva.Anulada)
          this.handleError('No se puede realizar una encuesta sobre una reserva anulada.');
      })
      .catch(response => this.handleError(response.json && response.json().Message))
  }

  onSubmit() {
    this.reservasSvc.postEncuesta(this.idReserva, this.encuesta)
      .then(reserva => {
        this.dialogSvc.openAlert({
          title: '¡Éxito!',
          message: '¡Encuesta realizada con éxito!'
        });
        this.router.navigateByUrl('/encuestas');
      })
      .catch(response => {
        this.handleError(response.json && response.json().Message);
      })
  }

  handleError(message: string) {
    this.dialogSvc.openAlert({
      title: 'Error',
      message: message || 'Ha ocurrido un error, por favor intente nuevamente.',
      closeButton: 'Aceptar'
    })
    this.router.navigateByUrl('/encuestas');
  }
}
