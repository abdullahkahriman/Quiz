using Quiz.Core;

namespace Quiz.Data.Model.Entity
{
    public class Answer : Superior
    {
        public string Text { get; set; }
        public bool IsTrue { get; set; }
    }
}
