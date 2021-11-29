import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {QuestionsDetailComponent} from './questions-detail.component';

const routes: Routes = [
  {
    path: 'question',
    component: MainLayoutComponent,
    children: [
      { path: ':id', component: QuestionsDetailComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuestionsDetailRoutingModule { }
