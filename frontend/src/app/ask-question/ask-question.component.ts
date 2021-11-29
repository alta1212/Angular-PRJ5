import { Component, OnInit } from '@angular/core';
import {Title} from "@angular/platform-browser";
import { UserService  } from '../share/user//user.service';
import { SystemService } from '../share/system/system.service';

@Component({
  selector: 'app-ask-question',
  templateUrl: './ask-question.component.html',
  styleUrls: ['./ask-question.component.css']
})
export class AskQuestionComponent implements OnInit {

  constructor(private titleService:Title,public service:UserService,public systemSys:SystemService) {
    this.titleService.setTitle("Questions");
  }

  ckConfig=this.systemSys.ckConfig;
  public Editor = this.systemSys.Editor;

  ngOnInit(): void {
     this.systemSys.getAllCategory().subscribe(
      (res:any) => {
        console.log(res)
        res.forEach(element => {
            var object = {
              label: element.categorY_NAME,
              value:  element.categorY_ID,
              code : element.slugs
            }
            this.CategoryOptions.push(object);
            
        });
 
      },
      err => {
        console.log(err);
      }
    );
  }
  postQuestionSubmit()
  {
    this.service.postQuestion().subscribe(
      (res:any) => {
        console.log(res)
        JSON.parse(JSON.stringify((<any>res)))
       window.location.href="/question/"+res.slugs
      },
      err => {
        console.log(err);
      }
    );
  }
  tag=[];
  tagPlaceholer="e.g. javascript"
  config= {
    create: true,
    labelField: 'label',
    valueField: 'value',
    maxItems: 10
  }

  

	CategoryConfig= {
    labelField: 'label',
    valueField: 'value',
    searchField: ['label']
  }
  CategoryOptions =[];



}
