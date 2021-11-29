using System;
using System.Collections.Generic;
using MODELS;
namespace BUS.Interface
{
    public partial interface Isystem
    {
       
        Tuple<questionModal,List <QUESTION_REPLY>,List <Comment>> getDetailQuestion(string slug,int id);
        object getQuestion();
        object getNotication(string id);
    }
}