using BUS.Interface;
using MODELS;
using DAL.Interface;
namespace BUS
{
    public class UserBUS : BUS.Interface.IuserBUS
    {

        private iUserDAl userDAl;
        public UserBUS (iUserDAl k)
        {
            userDAl=k;
        }

        public questionModal ask(questionModal q)
        {
           
           return userDAl.ask(q);
        }

        public userModel login(userModel user)
        {
            return userDAl.login(user);
        }

        public userModel signUp(userModel user)
        {
           return userDAl.signUp(user);
        }
        public bool enable2fa(string id)
        {
            return userDAl.enable2fa(id);
        }

        public bool disable2fa(string id)
        {
            return userDAl.disable2fa(id);
        }

        public QUESTION_REPLY REPLY(QUESTION_REPLY qUESTION_REPLY)
        {
            return userDAl.REPLY(qUESTION_REPLY);
        }

        public Comment comment(Comment c)
        {
            return userDAl.comment(c);
        }

        public object CreateAsset(string qid, string email)
        {
            return userDAl.CreateAsset(qid,email);;
        }
    }
}