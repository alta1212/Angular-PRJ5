using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using MODELS;

namespace BUS.Interface
{
    public partial interface IuserBUS
    {
        #region auth
        userModel login(userModel user);
        userModel signUp(userModel user);
        bool enable2fa(string id); 
        bool disable2fa(string v);
        #endregion auth
        #region  question
        questionModal ask(questionModal q);
       
        #endregion question
        #region  answer
        QUESTION_REPLY REPLY(QUESTION_REPLY qUESTION_REPLY);
        #endregion answer
        #region  comment
        Comment comment(Comment c);
        #endregion comment
        object CreateAsset(string qid,string email);
    }
}
