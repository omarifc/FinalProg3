import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms'

import {AutenticacionService} from '../servicios/autenticacion.service'
import {Iuser} from '../interfaces/iuser'
import {Router, ActivatedRoute} from '@angular/router'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup;
  returnUrl:string;

  constructor(
    private formBuilder:FormBuilder,
    private authService:AutenticacionService,
    private router: Router,
    private ruta: ActivatedRoute
  ) { }

  ngOnInit() {
    if(this.authService.estaLogueado){
      this.authService.logout;
    }
    this.loginForm=this.formBuilder.group(
      {
        userName:"",
        password:""
      }
    );
    this.returnUrl=this.ruta.snapshot.queryParams['returnUrl'] || '/';
  }

  Loguear(){
    let usuarioFormulario: Iuser=Object.assign({}, this.loginForm.value);

    this.authService.login(usuarioFormulario.userName, usuarioFormulario.password)
    .subscribe(
      resultado=>{
                  console.log(resultado);
                  this.router.navigate([this.returnUrl])

      },
      error=>console.log("Error" + error),
    )
  };
}
