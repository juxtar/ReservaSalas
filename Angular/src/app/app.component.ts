import { Component } from '@angular/core';
import { Router } from '@angular/router';

class MenuRoute {
    route: string;
    title: string;
    icon: string;
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    router: Router;
    routes: MenuRoute[] = [
        { route: '/salas', title: 'Salas', icon: 'home' },
        { route: '/reservas', title: 'Reservas', icon: 'assignment'}
    ]
    
    constructor(private _router: Router) {
        this.router = _router;
    }
}
