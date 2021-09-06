import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {IFutbolista} from '../../interfaces/ifutbolista';
import {FutbolistasService} from '../../servicios/futbolistas.service'
import {Router, ActivatedRoute} from '@angular/router'

@Component({
  selector: 'app-futbolistas-form',
  templateUrl: './futbolistas-form.component.html',
  styleUrls: ['./futbolistas-form.component.css']
})
export class FutbolistasFormComponent implements OnInit {

  formFutbolistas: FormGroup;
  futbolistaId: number;
  modoEdicion: boolean = false;

  constructor(private fb:FormBuilder,
    private router: Router,
    private ActivatedRoute: ActivatedRoute,
    private futbolistaService: FutbolistasService) {

      this.ActivatedRoute.params.subscribe(
        params=>{ 
          this.futbolistaId=params['id'];

          if (isNaN(this.futbolistaId))
          {
            return;
          }
          else{
            this.modoEdicion=true;
            this.futbolistaService.MostrarPorId(this.futbolistaId)
            .subscribe(
              resultado=> this.cargarFormulario(resultado),
              error=>console.error(error)
            );
          }
      }
    )
  }

  ngOnInit() {
    this.formFutbolistas=this.fb.group(
    {
      nombre: '',
      edad: '',
      descripcion: '',
      pais: '',
      goles:'',
      equipo:''
    }
    )
  }

  cargarFormulario(futbolista: IFutbolista){
    console.dir(futbolista);
    this.formFutbolistas.patchValue({

      nombre: futbolista.nombre,
      edad: futbolista.edad,
      descripcion: futbolista.descripcion,
      equipo: futbolista.equipo,
      goles: futbolista.goles,
      pais: futbolista.pais

    });
  }

  save(){
    let futbolistaFormulario: IFutbolista = Object.assign({}, this.formFutbolistas.value)
    console.dir(futbolistaFormulario);

    if(this.modoEdicion)
    {     
      futbolistaFormulario.futbolistaId=+this.futbolistaId;
      this.futbolistaService.Actualizar(futbolistaFormulario)
      .subscribe(
        resultado=>alert("Modificado correctamente" + resultado),
        error=>alert(error)
      );
    }
    else
    {
      var futbolistaNuevo : IFutbolista;
      futbolistaNuevo={
        futbolistaId: 1,      
        nombre: futbolistaFormulario.nombre,
        edad: futbolistaFormulario.edad,
        pais: futbolistaFormulario.pais,
        descripcion: futbolistaFormulario.descripcion,
        goles: futbolistaFormulario.goles,
        equipo: futbolistaFormulario.equipo,
      };
      this.futbolistaService.Crear(futbolistaFormulario)
      
      .subscribe(
        ()=>alert("Agregado correctamente"),
        error=>{alert("Error al crear" + error)
        console.dir(error)}
      );
    }
  }

}
