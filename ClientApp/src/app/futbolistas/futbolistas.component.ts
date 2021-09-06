import { Component, OnInit } from '@angular/core';
import { FutbolistasService } from '../servicios/futbolistas.service';
import { IFutbolista } from '../interfaces/ifutbolista';

@Component({
  selector: 'app-futbolistas',
  templateUrl: './futbolistas.component.html',
  styleUrls: ['./futbolistas.component.css']
})
export class FutbolistasComponent implements OnInit {

  public listadoFutbolistas: IFutbolista[];
  public campoBuscado: string;

  constructor(private servicio: FutbolistasService) { }

  ngOnInit() {
    this.mostrarListado();
  }

  //llama al servicio
  mostrarListado() {
    this.servicio.MostrarTodos()
      .subscribe(
        resultado => {
          console.log(resultado);
          this.listadoFutbolistas = resultado;
        },
        () => console.log("Terminó de listar")
      );
  }

  borrar(Id: number) {
    this.servicio.Borrar(Id)
      .subscribe(
        resultado => console.dir(resultado),
        error => console.log("Error al borrar" + error),
        () => this.mostrarListado(),
      );
  }

  Buscar() {
    if (this.campoBuscado) {
      this.servicio.Buscar(this.campoBuscado, this.campoBuscado)
        .subscribe(
          resultado => {
            console.log("Resultado de la búsqueda:");
            console.table(resultado);
            this.listadoFutbolistas = resultado;
          },
          error => console.log(error),
          () => console.log("busqueda")
        );
    }
    else {
      alert("Debe ingresar un campo a buscar");
    }
  }

  Crear() {
    var futbolistaNuevo: IFutbolista;
    futbolistaNuevo = {

      futbolistaId: 0,
      nombre: "Franco Soldano",
      edad: 26,
      pais: "Argentina",
      descripcion: "Futbolista profesional del primer equipo, habilitado para competencias internacionales",
      goles: "4",
      equipo: "Boca",
    };
    this.servicio.Crear(futbolistaNuevo)
      .subscribe(
        () => this.mostrarListado(),
        error => console.log(error)
      )
  }

  Modificar(fId: number) {
    var futbolistaModificado: IFutbolista;
    futbolistaModificado = {
      futbolistaId: fId,
      nombre: "Franco Soldano",
      edad: 28,
      pais: "Argentina",
      descripcion: "dsdf",
      goles: "200",
      equipo: "Union",
    };
    this.servicio.Actualizar(futbolistaModificado)
      .subscribe(
        resultado => alert("Actualizado: " + resultado),
        error => console.log(error),
        () => this.mostrarListado(),
      );
  }
}
