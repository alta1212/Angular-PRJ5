import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { LoginModule } from './login/login.module';
import { SignupComponent } from './signup/signup.component';
import { SignupModule } from './signup/signup.module';
import { FormGroup,ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent
  ],
  imports: [
    CommonModule,
    LoginModule,
    SignupModule,
    ReactiveFormsModule
  ],
  exports:[
    LoginComponent,
    LoginModule
  ]
})
export class UserAuthModule { }
