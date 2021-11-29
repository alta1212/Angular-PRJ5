using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using System.Text;
using Slugify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Google.Authenticator;
using MODELS;
using Tool;
using BUS.Interface;
using Newtonsoft.Json;
namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class systemController: ControllerBase
    {
     private Isystem isystemBUS;

      private IConfiguration config;
      public systemController (Isystem k,IConfiguration _config)
      {
        config=_config;
        isystemBUS=k;
      }
        
      [HttpPost]
      [Route("questionDetail/{slug}")]
      public object Deatail(string slug,QUESTION_Accset a)
      {  
         
          int userid=int.MaxValue;
          if(a.token!=null)
          {
            userid=int.Parse(tool.decryption(a.token));
          }
          
          return isystemBUS.getDetailQuestion(slug,userid);
      }

      [HttpGet]
      [Route("question")]
      public object Deatail()
      {
          return isystemBUS.getQuestion();
      }

      [HttpPost]
      [Route("getnoti")]
      public object getnoti(notication_model noti)
      {
       
          return isystemBUS.getNotication(tool.decryption(noti.token));
      }
    }
}