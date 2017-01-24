import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { TdDialogService } from '@covalent/core';

import { Sala, Reserva } from '../_models';
import { SalasService } from '../_services/salas.service';
import { ReservasService } from '../_services/reservas.service';
import { ReservautilsService } from '../_utils/reservautils.service';

@Component({
  selector: 'app-sala-detail',
  templateUrl: './sala-detail.component.html',
  styleUrls: ['./sala-detail.component.scss'],
  providers: [ ReservautilsService ]
})
export class SalaDetailComponent implements OnInit, OnChanges {
  @Input()
  sala: Sala;

  @Output()
  onActualizar = new EventEmitter<Sala>();

  reservas: Reserva[] = [];

  get hasReservas() {
    return this.reservas.length != 0;
  }

  constructor(
    private dialogSvc: TdDialogService,
    private salasSvc: SalasService,
    private reservasSvc: ReservasService,
    private reservaUtils: ReservautilsService
  ) { }

  ngOnInit() {
  }

  ngOnChanges() {
    this.fetchReservas();
  }
  
  confirmarEliminar(): void {
    this.dialogSvc.openConfirm({
      message: `¿Está seguro que desea eliminar la sala ${this.sala.Nombre}?
      Esta acción no se puede deshacer.`,
      disableClose: true,
      title: '¿Eliminar Sala?',
      cancelButton: 'Cancelar',
      acceptButton: 'Eliminar'
    }).afterClosed().subscribe((accept: boolean) => {
      if (accept) {
        this.salasSvc.deleteSala(this.sala.ID)
          .then(() => this.onActualizar.emit(this.sala));
      }
    });
  }

  fetchReservas() {
    this.reservasSvc.getReservasFiltered(this.sala.ID, null, false, false)
      .then((reservas: Reserva[]) => {
        this.reservas = reservas;
      });
  }

  formatReservaDia(reserva: Reserva): string {
    return this.reservaUtils.getDayFromISO(reserva.Inicio);
  }

  formatReservaHoras(date: string): string {
    return this.reservaUtils.getTimeFromISO(date);
  }
}
