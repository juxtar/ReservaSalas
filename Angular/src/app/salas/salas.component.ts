import { Component, OnInit } from '@angular/core';

import { ITdDataTableColumn, ITdDataTableSelectEvent } from '@covalent/core';

import { Sala } from '../sala';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.scss']
})
export class SalasComponent implements OnInit {

  data: Sala[] = [
    { id: 1, nombre: 'Centro 1', capacidad: 12, ubicacion: 'Centro', tipo: 'Reunion' },
    { id: 2, nombre: 'Sistemas 2', capacidad: 8, ubicacion: 'Sistemas', tipo: 'Reunion' }
  ];

  columns: ITdDataTableColumn[] = [
    { name: 'nombre', label: 'Nombre' },
    { name: 'capacidad', label: 'Capacidad' },
    { name: 'ubicacion', label: 'Ubicacion' },
    { name: 'tipo', label: 'Tipo de Sala' },
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
