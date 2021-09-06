import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FutbolistasComponent } from './futbolistas/futbolistas.component';
import { FutbolistasFormComponent } from './futbolistas/futbolistas-form/futbolistas-form.component';
import { LoginComponent } from './login/login.component';
import { GuardpaginaService } from './servicios/guardpagina.service';
import { InterceptorService } from './servicios/interceptor.service'

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    FutbolistasComponent,
    FutbolistasFormComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent},
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'Futbolistas', component: FutbolistasComponent },
      { path: 'Futbolistas-crear', component: FutbolistasFormComponent, canActivate: [GuardpaginaService] },
      { path: 'Futbolistas-editar/:id', component: FutbolistasFormComponent },
      { path: 'Login', component: LoginComponent },
    ])
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
