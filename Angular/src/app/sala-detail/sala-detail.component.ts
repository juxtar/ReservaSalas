import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TdDialogService } from '@covalent/core';

import { Sala } from '../_models';
import { SalasService } from '../_services/salas.service';

@Component({
  selector: 'app-sala-detail',
  templateUrl: './sala-detail.component.html',
  styleUrls: ['./sala-detail.component.scss']
})
export class SalaDetailComponent implements OnInit {
  @Input()
  sala: Sala;

  @Output()
  onActualizar = new EventEmitter<Sala>();

  get detail() {
    if (this.sala.ID == 1) {
      return "Disponible"
    }
    else {
      return "Reservada de 9 a 11"
    }
  }

  constructor(
    private dialogSvc: TdDialogService,
    private salas: SalasService
  ) { }

  ngOnInit() {
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
        this.salas.deleteSala(this.sala.ID)
          .then(() => this.onActualizar.emit(this.sala));
      }
    });
  }
}
