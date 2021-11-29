import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { FooterComponent } from './footer/footer.component';
import { ModalComponent } from './modal/modal.component';
import { RouterModule } from '@angular/router';
import { AccountBarComponent } from './account-bar/account-bar.component';
import { FormGroup,ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HeaderComponent,
    MainLayoutComponent,
    FooterComponent,
    ModalComponent,
    AccountBarComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([]),
    ReactiveFormsModule
  ],
  exports:[
    MainLayoutComponent
  ]
})
export class LayoutModule { }
