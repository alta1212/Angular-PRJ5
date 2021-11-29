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
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

namespace backend.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class userController : ControllerBase
    {
        private IuserBUS iuserBUS;

        private IConfiguration config;
        public userController(IuserBUS k, IConfiguration _config)
        {
            config = _config;
            iuserBUS = k;
        }

        [HttpGet]
        [Route("testmail")]
        public string s()
        {
            tool.SendMessageSmtp();
            return "ok";
        }
        // [HttpGet]
        // [Authorize(Roles = "login")]
        // [Route("testau")]
        // public string s()
        // {
        //   return "sâ";
        // }

        Tool.tool tool = new Tool.tool();
        SlugHelper helper = new SlugHelper();
        #region 2fa


        //sau này sẽ cho vào seesion
        private const string key = "Alta122@";

        static userModel User_;

        [Route("getQr2fa")]
        [HttpPost]
        public object create2FaQrCode(userModel user)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = user.USER_ID + key;
            //   var hash =System.Text.Encoding.UTF8.GetBytes(UserUniqueKey);

            //  return System.Text.Encoding.UTF8.GetString(hash);
            // k=UserUniqueKey;//cho vào session để  so sánh
            var setupInfo = tfa.GenerateSetupCode("Disilab", user.USER_EMAIL, UserUniqueKey, false, 10);
            return setupInfo.QrCodeSetupImageUrl;

        }
        [Route("check2fa")]
        [HttpPost]
        public userModel verify2Fa(twofaVerifyCode code)
        {
            if (code.token != null)
            {
                var idUser = tool.decryption(code.token);
                code.UserUniqueKey = idUser;
            }
            else
            {

            }

            if (check2fa(code))
            {

                User_.token = tool.token(config, User_);
                User_.USER_PASSWORD = null;
                return User_;
            }
            return null;
        }

        [Route("enable2fa")]
        [HttpPost]
        public bool enable2fa(twofaVerifyCode code)
        {
            var idUser = tool.decryption(code.token);
            code.UserUniqueKey = idUser;
            if (check2fa(code))
            {
                return iuserBUS.enable2fa(tool.decryption(code.token));
            }
            return false;
        }
        [Route("disable2fa")]
        [HttpPost]
        public bool disable2fa(twofaVerifyCode code)
        {
            var idUser = tool.decryption(code.token);
            code.UserUniqueKey = idUser;
            if (check2fa(code))
            {
                return iuserBUS.disable2fa(idUser);
            }
            return false;
        }
        public bool check2fa(twofaVerifyCode code)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

            return tfa.ValidateTwoFactorPIN(code.UserUniqueKey + key, code.code);

        }
        #endregion 2fa
        #region auth
        [HttpPost]
        [Route("login")]
        public object login(userModel user)
        {
            user = iuserBUS.login(user);
            if (user != null)
            {
                if (iuserBUS.login(user).Tfa == "0")//kiếm tra nếu người dùng bật 2fa
                {
                    user.USER_PASSWORD = null;
                    user.token = tool.token(config, user);
                    return user;
                }
                User_ = user;
                return user;//không tạo token truy cập đến khi xác thực đƯợc 2fa
            }
            return null;



        }
        [HttpPost]
        [Route("signup")]
        public string signup(userModel user)
        {
            double ms = Math.Round((DateTime.Now - DateTime.MinValue).TotalMilliseconds);
            user.slug = helper.GenerateSlug(user.USER_NAME) + "-" + ms;
            user.USER_IMAGE="Resources/Images/AvataUser/"+user.slug+"/avata.png";
            tool.GenerateAvata(user);
            iuserBUS.signUp(user);
            return user.USER_IMAGE;

        }
        #endregion auth
        #region question

        //question

        [HttpPost]
        [Route("ask/{token}")]
        public object ask(questionModal ques, string token)
        {
            double ms = Math.Round((DateTime.Now - DateTime.MinValue).TotalMilliseconds);
            ques.author = int.Parse(tool.decryption(token));
            ques.SLUGS = helper.GenerateSlug(ques.QUESTION_TITLE) + "-" + ms;
            return iuserBUS.ask(ques);
        }
         [HttpPost]
        [Route("CreateAsset")]
        public object CreateAsset(object data)
        {
            object json =JsonConvert.DeserializeObject(data.ToString());
            var datajson=(JToken)JsonConvert.DeserializeObject(json.ToString());
            string qID=datajson["qid"].Value<object>().ToString();
            string Usermail=datajson["email"].Value<object>().ToString();
            return iuserBUS.CreateAsset(qID,Usermail);
       
        }




        #endregion question

        #region answer
        [HttpPost]
        [Route("answer")]
        public QUESTION_REPLY REPLY(QUESTION_REPLY qUESTION_REPLY)
        {
            System.Threading.Tasks.Task<ActionResult> task = tool.webSocket(qUESTION_REPLY, tool.decryption(qUESTION_REPLY.token));
            return iuserBUS.REPLY(qUESTION_REPLY);
        }
        #endregion answer


        #region comment
        [HttpPost]
        [Route("comment")]
        public Comment comment(Comment Comment)
        {
            return iuserBUS.comment(Comment);
        }
        #endregion comment
    }

}