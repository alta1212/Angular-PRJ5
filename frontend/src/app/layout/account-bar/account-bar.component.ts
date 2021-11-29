import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/share/user/user.service';
import { CookieService } from 'ngx-cookie-service';
import { PusherService } from 'src/app/share/pusher/pusher.service';
import {SystemService} from '../../share/system/system.service'
import { AngularDayjsService } from 'angular-dayjs';
declare var cuteToast: any;

@Component({
  selector: 'app-account-bar',
  templateUrl: './account-bar.component.html',
  styleUrls: ['./account-bar.component.css']
})
export class AccountBarComponent implements OnInit {
  user_name :any;
  user_image;
  listnoti=[];

  constructor(public sys:SystemService,private cookieService:CookieService,public service: UserService,private pusherService: PusherService,public angularDayjsService: AngularDayjsService) { }
  log:boolean;
  
  ngOnInit(): void {

    
    this.log= this.service.checkLogin();
    if(this.log)
    {
      this.user_name=JSON.parse(this.cookieService.get('user')).useR_NAME;
      this.user_image=JSON.parse(this.cookieService.get('user')).useR_IMAGE
      this.pusherService.channel.bind(String(JSON.parse(this.cookieService.get('user')).useR_ID), data => {
        console.log(data.message.answer_author_name);
        cuteToast({
          type: "info",
          message: data.message.answer_author_name + " Reply on your question ",
          timer: 5000
        });
      });
    
      this.service.getNOTICATION().subscribe(
        (res:any) => {
        console.log(res)
          this.listnoti=res;
        },
        err => {
          console.log(err);
        }
      );
    }
  }
  logout()
  {
    this.service.logout();
  }
 

 
}
