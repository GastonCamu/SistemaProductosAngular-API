import { Component, inject } from '@angular/core';
import { ProductoService } from '../../services/producto.service';
import { Producto } from '../../models/producto';
import { Router } from '@angular/router';

import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.scss'
})
export class InicioComponent {

  private productoServicio = inject(ProductoService);
  public listaProductos:Producto[] = [];
  public displayedColumns: string[] = ['descripcion','precio','stock','accion'];

  obtenerProductos(){
    this.productoServicio.lista().subscribe({
      next:(data)=>{
        if(data.data.length > 0){
          this.listaProductos = data.data;
        }
      },
      error:(err)=>{
        console.log(err.message);
      }
    })
  }

  constructor(private router:Router){
    this.obtenerProductos();
  }

  nuevo(){
    this.router.navigate(['/producto',0]);
  }

  editar(objeto:Producto){
    this.router.navigate(['/producto',objeto.idProducto]);
  }

  eliminar(objeto:Producto){
    if(confirm("Desea eliminar el producto: " + objeto.descripcion)) {
      this.productoServicio.eliminar(objeto.idProducto).subscribe({
        next:(data)=>{
          if(data.isSuccess) {
            this.obtenerProductos();
          }else{
            alert("no se pudo eliminar");
          }
        },
        error:(err)=>{
          console.log(err.message);
        }
      })
    }
  }
}
