import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services/authentication.service';

class MenuRoute {
    route: string;
    title: string;
    icon: string;
    auth: boolean;
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    _routes: MenuRoute[] = [
        { route: '/salas', title: 'Salas', icon: 'home', auth: true},
        { route: '/reservas', title: 'Reservas', icon: 'assignment', auth: false },
        { route: '/encuestas', title: 'Encuestas', icon: 'thumbs_up_down', auth: true },
        { route: '/account', title: 'Mi Cuenta', icon: 'account_circle', auth: true },
        { route: '/login', title: 'Cerrar sesión', icon: 'power_settings_new', auth: true },
    ]

    _login: MenuRoute = 
        { route: '/login', title: 'Iniciar sesión',
          icon: 'input', auth: false }
    
    constructor(
        private router: Router,
        private auth: AuthenticationService
    ) { }

    get routes(): MenuRoute[] {
        if (this.auth.isAuthenticated())
            return this._routes;
        else
            return this._routes.filter(value => !value.auth)
                .concat([this._login]);
    }
}
