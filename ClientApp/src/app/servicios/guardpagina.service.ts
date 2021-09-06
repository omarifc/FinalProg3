import { Injectable } from '@angular/core';
import {Router, CanActivate} from '@angular/router';
import {AutenticacionService} from '../servicios/autenticacion.service';

@Injectable({
  providedIn: 'root'
})
export class GuardpaginaService implements CanActivate{

  constructor(private srvAutenticacion:AutenticacionService, private router: Router) { }

  canActivate(){
    if(!this.srvAutenticacion.estaLogueado()){
      console.log("No está logueado");
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}
