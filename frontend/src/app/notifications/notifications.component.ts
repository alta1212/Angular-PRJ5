import { Component, OnInit } from '@angular/core';
import {Title} from "@angular/platform-browser";
import { UserService } from '../share/user/user.service';
import { SystemService } from '../share/system/system.service';
import { AngularDayjsService } from 'angular-dayjs';
@Component({
  selector: 'app-notication',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class notificationsComponent implements OnInit {
listnotication=[];
listnoticationlength;
ListNotipage;
  constructor(private titleService:Title,private service:UserService,public system:SystemService,public angularDayjsService: AngularDayjsService) {
    this.titleService.setTitle("Notifications");
  }

  ngOnInit(): void {
    this.service.getNOTICATION().subscribe(
      (res:any) => {
      console.log(res)
        this.listnotication=res;
        this.listnoticationlength=this.listnotication.length;
      },
      err => {
        console.log(err);
      }
    );
  }

}
