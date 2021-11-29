import { Component, OnInit } from '@angular/core';
import {Title} from "@angular/platform-browser";
import { UserService } from 'src/app/share/user/user.service';
import { CookieService } from 'ngx-cookie-service';
declare var cuteAlert: any;
@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {
  checkboxValue: any;
  constructor(private titleService:Title,public service: UserService,private cookie:CookieService) {
    this.titleService.setTitle("Seting");
  }
  Enable2fa;
 
  ngOnInit(): void {
    this.Enable2fa=JSON.parse(this.cookie.get('user')).tfa;
  }
  //#region 2fa
  //-------------------------------start 2fa-----------------------------------------------
  showFa=false; 
  DeactivateTfa=false;
  imgSrc=""; 
  twofaopen()
  { 
    this.showFa=true;
    this.imgSrc="../../../assets/images/loading.gif"; 
    this.service.getQr().subscribe(
      (res:string) => {
        this.imgSrc=res; 
      },
      err => {
        console.log(err);
      }
    );
   
    
  }


  closeTwoFaForm()
  {
    this.showFa=false;
  }

  verity2faCode(code)
  {
    this.service.enableTwoFaCode(code).subscribe(
      (res) => {
       console.log(res)
        if(res=="true")
        {
          this.cookie.deleteAll();
          cuteAlert({
            type: "success",
            title: "success",
            message: "2Fa enabled successfully,please login again",
            buttonText: "Okay"
          }).then(() => {
            window.location.href="/signin"
          })
        
        }
        else
        {
          cuteAlert({
            type: "error",
            title: "Who TF are you?",
            message: "2fa Code invalid",
            buttonText: "Okay"
          })
        }
      },
      err => {
        console.log(err);
      }
    );
  }
  //-------------------------------------------end 2fa
  //Deactivate 2fa
  Deactivate()
  {
    this.DeactivateTfa=true;
  }
  closeDeactivateTwoFaForm()
  {
    this.DeactivateTfa=false;
  }
  verityDeactivate2faCode(s)
  {
   this.service.disableTwoFaCode(s).subscribe(
    (res) => {
      if(res=="true")
      {
        this.cookie.deleteAll();
        cuteAlert({
          type: "success",
          title: "success",
          message: "2Fa disable successfully,please login again",
          buttonText: "Okay"
        }).then(() => {
          window.location.href="/signin"
        })
      }
      else
      {
        cuteAlert({
          type: "error",
          title: "Error Title",
          message: "2fa Code invalid",
          buttonText: "Okay"
        })
      }
    }
    ,
      err => {
        console.log(err);
      }
   )
  }
  //endDeactivate 2fa
  //#endregion 2fa

  
}
