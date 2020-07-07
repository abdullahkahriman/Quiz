using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Data.Model.Response
{
    public class QuestionResponseModel
    {
        public long ID { get; set; }
        public string Question { get; set; }
        public List<AnswerResponseModel> Answers { get; set; }
    }

    public class AnswerResponseModel
    {
        public string Text { get; set; }
        public bool IsTrue { get; set; }
        public long ID { get; set; }
    }
}