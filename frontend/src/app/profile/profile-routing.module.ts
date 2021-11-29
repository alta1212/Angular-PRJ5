import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {ProfileComponent} from './profile.component';
import { GuardsService } from '../guards/guards.service';
const routes: Routes = [
  {
    path: 'myProfile',
    component: MainLayoutComponent,
    children: [
      { path: '', component: ProfileComponent }
    ],
     canActivate:[GuardsService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class profileRoutingModule { }
