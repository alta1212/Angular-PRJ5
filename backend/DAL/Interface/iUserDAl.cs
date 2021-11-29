using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using MODELS;

namespace DAL.Interface
{
    public partial interface iUserDAl
    {
        
        #region auth
        userModel login(userModel user);
        userModel signUp(userModel user);
        bool enable2fa(string id);
       
        #endregion auth
        #region  ask
        questionModal ask(questionModal q);
        bool disable2fa(string id);

        #endregion  ask
        #region  answer
        QUESTION_REPLY REPLY(QUESTION_REPLY qUESTION_REPLY);
        Comment comment(Comment c);
        #endregion answer
        object CreateAsset(string QID,string usermail);
    }
}
