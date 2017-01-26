import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { API_URL } from '../httpconfig';

@Injectable()
export class AuthenticationService {
    public token: string;

    constructor(private http: Http) {
        // setear token si ya hay uno guardado válido
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
        if (this.token && (currentUser.expiration <= Date.now())) {
            this.token = null;
            localStorage.removeItem('currentUser');
        }
    }

    login(username: string, password: string): Observable<boolean> {
        let headers = new Headers({'Content-Type': 'application/x-www-form-urlencoded'});
        return this.http.post(API_URL + '/oauth/token', 
                `username=${username}&password=${password}&grant_type=password`,
                headers)
            .map((response: Response) => {
                let token = response.json() && response.json().access_token;
                if (token) {
                    let expiration = Date.now() + (response.json().expires_in * 1000);

                    // setear token
                    this.token = token;

                    // guardar en local storage
                    localStorage.setItem('currentUser', 
                            JSON.stringify({
                                username: username,
                                token: token,
                                expiration: expiration
                            }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    logout(): void {
        // clear token remove user from local storage to log out
        this.token = null;
        localStorage.removeItem('currentUser');
    }

    isAuthenticated(): boolean {
        return !!this.token;
    }
}
