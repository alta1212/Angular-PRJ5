using System.Collections.Generic;

namespace MODELS
{
    public class questionModal
    {
        public int QUESTION_ID { get; set; }
        public int author{ get; set; }
        public string QUESTION_TITLE { get; set; }
        public string QUESTION_TAG { get; set; }
        public string QUESTION_CATEGORY { get; set; }
        public string QUESTION_DETAIL { get; set; }
        public string QUESTION_IMAGE { get; set; }
        public string SLUGS { get; set; }
        public string getNotication { get; set; }
        public string type { get; set; }
        public string author_image { get; set; }
        public string author_name { get; set; }
        public string time { get; set; }
        public int vote { get; set; }
        public int answer{ get; set; }
    }
    public class QUESTION_Accset
    {
        public string token { get; set; }
    }
}