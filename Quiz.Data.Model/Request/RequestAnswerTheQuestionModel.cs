namespace Quiz.Data.Model.Request
{
    public class RequestAnswerTheQuestionModel
    {
        public long QuestionID { get; set; }
        public long AnswerID { get; set; }
        public long UserID { get; set; }
    }
}
