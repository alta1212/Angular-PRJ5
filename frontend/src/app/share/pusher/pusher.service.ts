import { Injectable } from '@angular/core';
//import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
declare const Pusher: any;
@Injectable({
  providedIn: 'root'
})
export class PusherService {
  pusher: any;
  channel: any;
  s:any;
  constructor(private http: HttpClient) { 
 
    this.pusher = new Pusher("609d6dc690cd8764da77", {
      cluster: "ap3",
      encrypted: true
    });
    this.channel = this.pusher.subscribe('notification');
   
  }

}
