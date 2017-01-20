import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, RequestOptions } from '@angular/http';

import { CovalentCoreModule } from '@covalent/core';
import { CovalentDynamicFormsModule } from '@covalent/dynamic-forms';

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

@NgModule({
  declarations: [
    AppComponent,
    SalasComponent,
    ReservasComponent,
    SalaDetailComponent,
    LoginComponent,
    SalaFormularioComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    CovalentCoreModule.forRoot(),
    CovalentDynamicFormsModule.forRoot()
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
