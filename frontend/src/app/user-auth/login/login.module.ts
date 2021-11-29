import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from '../../layout/main-layout/main-layout.component';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { loginRoutingModule } from './login-routing.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    loginRoutingModule
  ]
})
export class LoginModule { }
