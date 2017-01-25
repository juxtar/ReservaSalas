import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { TdDialogService } from '@covalent/core';

import { Reserva, Sala } from '../_models';
import { ReservasService } from '../_services/reservas.service';
import { SalasService } from '../_services/salas.service';

@Component({
  selector: 'app-reserva-formulario',
  templateUrl: './reserva-formulario.component.html',
  styleUrls: ['./reserva-formulario.component.scss']
})
export class ReservaFormularioComponent implements OnInit {
  capacidadMax = 1;

  salaReunion = true;
  horasValidas = true;
  fecha: string;
  inicio: string;
  fin: string;
  hoy = new Date();
  get minFin() {
    let ret = new Date(this.inicio);
    return ret.setHours(ret.getHours() + 1);
  }

  nombreSala = '';

  reserva: Reserva = {
    Inicio: null,
    Fin: null,
    Cantidad: null,
    Sala: null,
    Motivo: null,
    Servicio: false,
    Almuerzo: false,
    Proyector: false,
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reservasSvc: ReservasService,
    private salasSvc: SalasService,
    private dialogSvc: TdDialogService
  ) { }

  ngOnInit() {
    this.route.params
      .subscribe((params: Params) => {
        this.salasSvc.getSala(+params['id'])
          .then((sala: Sala) => {
            this.reserva.Sala = sala;
            this.capacidadMax = sala.Capacidad;
            this.nombreSala = sala.Nombre;
            this.salaReunion = sala.Tipo == 1;
          });
      });
  }

  onSubmit() {
    this.reservasSvc.newReserva(this.reserva)
      .then(() => {
        this.dialogSvc.openAlert({
          message: '¡Sala reservada con éxito!',
          closeButton: 'Volver'
        }).afterClosed().subscribe(() => {
          this.router.navigateByUrl('/reservas');
        })
      })
      .catch((response) => this.handleError(response.Message));
  }

  validarTiempo(event) {
    if (this.fecha && this.inicio && this.fin) {
      this.reserva.Inicio = this.buildDateISO(this.fecha, this.inicio);
      this.reserva.Fin = this.buildDateISO(this.fecha, this.fin);
      let inicio: any = new Date(this.reserva.Inicio);
      let fin: any = new Date(this.reserva.Fin);
      // Diferencia entre fechas en milisegundos
      let diff = fin-inicio;
      // Horas de diferencia debe ser al menos 1
      this.horasValidas = (diff / 1000 / 60 / 60) >= 1;
    }
  }

  buildDateISO(date, time) {
    let value = new Date(date);
    let [hours, minutes] = time.split(':');
    value.setHours(+hours);
    value.setMinutes(+minutes);
    return value.toISOString();
  }

  handleError(message: string) {
    this.dialogSvc.openAlert({
      message: message || 'Ha ocurrido un error, por favor intente nuevamente.',
      closeButton: 'Aceptar'
    })
  }
}
