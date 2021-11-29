import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {AskQuestionComponent} from './ask-question.component';
import { GuardsService } from '../guards/guards.service';
const routes: Routes = [
  {
    path: 'ask',
    component: MainLayoutComponent,
    children: [
      { path: '', component: AskQuestionComponent }
    ],
    canActivate:[GuardsService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class askquestionsRoutingModule { }
