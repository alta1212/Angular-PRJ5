import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from 'src/app/layout/main-layout/main-layout.component';
import {notificationsComponent} from './notifications.component';
import { GuardsService } from '../guards/guards.service';
const routes: Routes = [
  {
    path: 'notifications',
    component: MainLayoutComponent,
    children: [
      { path: '', component: notificationsComponent }
    ],
    canActivate:[GuardsService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class notificationsnRoutingModule { }
