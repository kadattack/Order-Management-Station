import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {EditComponent} from "./edit/edit.component";
import {CreateComponent} from "./create/create.component";

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'edit', component: EditComponent},
  {path: 'create', component: CreateComponent},
  {path:"**", component: HomeComponent, pathMatch:'full'},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
