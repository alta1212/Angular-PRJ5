using System;
using MODELS;
using DAL.Helper;
using System.Collections.Generic;
using DAL.Interface;

using System.Linq;
namespace DAL
{
    public class userDAl : DAL.Interface.iUserDAl
    {
        IDatabaseHelper databaseHelper;
        public userDAl(IDatabaseHelper k)
        {
            databaseHelper = k;
        }
        #region  question
        public questionModal ask(questionModal q)
        {


            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "newQuestion",
                    "@title", q.QUESTION_TITLE,
                     "@tag", q.QUESTION_TAG,
                     "@category", q.QUESTION_CATEGORY,
                     "@detail", q.QUESTION_DETAIL,
                     "@slug", q.SLUGS,
                     "@getNOtication",q.getNotication,
                     "@type",q.type ,
                     "@author", q.author,
                     "@author_image", q.author_image,
                     "@author_name", q.author_name);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion  question
        #region auth

        public userModel login(userModel user)
        {
            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "userLogin",
                     "@usermail", user.USER_EMAIL,
                     "@userpassword", user.USER_PASSWORD
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0)
                {
                    return dt.ConvertTo<userModel>().FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public userModel signUp(userModel user)
        {

            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "userSignup",
                     "@username", user.USER_NAME,
                     "@usermail", user.USER_EMAIL,
                     "@userpassword", user.USER_PASSWORD,
                     "@slug", user.slug,
                     "@image",user.USER_IMAGE
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<userModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool enable2fa(string id)
        {
            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "ENABLE2fa",
                     "@id", id
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool disable2fa(string id)
        {
            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "disable2fa",
                     "@id", id
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion auth

        #region answer
        public QUESTION_REPLY REPLY(QUESTION_REPLY qUESTION_REPLY)
        {
            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "answer",
                     "@answer_QUESTION_ID", qUESTION_REPLY.answer_QUESTION_ID,
                     "@detail", qUESTION_REPLY.answer_DETAIL,
                     "@author_name", qUESTION_REPLY.answer_author_name,
                     "@author_image", qUESTION_REPLY.answer_author_image,
                     "@author", qUESTION_REPLY.answer_author
                     );
                     databaseHelper.ExecuteSProcedure("PutNotication",
                     "@detail",qUESTION_REPLY.answer_author_name +" has reply to you question :",
                     "@user",qUESTION_REPLY.author_QUESTION_ID,
                     "@avata",qUESTION_REPLY.answer_author_image);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<QUESTION_REPLY>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Comment comment(Comment c)
        {
            string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "PostComment",
                     "@answer_REPLY_ID", c.answer_REPLY_ID,
                     "@QUESTION_ID", c.QUESTION_ID,
                     "@COMMENT_DETAIL", c.COMMENT_DETAIL,
                     "@COMMENT_author", c.COMMENT_author,
                     "@COMMENT_author_image", c.COMMENT_author_image,
                     "@COMMENT_author_name", c.COMMENT_author_name
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Comment>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object CreateAsset(string QID, string usermail)
        {
           string msgError = "";
            try
            {
                var dt = databaseHelper.ExecuteSProcedureReturnDataset(out msgError, "CreateAsset",
                "@Qid",QID,
                "@email",usermail
                     );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion answer

    }
}
