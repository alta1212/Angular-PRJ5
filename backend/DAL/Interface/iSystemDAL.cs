using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using MODELS;
namespace DAL.Interface
{
    public partial interface iSystemDAL
    {
        #region  question

        Tuple<questionModal,List <QUESTION_REPLY>,List <Comment>> getDetailQuestion(string slug,int id);
        object getQuestion();
        object getNotication(string id);
        #endregion question
    }
}