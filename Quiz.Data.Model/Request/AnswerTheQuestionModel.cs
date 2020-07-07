using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Data.Model.Request
{
   public class AnswerTheQuestionModel
    {
        public long QuestionID { get; set; }
        public long AnswerID { get; set; }
        public long UserID { get; set; }
    }
}
