import { Injectable, Inject, inject } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Iuser} from '../interfaces/iuser'
import { logging } from 'selenium-webdriver';

@Injectable({
  providedIn: 'root'
})
export class AutenticacionService {

  public usuarioActual:Observable<Iuser>;
  private apiUrl= this.baseUrl + 'api/login'
  constructor(private Http: HttpClient, @Inject('BASE_URL') private baseUrl) {

   }

   login(userName:string, password:string){

    const usuarioApi: Iuser= {
        userName:userName,
        password:password,
        fullName:"",
        token:""
    };

    return this.Http.post<any>(this.apiUrl, usuarioApi)
            .pipe(
              map(respuesta=>{
                console.log("Respuesta Api: ",respuesta);
                localStorage.setItem("UsuarioGuardado", JSON.stringify(respuesta));
                return respuesta;
              })
            );
   }

   estaLogueado(){
    var logueado=false;
    var user= JSON.parse(localStorage.getItem("UsuarioGuardado"));
    if (user){
      const token = user["token"];
      console.log(token);
      logueado=true;
    }
    return logueado;
  }

  logout(){
    localStorage.removeItem("UsuarioGuardado")
  }

}

