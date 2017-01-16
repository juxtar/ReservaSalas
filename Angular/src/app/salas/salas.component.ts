import { Component, OnInit } from '@angular/core';

import { ITdDataTableColumn, ITdDataTableSelectEvent } from '@covalent/core';

import { Sala } from '../_models';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.scss']
})
export class SalasComponent implements OnInit {

  data: Sala[] = [
    { ID: 1, Nombre: 'Centro 1', Capacidad: 12, Ubicacion: 'Centro', Tipo: 1 },
    { ID: 2, Nombre: 'Sistemas 2', Capacidad: 8, Ubicacion: 'Sistemas', Tipo: 2 },
    { ID: 1, Nombre: 'Centro 1', Capacidad: 12, Ubicacion: 'Centro', Tipo: 0 },
    { ID: 1, Nombre: 'Centro 1', Capacidad: 12, Ubicacion: 'Centro', Tipo: 4 }
  ];

  columns: ITdDataTableColumn[] = [
    { name: 'Nombre', label: 'Nombre' },
    { name: 'Capacidad', label: 'Capacidad' },
    { name: 'Ubicacion', label: 'Ubicacion' },
    { name: 'Tipo', label: 'Tipo de Sala', numeric: false,
        format: t => t == 0 ? 'Capacitación' : 
                     t == 1 ? 'Reunión' :
                     t == 2 ? 'Auditorio' : '-' },
  ];

  selectedRow: Sala = null;

  selected(selectEvent: ITdDataTableSelectEvent): void {
    if(selectEvent.selected)
      this.selectedRow = selectEvent.row;
    else
      this.selectedRow = null;
  }

  constructor() { }

  ngOnInit() {
  }

}
