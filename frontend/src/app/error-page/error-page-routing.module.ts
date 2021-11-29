import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import { ErrorPageComponent } from './error-page.component';
const routes: Routes = [
  {
    path: '404',
    component: MainLayoutComponent,
    children: [
      { path: '', component: ErrorPageComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class errorpageRoutingModule { }
