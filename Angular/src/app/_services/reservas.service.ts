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

  getReservas(): Promise<Reserva[]> {
    return this.http.get(API_URL + '/api/Reservas')
      .toPromise()
      .then((response: Response) => {
        return response.json() as Reserva[];
      });
  }

  getReservasFiltered(idSala?: number, idResponsable?: number,
      anulada?: boolean, caducada?: boolean): Promise<Reserva[]> {
    let query = `/api/Reservas?IdSala=${idSala || null}` +
                `&IdResponsable=${idResponsable || null}` +
                `&Anulada=${anulada || null}` + 
                `&Caducada=${caducada || null}`;
    return this.http.get(API_URL + query)
        .toPromise()
        .then((response: Response) => {
          return response.json() as Reserva[];
        });
  }
}
