using MODELS;
using DAL.Interface;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class systemBUS : BUS.Interface.Isystem
    {
        private iSystemDAL systemDAl;
        public systemBUS (iSystemDAL k)
        {
            systemDAl=k;
        }
     
        public  Tuple<questionModal,List <QUESTION_REPLY>,List <Comment>> getDetailQuestion(string slug,int id)
        {
            return systemDAl.getDetailQuestion(slug,id);
        }

        public object getNotication(string id)
        {
            return systemDAl.getNotication(id);
        }

        public object getQuestion()
        {
            return systemDAl.getQuestion();
        }
    }
}