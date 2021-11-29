import { Injectable } from '@angular/core';
import { CanActivate,Router } from '@angular/router';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
@Injectable({
  providedIn: 'root'
})
export class GuardsService implements CanActivate {

  constructor(private router:Router,private jwtSevice:JwtHelperService,private cookieService:CookieService) { }
  canActivate()
  {
    //token
      if(!this.cookieService.get('user'))
      {
         window.location.href = '/signin'; 
         return false;
      }
     

      var token=JSON.parse(this.cookieService.get('user')).token;
      if(token && !this.jwtSevice.isTokenExpired(token))
      {
      
        return true;
      }
      window.location.href = '/signin';
      return false;
  }

}
