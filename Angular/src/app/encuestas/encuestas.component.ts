import { Component, OnInit } from '@angular/core';

import { ReservasService } from '../_services/reservas.service';
import { Reserva } from '../_models';

@Component({
  selector: 'app-encuestas',
  templateUrl: './encuestas.component.html',
  styleUrls: ['./encuestas.component.scss']
})
export class EncuestasComponent implements OnInit {
  loading = true;
  
  reservas: Reserva[];

  constructor(
    private reservasSvc: ReservasService
  ) { }

  ngOnInit() {
    this.fetchReservas();
  }

  fetchReservas(){
    this.loading = true;
    this.reservasSvc.getMisReservas(false)
      .then((reservas: Reserva[]) => {
        this.reservas = reservas;
        this.loading = false;
      })
  }

  toDate(ISOstring: string) {
    return new Date(ISOstring);
  }
}
