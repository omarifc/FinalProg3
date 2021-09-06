import { Injectable, Inject } from '@angular/core';
import { IFutbolista } from '../interfaces/ifutbolista';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { inject } from '@angular/core/testing';

@Injectable({
  providedIn: 'root'
})
export class FutbolistasService {

  public listaFutbolistas: IFutbolista[];
  private apiUrl: string = "https://localhost:5001/" + "api/futbolistas";

  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) { }

  MostrarTodos(): Observable<IFutbolista[]> {
    return this.http.get<IFutbolista[]>(this.apiUrl);
  }

  MostrarPorId(Id: number) {
    return this.http.get<IFutbolista>(this.apiUrl + "/" + Id);
  }

  Borrar(Id: number): Observable<any> {
    const headers = {
      'Accept': 'application/json'
    }
    return this.http.delete<any>(this.apiUrl + "/" + Id, { headers });
  }

  //Revisar!
  Buscar(nombreBuscado: string, equipoBuscado: string): Observable<IFutbolista[]> {
    console.log("Nombre" + nombreBuscado);
    console.log("Equipo" + equipoBuscado);
    const headers = {
      'Accept': 'application/json',
      'Content-Type': 'application/Json',
      'Access-Control-Allow-Origin': "https://localhost:5001",
      'Access-Control-Allow-Methods': 'POST, PUT, GET, DELETE',
    }
    return this.http.get<IFutbolista[]>(this.apiUrl + '/busqueda/' + nombreBuscado + '/equipo/' + equipoBuscado, { headers })
  }

  Crear(fut: IFutbolista): Observable<void> {
    const headers = {
      'Accept': 'application/json',
      'Content-Type': 'application/Json',
      'Access-Control-Allow-Origin': "https://localhost:5001",
      'Access-Control-Allow-Methods': 'POST, PUT, GET, DELETE',
    }
    return this.http.post<void>(this.apiUrl, fut, { headers })
  }

  Actualizar(fut: IFutbolista): Observable<boolean> {
    fut.futbolistaId=+fut.futbolistaId; // conversión a numérico, por si llega un string
    return this.http.put<boolean>(this.apiUrl + '/' + fut.futbolistaId, fut);
  }

}
