namespace MODELS
{
    public class QUESTION_REPLY
    {
        public int  answer_REPLY_ID {get;set;}
        public int  answer_QUESTION_ID {get;set;}
        public int author_QUESTION_ID {get;set;}
        public int  PIN  {get;set;}
        public string answer_DETAIL {get;set;}
        public string answer_IMAGE {get;set;}
        public int  answer_author {get;set;}
        public string answer_time {get;set;}
        public string answer_author_name {get;set;}
        public string  answer_author_image {get;set;}
        public string token  {get;set;}
    }
}