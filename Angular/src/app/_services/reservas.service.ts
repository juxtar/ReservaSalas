import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { Reserva } from '../_models';
import { AuthenticationService } from './authentication.service';
import { API_URL } from '../httpconfig';

@Injectable()
export class ReservasService {

  constructor(
    private http: Http,
    private auth: AuthenticationService
  ) { }

  get options(): RequestOptions {
    let headers = new Headers({'Authorization': 'Bearer ' + this.auth.token});
    let options = new RequestOptions({headers: headers});
    return options;
  }

  getReserva(id: number): Promise<Reserva> {
    return this.http.get(API_URL + `/api/Reservas/${id}`, this.options)
      .toPromise()
      .then((response: Response) => {
        return response.json() as Reserva;
      })
  }

  getReservas(): Promise<Reserva[]> {
    return this.http.get(API_URL + '/api/Reservas')
      .toPromise()
      .then((response: Response) => {
        return response.json() as Reserva[];
      });
  }

  getReservasFiltered(anulada: string, caducada: string,
    idSala?: number, idResponsable?: number): Promise<Reserva[]> {
    let query = `/api/Reservas?IdSala=${idSala || null}` +
                `&IdResponsable=${idResponsable || null}` +
                `&Anulada=${anulada}` + 
                `&Caducada=${caducada}`;
    return this.http.get(API_URL + query)
        .toPromise()
        .then((response: Response) => {
          return response.json() as Reserva[];
        });
  }

  newReserva(reserva: Reserva): Promise<Reserva> {
    return this.http.post(API_URL + '/api/Reservas', reserva, this.options)
      .toPromise()
      .then((response: Response) => {
        return response.json() as Reserva;
      })
  }

  deleteReserva(id: number): Promise<Response> {
    return this.http.delete(API_URL + `/api/Reservas/${id}`, this.options)
      .toPromise();
  }
}
