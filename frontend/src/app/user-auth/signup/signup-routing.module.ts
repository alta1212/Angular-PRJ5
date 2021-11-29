import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {SignupComponent} from './signup.component';

const routes: Routes = [
  {
    path: 'signup',
    component: MainLayoutComponent,
    children: [
      { path: '', component: SignupComponent }
    ]
    
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class signupRoutingModule { }
