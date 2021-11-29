using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;  
using System.Linq;  
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MODELS;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tool
{
    public class tool
    {

       public string token(IConfiguration _config,userModel user)
        {
                 var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));    
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
                        var Claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, user.USER_ID.ToString()),
                        };
                        var token = new JwtSecurityToken(_config["Jwt:Issuer"],    
                        _config["Jwt:Issuer"],    
                        Claims,    
                        // expires: DateTime.Now.AddDays(1),    
                        signingCredentials: credentials);    
                
                        return new JwtSecurityTokenHandler().WriteToken(token);    
        }

        public static string decryption(string token)
        {
        
          var handler = new JwtSecurityTokenHandler();
         var decryptiontoken = handler.ReadJwtToken(token);

           var claim= decryptiontoken.Claims.ToArray();
           return claim[0].Value;
           
        }
   
        //gửi mail
         public static void SendMessageSmtp ()
        {
        // Compose a message
        MimeMessage mail = new MimeMessage ();
        mail.From.Add (new MailboxAddress ("Excited Admin", "foo@caralta.games"));
        mail.To.Add (new MailboxAddress ("Excited User", "doremon209a@gmail.com"));
        mail.Subject = "Hello";
        mail.Body = new TextPart ("plain") {
            Text = @"Testing some Mailgun awesomesauce!",
        };

        // Send it!
        using (var client = new SmtpClient ()) {
            // XXX - Should this be a little different?
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            client.Connect ("smtp.mailgun.org", 587, false);
            client.AuthenticationMechanisms.Remove ("XOAUTH2");
            client.Authenticate ("postmaster@caralta.games", pass());

            client.Send (mail);
            client.Disconnect (true);
            }
         }
        //trick mail gun
        public static string pass() {
              string decode="ZTllZDY3Mzk3YjliMzk3MzU1NzAyNzBmMmY3NGRiZDQtYTBjZmI5NTctOTc2MDI0MGE=";
                var base64EncodedBytes = System.Convert.FromBase64String(decode);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
       
        
        public async Task<ActionResult> webSocket(QUESTION_REPLY obj,string Event) {
            var options = new PusherOptions
                {
                Cluster = "ap3",
                Encrypted = true
                };  
            string chanel ="notification";
            var pusher = new Pusher(
            "1243189",
            "609d6dc690cd8764da77",
            "075b8571e139675b39eb",
            options);
            var result = await pusher.TriggerAsync(
            chanel,
            obj.author_QUESTION_ID.ToString(),
            new { message = obj } );

            return null;
        }

        public static string GenerateAvata(userModel user)
        {   
            var nameSplit=user.USER_NAME.Split(' ');
            var _BackgroundColours = new List<string> { "B26126", "080808", "2c24bf", "008d99", "ff0000" };
            var avatarString ="";
            if(nameSplit.Length>=2)
             avatarString = string.Format("{0}{1}", nameSplit[nameSplit.Length-2][0], nameSplit[nameSplit.Length-1][0]).ToUpper();
            else
             avatarString = string.Format("{0}", nameSplit[0][0]).ToUpper();           
            var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
            var bgColour = _BackgroundColours[randomIndex];

            var bmp = new Bitmap(192, 192);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            var font = new Font("Arial", 48, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(bmp);

            graphics.Clear((Color)new ColorConverter().ConvertFromString("#" + bgColour));
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.DrawString(avatarString, font, new SolidBrush(Color.WhiteSmoke), new RectangleF(0, 0, 192, 192), sf);
            graphics.Flush();

            var ms = new MemoryStream();
            string Path="../backend/Resources/Images/AvataUser/"+user.slug+"";
            // if (Directory.Exists("../backend/Resources/Images/AvataUser/"+user.USER_ID))
            // {
            //     DirectoryInfo di = Directory.CreateDirectory(Path);
            // }
            DirectoryInfo di = Directory.CreateDirectory(Path);
            bmp.Save(Path+"/avata.png", ImageFormat.Png);

            return Path;
      }

    }
}
