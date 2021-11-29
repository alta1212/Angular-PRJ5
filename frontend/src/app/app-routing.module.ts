import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserAuthModule } from './/user-auth/user-auth.module';
import { LoginComponent } from './user-auth/login/login.component';
import { LayoutModule } from './layout/layout.module';
import { GuardsService } from './guards/guards.service';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';

const routes: Routes = [{
  path: 'not@123982173ad',
  component: MainLayoutComponent
} ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
