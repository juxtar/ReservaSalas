import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable() 
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate() {
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && (currentUser.expiration > Date.now())) {
            // logged in so return true
            return true;
        }
        localStorage.removeItem('currentUser');
        // not logged in so redirect to login page
        this.router.navigate(['/login']);
        return false;
    }
}
