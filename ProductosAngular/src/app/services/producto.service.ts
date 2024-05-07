import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../settings/appsettings';
import { Producto } from '../models/producto';
import { ResponseAPI } from '../models/responseAPI';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private http = inject(HttpClient);
  private apiUrl: string = appsettings.apiUrl + "Producto";

  constructor() { }

  lista() {
    return this.http.get<ResponseAPI>(this.apiUrl);
  }

  obtener(id:number) {
    return this.http.get<ResponseAPI>(`${this.apiUrl}/${id}`);
  }

  crear(objeto:Producto){
    return this.http.post<ResponseAPI>(this.apiUrl, objeto);
  }

  editar(objeto:Producto){
    return this.http.put<ResponseAPI>(this.apiUrl, objeto);
  }

  eliminar(id:number) {
    return this.http.delete<ResponseAPI>(`${this.apiUrl}/${id}`);
  }
}
