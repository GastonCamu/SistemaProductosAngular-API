import { Component, Input, OnInit, inject } from '@angular/core';

import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder, FormGroup, ReactiveFormsModule,} from '@angular/forms';
import { ProductoService } from '../../services/producto.service';
import { Router } from '@angular/router';
import { Producto } from '../../models/producto';



@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.scss'
})
export class ProductoComponent implements OnInit {

  @Input('id')idProducto! : number;
  private productoService = inject(ProductoService);
  public formBuild = inject(FormBuilder);

  public formProducto:FormGroup = this.formBuild.group({
    descripcion: [''],
    precio: [0],
    stock: [0]
  });

  constructor(private router:Router) {}

  ngOnInit(): void {
    if(this.idProducto != 0){
      this.productoService.obtener(this.idProducto).subscribe({
        next:(data)=>{
          this.formProducto.patchValue({
            descripcion: data.data.descripcion,
            precio: data.data.precio,
            stock: data.data.stock
          })
        },
        error:(err)=>{
          console.log(err.message);
        }
      })
    }
  }

  guardar(){
    const objeto: Producto = {
      idProducto: this.idProducto,
      descripcion: this.formProducto.value.descripcion,
      precio: this.formProducto.value.precio,
      stock: this.formProducto.value.stock,
    }

    if(this.idProducto == 0){
      this.productoService.crear(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }
          else {
            alert("Error al crear");
          }
        },
        error:(err)=>{
          console.log(err.message);
        }
      });
    }
    else {
      this.productoService.editar(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }
          else {
            alert("Error al editar");
          }
        },
        error:(err)=>{
          console.log(err.message);
        }
      });
    };
  }

  volver(){
    this.router.navigate(["/"]);
  }
}
