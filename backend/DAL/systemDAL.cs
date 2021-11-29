using MODELS;
using System;

using DAL.Helper;
using System.Collections.Generic;
using DAL.Interface;
using System.Linq;

namespace DAL
{
  

    public class systemDAL : DAL.Interface.iSystemDAL
    {
        IDatabaseHelper databaseHelper;
        public systemDAL(IDatabaseHelper k)
        {
            databaseHelper=k;
        }
   

        public  Tuple<questionModal,List <QUESTION_REPLY>,List <Comment>> getDetailQuestion(string slug,int id)
        {
            string msgError = "";
          
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "QuestionDetail",
                     "@slug", slug
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                      if(dt.Rows.Count>0)
                        { 
                            var question=dt.ConvertTo<questionModal>().FirstOrDefault();
                            if(question.type=="true"  )
                            {
                                
                                if(CheckAccset(id,question.QUESTION_ID))
                                {
                                    
                                    var rep = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "getReply",
                                        "@id", question.QUESTION_ID
                                        );
                                    
                                            var reply=rep.ConvertTo<QUESTION_REPLY>().ToList();
                                    var Comment = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "getComment",
                                        "@id", question.QUESTION_ID
                                        );
                                    var CommentList=Comment.ConvertTo<Comment>().ToList();
                                        return Tuple.Create(question,reply,CommentList);
                                    
                                }
                                return null;
                            }
                            else
                            {

                            
                                 var rep = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "getReply",
                                        "@id", question.QUESTION_ID
                                        );
                                    
                                            var reply=rep.ConvertTo<QUESTION_REPLY>().ToList();
                                    var Comment = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "getComment",
                                        "@id", question.QUESTION_ID
                                        );
                                    var CommentList=Comment.ConvertTo<Comment>().ToList();
                                        return Tuple.Create(question,reply,CommentList);
                            }
                                
                            
                        }
                     return null;
          
               
        }
        public object getQuestion()
        {
            string msgError = "";
          
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "GetAllQuestion");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                      if(dt.Rows.Count>0)
                        { 
                            
                            return dt.ConvertTo<questionModal>().ToList();

                        }
                     return null;
          
        }
        public bool CheckAccset(int userID,int qID)
        {
            string msgError="";
            var acc = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "checkAccset",
                "@uID", userID,
                "@qID",qID
            );
            int k=int.Parse(acc.Rows[0]["count"].ToString());
          
            if(k>0)
            {
                return true;
            }
            return false;
        }

        public object getNotication(string id)
        {
            string msgError = "";
          
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "getNOtication",
                "@id",id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                      if(dt.Rows.Count>0)
                        { 
                            
                            return dt.ConvertTo<notication_model>().ToList();

                        }
                     return null;
        }
    }
}