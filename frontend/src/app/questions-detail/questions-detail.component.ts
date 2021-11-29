import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs'; 
import { ActivatedRoute } from '@angular/router';
import { SystemService } from '../share/system/system.service';
import { UserService } from '../share/user/user.service';
import { CookieService } from 'ngx-cookie-service';
import {Title} from "@angular/platform-browser";
import $ from "jquery";
import { AngularDayjsService } from 'angular-dayjs';
import * as EmailValidator from 'email-validator';

declare var cuteToast: any;
@Component({
  selector: 'app-questions-detail',
  templateUrl: './questions-detail.component.html',
  styleUrls: ['./questions-detail.component.css']
})
export class QuestionsDetailComponent implements OnInit {
  private routeSub: Subscription;
  questionTitle; 
  questionBody;
  questionTag="";
  time;
  shareLink;
  author;
  comment;
  authorName;
  reply:any=[];
  log:boolean;
  mes;
  authorImage="";
  editasset;
  //user
  UserName;
  UserImage;
  mesEmail=null;
  constructor(private titleService:Title,public angularDayjsService: AngularDayjsService,private cookieService:CookieService,private route: ActivatedRoute,public system:SystemService,public service:UserService) {
   
  }
  private slug ;
  private id;
  ckConfig=this.system.ckConfig;
  public Editor = this.system.Editor;
 
  ngOnInit(): void {
    this.log= this.service.checkLogin();
    this.mes="You need <a href='/signin'>log in</a> to answer here"
    if(this.log)
    {
      this.mes="Your Answer";
      this.UserName=JSON.parse(this.cookieService.get('user')).useR_Name;
      this.UserImage=JSON.parse(this.cookieService.get('user')).useR_IMAGE;
    }
  
   
    this.routeSub = this.route.params.subscribe(params => {
      this.slug=params['id'];
      this.shareLink=location.href.replace(location.hash,"")
    });
    
    this.system.getAllQuestionDetail(this.slug).subscribe(
      (res:any) => {
        console.log(res)
        if(res===null)
        {
           
            window.location.href="404"
        }
        else
        {
          this.reply=res.item2;
          this.comment=res.item3;
          this.author=res.item1.author;
          this.authorName=res.item1.author_name
          this.authorImage=res.item1.author_image
           for (let i = 0; i < this.reply.length; i++) {
             const node =this.reply[i];
               node.listComent = [];
            for (let j = 0; j < this.comment.length; j++) {
                 const node_con = this.comment[j];
                 if (node_con.answer_REPLY_ID == node.answer_REPLY_ID) {
                      node.listComent.push(node_con);
                 } 
                }
          
           }

          this.id=res.item1.questioN_ID
        
          var tagList=res.item1.questioN_TAG.split(",",);
          tagList.forEach(tag => {
            this.questionTag+='<a href="#" class="tag-link">'+tag+'</a>';
          });
          this.questionBody=res.item1.questioN_DETAIL;
          this.questionTitle=res.item1.questioN_TITLE;
          this.time=res.item1.time;
          this.titleService.setTitle(this.questionTitle);
        }

      },
      err=>{
        cuteToast({
          type: "error",
          message: "Something went wrong,Try refresh the page",
          timer: 5000
        })
        console.log(err)
      }
    )
  }

  postAnswerSubmit()
  {

    this.service.formReply.value.token=JSON.parse(this.cookieService.get('user')).token
    this.service.formReply.value.answer_QUESTION_ID=this.id;
    this.service.formReply.value.author_QUESTION_ID=this.author
    this.service.postAnswer().subscribe(
      (res:any) => {
      location.reload();
       console.log(res)
      },
      err=>{
        cuteToast({
          type: "error",
          message: "Something went wrong,Try refresh the page",
          timer: 5000
        })
        console.log(err)
      }
    )
  
  }
  openeditasset()
  {
    this.editasset=true;
  }
  createAsset(email)
  {
    if(!EmailValidator.validate(email) )
      {
        this.mesEmail="email not vaild";
      }
    
  }
  closeeditasset()
  {
    this.editasset=false;
  }
  postComment(a)
  {
    this.service.formComment.value.answer_REPLY_ID=a;
    this.service.formComment.value.QUESTION_ID=this.id;
    this.service.formComment.value.token=JSON.parse(this.cookieService.get('user')).token
    this.service.formComment.value.author_QUESTION_ID=this.author
    this.service.postComment(a).subscribe(
      (res:any) => {
        location.reload();
        console.log(res)
      },
      err=>{
        cuteToast({
          type: "error",
          message: "Something went wrong,Try refresh the page",
          timer: 5000
        })
        console.log(err)
      }
    )
  }

}
