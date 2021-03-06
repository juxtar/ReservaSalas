import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, RequestOptions } from '@angular/http';

import { CovalentCoreModule } from '@covalent/core';
import { CovalentDynamicFormsModule } from '@covalent/dynamic-forms';
import { Md2Module } from 'md2';
import { MomentModule } from 'angular2-moment';
import 'moment/locale/es';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SalasComponent } from './salas/salas.component';
import { ReservasComponent } from './reservas/reservas.component';
import { SalaDetailComponent } from './sala-detail/sala-detail.component';
import { LoginComponent } from './login/login.component';
import { AuthenticationService, SalasService } from './_services';
import { ReservasService } from './_services/reservas.service';
import { AuthGuard } from './_guards';
import { SalaFormularioComponent } from './sala-formulario/sala-formulario.component';
import { ReservaFormularioComponent } from './reserva-formulario/reserva-formulario.component';
import { ReservaDetailComponent } from './reserva-detail/reserva-detail.component';
import { AccountComponent } from './account/account.component';
import { EncuestasComponent } from './encuestas/encuestas.component';
import { EncuestaFormularioComponent } from './encuesta-formulario/encuesta-formulario.component';
import { EqualValidator } from './_utils/validate-equal.directive';
import { RegistroComponent } from './registro/registro.component';

@NgModule({
  declarations: [
    AppComponent,
    SalasComponent,
    ReservasComponent,
    SalaDetailComponent,
    LoginComponent,
    SalaFormularioComponent,
    ReservaFormularioComponent,
    ReservaDetailComponent,
    AccountComponent,
    EncuestasComponent,
    EncuestaFormularioComponent,
    EqualValidator,
    RegistroComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    CovalentCoreModule.forRoot(),
    CovalentDynamicFormsModule.forRoot(),
    Md2Module.forRoot(),
    MomentModule
  ],
  providers: [
    AuthGuard,
    AuthenticationService,
    SalasService,
    ReservasService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
