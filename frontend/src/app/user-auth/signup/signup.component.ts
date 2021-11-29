import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { UserService } from 'src/app/share/user/user.service';
import { GuardsService } from '../../guards/guards.service';
import { Routes, RouterModule } from '@angular/router';
import { AppModule } from 'src/app/app.module';
declare var cuteToast: any;
//declare var window: any;
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

    constructor(private appMain:AppModule,private titleService:Title,public service: UserService) {
    this.titleService.setTitle("Sign up");
    
  }

  ngOnInit(): void {
    if(this.service.checkLogin())
    {
      window.location.href="/";
    }
      console.log(this.service.checkLogin())
  }
  formregSubmit()
  {
  
    this.service.register().subscribe(
      (res) => {    
        
        cuteToast({
          type: "success",
          message: "Successful Registration",
          timer: 5000
        });
   
        window.location.href = '/signin';
        console.log(res)
      },
      err => {
        console.log(err);
      }
    );
  }

  logout()
  {
    
  }
}
