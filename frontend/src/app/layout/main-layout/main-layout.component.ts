import { Component, OnInit } from '@angular/core';
import { AppModule } from 'src/app/app.module';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: []
})
export class MainLayoutComponent implements OnInit {

  constructor(private appMain:AppModule) { 
    console.log("gọi main layout")
  }

  ngOnInit(): void { 
    
  }

 

}
