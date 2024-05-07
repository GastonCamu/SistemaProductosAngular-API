import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { ProductoComponent } from './pages/producto/producto.component';

export const routes: Routes = [
    {path:'', component: InicioComponent},
    {path:'inicio', component: InicioComponent},
    {path:'producto/:id', component: ProductoComponent},
];
