import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { Sala } from '../_models';
import { AuthenticationService } from './';
import { API_URL } from '../httpconfig';

@Injectable()
export class SalasService {

  constructor(
    private auth: AuthenticationService,
    private http: Http
  ) { }

  get options(): RequestOptions {
    let headers = new Headers({'Authorization': 'Bearer ' + this.auth.token});
    let options = new RequestOptions({headers: headers});
    return options;
  }

  getSalas(): Promise<Sala[]> {
    return this.http.get(API_URL + '/api/Salas', this.options)
      .toPromise()
      .then((response: Response) => {
        return response.json() as Sala[];
      });
  }

  deleteSala(id: number): Promise<Response> {
    return this.http.delete(API_URL + `/api/Salas/${id}`, this.options)
      .toPromise();
  }
}
