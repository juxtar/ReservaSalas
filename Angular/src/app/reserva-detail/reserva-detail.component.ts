import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { TdDialogService } from '@covalent/core';

import { Reserva, Sala } from '../_models';
import { ReservasService } from '../_services/reservas.service';

@Component({
  selector: 'app-reserva-detail',
  templateUrl: './reserva-detail.component.html',
  styleUrls: ['./reserva-detail.component.scss']
})
export class ReservaDetailComponent implements OnInit {
  loading = true;

  reserva: Reserva;

  detalles = ['Reserva', 'Responsable', 'Sala'];

  detallesReserva;
  detallesResponsable;
  detallesSala;

  icons = {
    date: 'event',
    Ubicacion: 'location_on',
    true: 'done',
    false: 'close',
    Capacidad: 'people',
    Cantidad: 'people',
    Descripcion: 'account_circle',
    Legajo: 'assignment_ind',
    Motivo: 'short_text',
    Nombre: 'label',
    Tipo: 'label_outline'
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reservasSvc: ReservasService,
    private dialogSvc: TdDialogService,
  ) { }

  ngOnInit() {
    this.route.params
      .subscribe((params: Params) => {
        this.reservasSvc.getReserva(+params['id'])
          .then((reserva: Reserva) => {
            this.loading = false;
            this.reserva = reserva;
            this.detallesReserva = this.formatDetails(reserva);
          })
          .catch(response => this.handleError(response.json().Message));
      });
  }

  formatDetails(object: any): any[] {
    let value = [];
    let keys = Object.keys(object);
    keys.forEach((key) => {
      if (key == 'Responsable')
        this.detallesResponsable = this.formatDetails(object[key])
      else if (key == 'Sala')
        this.detallesSala = this.formatDetails(object[key])
      else {
        if (key == 'ID')
          return;
        let detail = { name: key, value: object[key], icon: (this.icons[key] || 'details' ) };
        if (key == 'Inicio' || key == 'Fin') {
          detail.value = (new Date(detail.value)).toLocaleString();
          detail.icon = this.icons.date;
        }
        if (key == 'Tipo')
          detail.value = detail.value == 0 ? 'Capacitación' : 
                     detail.value == 1 ? 'Reunión' :
                     detail.value == 2 ? 'Auditorio' : '-';
        if (typeof detail.value == 'boolean') {
          detail.icon = detail.value ? this.icons.true : this.icons.false;
          detail.value = detail.value ? 'Sí':'No';
        }
        value.push(detail);
      }
    })
    return value;
  }

  confirmarEliminar(): void {
    if ((new Date(this.reserva.Fin)) < (new Date()) ) {
      this.handleError('La reserva no se puede anular porque ya caducó.');
      return;
    }
    this.dialogSvc.openConfirm({
      message: `¿Está seguro que desea anular la reserva?
      Esta acción no se puede deshacer.`,
      disableClose: true,
      title: '¿Anular Reserva?',
      cancelButton: 'Cancelar',
      acceptButton: 'Anular'
    }).afterClosed().subscribe((accept: boolean) => {
      if (accept) {
        this.reservasSvc.deleteReserva(this.reserva.ID)
          .then(() => this.router.navigate(['/reservas']))
          .catch(response => this.handleError(response.json().Message));
      }
    });
  }

  handleError(message: string) {
    this.dialogSvc.openAlert({
      message: message || 'Ha ocurrido un error, por favor intente nuevamente.',
      closeButton: 'Aceptar'
    })
  }
}
