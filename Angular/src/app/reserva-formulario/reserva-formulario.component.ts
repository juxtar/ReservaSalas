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

  horasValidas = true;
  fecha: Date;
  inicio: Date;
  fin: Date;
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
    Servicio: false, // Solo salas reunion
    Almuerzo: false, // Solo salas reunion
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
          });
      });
  }

  onSubmit() {
    console.log(this.reserva);
    console.log(this.fecha, this.inicio, this.fin);
  }

  validarTiempo(event) {
    
  }
}
