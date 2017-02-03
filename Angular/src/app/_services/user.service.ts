import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { User, Password } from '../_models';
import { AuthenticationService } from './authentication.service';
import { API_URL } from '../httpconfig';

@Injectable()
export class UserService {

  constructor(
    private auth: AuthenticationService,
    private http: Http
  ) { }

  get headers(): Headers {
    let headers = new Headers({'Authorization': 'Bearer ' + this.auth.token});
    return headers;
  }

  myUser(): Promise<User> {
    return this.http.get(API_URL + '/api/Accounts/Me', {headers: this.headers})
      .toPromise()
      .then((response: Response) => {
          return response.json() as User;
      })
  }

  changePassword(password: Password): Promise<Response> {
    return this.http.post(API_URL + '/api/Accounts/ChangePassword', password, {headers: this.headers})
      .toPromise();
  }

  createUser(user: User): Promise<User> {
    return this.http.post(API_URL + '/api/Accounts/Create', user, {headers: this.headers})
      .toPromise()
      .then((response: Response) => {
        return response.json() as User;
      })
  }
}