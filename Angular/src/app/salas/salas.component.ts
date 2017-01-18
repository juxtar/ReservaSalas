import { Component, OnInit } from '@angular/core';

import { ITdDataTableColumn, ITdDataTableSelectEvent,
  TdDataTableSortingOrder, IPageChangeEvent,
  TdDialogService, TdDataTableService, ITdDataTableSortChangeEvent } from '@covalent/core';

import { Sala } from '../_models';
import { SalasService } from '../_services/salas.service';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.scss'],
  providers: [ SalasService ]
})
export class SalasComponent implements OnInit {

  data: Sala[];
  filteredData: Sala[];
  filteredTotal: number = 0;
  sortBy: string = 'Nombre';
  fromRow: number = 1;
  pageSize: number = 3;
  currentPage: number = 1;
  sortOrder: TdDataTableSortingOrder = TdDataTableSortingOrder.Ascending;
  searchTerm: string = '';

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

  constructor(
    private salasSvc: SalasService,
    private dialogSvc: TdDialogService,
    private tableSvc: TdDataTableService
  ) { }

  ngOnInit() {
    this.fetchSalas();
  }

  fetchSalas() {
    this.salasSvc.getSalas()
      .then((salas: Sala[]) => {
        this.data = salas;
        this.filter();
        this.selectedRow = null;
      })
      .catch(response => this.dialogSvc.openAlert({
        message: response.json().Message,
        title: 'Error',
        closeButton: 'Cerrar'
      }));
  }

  sort(sortEvent: ITdDataTableSortChangeEvent): void {
    this.sortBy = sortEvent.name;
    this.sortOrder = sortEvent.order;
    this.filter();
  }

  page(pagingEvent: IPageChangeEvent): void {
    this.fromRow = pagingEvent.fromRow;
    this.currentPage = pagingEvent.page;
    this.pageSize = pagingEvent.pageSize;
    this.filter();
  }

  search(searchTerm: string): void {
    this.searchTerm = searchTerm;
    this.filter();
  }

  filter(): void {
    let newData = this.data;
    newData = this.tableSvc.filterData(newData, this.searchTerm, true);
    this.filteredTotal = newData.length;
    newData = this.tableSvc.sortData(newData, this.sortBy, this.sortOrder);
    newData = this.tableSvc.pageData(newData, this.fromRow, this.currentPage * this.pageSize);
    this.filteredData = newData;
  }
}
