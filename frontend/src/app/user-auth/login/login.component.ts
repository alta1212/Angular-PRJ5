import { Component, OnInit } from '@angular/core';
import {Title} from "@angular/platform-browser";
import { UserService } from 'src/app/share/user/user.service';
import {CookieService} from 'ngx-cookie-service';
import { AppModule } from 'src/app/app.module';
declare var cuteToast: any;

//https://www.youtube.com/watch?v=NSQHiIAP7Z8&ab_channel=CodeMaze
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invaildLogin:boolean;
  constructor(private appMain:AppModule,private cookieService: CookieService,private titleService:Title,public service: UserService) {
    this.titleService.setTitle("Sign in");
    
  }

  ngOnInit(): void {
    if(this.service.checkLogin())
    {
      window.location.href="/";
    }
  }

  TFA=false;
  verity2faCode(code)
  {
    console.log(code)
    this.service.checkTwoFaCode(code).subscribe(
      (res) => {
        if(res!==null)
        {
          this.cookieService.set("user",JSON.parse(JSON.stringify((<any>res))))
          cuteToast({
            type: "success",
            message: "Successful login",
            timer: 5000
          })
          window.location.href="/";
         
        }
        else{
          cuteToast({
            type: "error",
            message: "2fa Code invalid",
            timer: 5000
          })
        }
        
      },
      err => {
        console.log(err);
      }
    );
  }

  loginSubmit()
  {
    
    this.service.login().subscribe(
      (res) => {    
         
        if(res!==null && (<any>res).token!==null)//đăng nhập k có 2fa
        {
          cuteToast({
            type: "success",
            message: "Successful login",
            timer: 5000
          })
         
          this.cookieService.set("user",JSON.stringify((<any>res)))
          window.location.href="/";
        }
        //có 2fa
        else if(res!==null)
        {
          this.TFA=true;
         
          this.cookieService.set("user",JSON.stringify((<any>res)))
          console.log(JSON.parse(this.cookieService.get('user')))
        }
        else
        {
          cuteToast({
            type: "error",
            message: "invalid email or password",
            timer: 5000
          })
        }
        console.log(res);
       
      },
      err => {
        console.log(err);
      }
    );
  }


  
  logout()
  {
    this.cookieService.deleteAll();
  }
}
