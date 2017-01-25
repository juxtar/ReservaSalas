import { Component, OnInit } from '@angular/core';

import { ITdDataTableColumn, ITdDataTableSelectEvent,
  TdDataTableSortingOrder, IPageChangeEvent, TdLoadingService,
  TdDialogService, TdDataTableService, ITdDataTableSortChangeEvent } from '@covalent/core';

import { Reserva } from '../_models';
import { ReservasService } from '../_services/reservas.service';
import { ReservautilsService } from '../_utils/reservautils.service';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.scss'],
  providers: [ ReservautilsService ]
})
export class ReservasComponent implements OnInit {

  loading = true;
  loadingId;

  data: Reserva[];
  filteredData: Reserva[];
  filteredTotal: number = 0;
  sortBy: string = 'Anulada';
  fromRow: number = 1;
  pageSize: number = 10;
  currentPage: number = 1;
  sortOrder: TdDataTableSortingOrder = TdDataTableSortingOrder.Ascending;
  searchTerm: string = '';

  mostrarCaducadas = false;
  mostrarAnuladas = false;

  columns: ITdDataTableColumn[] = [
    { name: 'Anulada', label: 'Estado' },
    { name: 'Sala.Nombre', label: 'Sala' },
    { name: 'Motivo', label: 'Motivo' },
    { name: 'Responsable.Descripcion', label: 'Responsable' },
    { name: 'Inicio', label: 'Fecha', format: this.reservaUtils.getDayFromISO },
    { name: 'Inicio', label: 'Inicio', format: this.reservaUtils.getTimeFromISO },
    { name: 'Fin', label: 'Fin', format: this.reservaUtils.getTimeFromISO },
  ];

  constructor(
    private reservasSvc: ReservasService,
    private tableSvc: TdDataTableService,
    private dialogSvc: TdDialogService,
    private reservaUtils: ReservautilsService,
  ) { }

  ngOnInit() {
    this.fetchReservas();
  }

  fetchReservas() {
    this.registerLoading();
    this.reservasSvc.getReservasFiltered(
              this.mostrarAnuladas ? "null" : "false",
              this.mostrarCaducadas ? "null" : "false")
      .then((reservas: Reserva[]) => {
        this.data = reservas;
        this.resolveLoading();
        this.filter();
      })
      .catch(response => {
        console.log(response);
        this.dialogSvc.openAlert({
        message: response.json().Message 
            || 'Ha ocurrido un error, intente nuevamente.',
        title: 'Error',
        closeButton: 'Cerrar'
      })});
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

  // Implementar propio sort y filter
  filter(): void {
    let newData = this.data;
    newData = this.tableSvc.filterData(newData, this.searchTerm, true);
    this.filteredTotal = newData.length;
    newData = this.tableSvc.sortData(newData, this.sortBy, this.sortOrder);
    newData = this.tableSvc.pageData(newData, this.fromRow, this.currentPage * this.pageSize);
    this.filteredData = newData;
  }

  displayStatus(reserva: Reserva) {
    if (reserva.Anulada)
      return 'Anulada';
    if ((new Date(reserva.Fin)).valueOf() < Date.now())
      return 'Caducada'
    return 'Vigente'
  }

  displayIcon(reserva: Reserva) {
    let status = this.displayStatus(reserva);
    switch (status) {
      case 'Anulada':
        return 'cancel';
      case 'Caducada':
        return 'event_busy';
      case 'Vigente':
        return 'event_available';
    }
  }
  
  toggleFilter(filter: string, value: boolean) {
    switch(filter){
      case 'caducada':
        this.mostrarCaducadas = value;
        break;
      case 'anulada':
        this.mostrarAnuladas = value;
        break;
      default:
        return;
    }
    this.fetchReservas();
  }

  registerLoading() {
    if (this.loadingId) clearTimeout(this.loadingId);
    this.loadingId = setTimeout(() => this.loading = true, 250);
  }

  resolveLoading() {
    if (this.loadingId) clearTimeout(this.loadingId);
    this.loading = false;
  }
}
