import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {QuestionsComponent} from './questions.component';

const routes: Routes = [
  {
    path: 'questions',
    component: MainLayoutComponent,
    children: [
      { path: '', component: QuestionsComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class questionsRoutingModule { }
