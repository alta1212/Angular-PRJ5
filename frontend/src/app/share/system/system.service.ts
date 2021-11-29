import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { CanActivate,Router } from '@angular/router';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import * as code from "../ckeditor/build/ckEditor";

@Injectable({
  providedIn: 'root'
})
export class SystemService {

  constructor(private http: HttpClient,private cookieService:CookieService) { }
  //api url
  readonly BaseURI="https://localhost:5001/";
  //end api url
  getAllCategory()
  {
    // return this.http.get(this.BaseURI+"user/check2fa",
    // { responseType: 'text' });
    return this.http.get(this.BaseURI+"Category/getAllCategory",{responseType: 'json'});
  }
  getAllQuestionDetail(id)
  {
    var body={
     
    };
    if(this.cookieService.get('user'))
    {
      body ={
        "token":JSON.parse(this.cookieService.get('user')).token
      }
    }
   
    return this.http.post(this.BaseURI+"system/questionDetail/"+id,body,{responseType: 'json'});
  }

  GetAllQuestion()
  {
    return this.http.get(this.BaseURI+"system/question/",{responseType: 'json'});
  }
  ckConfig={
    toolbar: [ 'heading', 
    "Autoformat",
    "BlockQuote",
    "Bold",
    "Code",
    "CodeBlock",
    "Essentials",
    "Indent",
    "Italic",
    "Link",
    "List",
    "MediaEmbed",
    "Paragraph",
    "PasteFromOffice",
    "Table",
    "TableToolbar",
    "TextTransformation" ],
   
    // This value must be kept in sync with the language defined in webpack.config.js.
    language: 'vi'
}
  public Editor = code;

}
