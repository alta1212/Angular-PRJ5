import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {SettingComponent} from './setting.component';

const routes: Routes = [
  {
    path: 'setting',
    component: MainLayoutComponent,
    children: [
      { path: '', component: SettingComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class settingRoutingModule { }
