import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  intercept(req:HttpRequest<any>, next:HttpHandler): Observable<HttpEvent<any>>{
    var user=JSON.parse(localStorage.getItem('UsuarioGuardado'));
    if(!user){
      console.log("Interceptor sin usuario logueado")
      return next.handle(req);
    }
    const headers= req.clone({
      headers: req.headers.set('Authorization', `Bearer ${user["token"]}`)
    });
    console.log("Interceptor con usuario validado")
    return next.handle(headers);
  }
  constructor() { }
}
