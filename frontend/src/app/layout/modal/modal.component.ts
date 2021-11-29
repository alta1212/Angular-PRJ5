import { Component, OnInit } from '@angular/core';
declare var cuteToast: any;
import { UserService } from 'src/app/share/user/user.service';
import {CookieService} from 'ngx-cookie-service';
import { AppModule } from 'src/app/app.module';
@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  constructor(private appMain:AppModule,private cookieService: CookieService,public service: UserService) { }

  ngOnInit(): void {
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
        cuteToast({
          type: "error",
          message: "Something went wrong,Try refresh the page",
          timer: 5000
        })
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
        cuteToast({
          type: "error",
          message: "Something went wrong,Try refresh the page",
          timer: 5000
        })
        console.log(err);
      }
    );
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
    this.cookieService.deleteAll();
  }
}
