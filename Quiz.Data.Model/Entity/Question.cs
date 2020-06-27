using Quiz.Core;
using System.Collections.Generic;

namespace Quiz.Data.Model.Entity
{
    public class Question:Superior
    {
        public string Text { get; set; }
        public bool IsMultipleChoice { get; set; }

        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}