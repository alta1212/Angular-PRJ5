import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {ForgotPasswordComponent} from './forgot-password.component';

const routes: Routes = [
  {
    path: 'forgot',
    component: MainLayoutComponent,
    children: [
      { path: '', component: ForgotPasswordComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ForgotPasswordRoutingModule { }
