import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalasComponent } from '../salas/salas.component';
import { ReservasComponent } from '../reservas/reservas.component';

const routes: Routes = [
    { path: '', redirectTo: '/salas', pathMatch: 'full' },
    { path: 'salas', component: SalasComponent },
    { path: 'reservas', component: ReservasComponent }
];

@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule ]
})
export class AppRoutingModule { }
