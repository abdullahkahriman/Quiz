using Quiz.Core;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz.Data.Service
{
    public class QuizService : Repository<Question>
    {
        public QuizService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Result<List<QuestionResponseModel>> Questions()
        {
            var questions = this._context.Question
                .Where(c => !c.IsDeleted)
                .Select(c => new QuestionResponseModel()
                {
                    Question = c.Text,
                    Answers = c.QuestionAnswers.Where(qa => !qa.Answer.IsDeleted).Select(qa => new AnswerResponseModel()
                    {
                        Text = qa.Answer.Text,
                        IsTrue = qa.Answer.IsTrue
                    }).ToList()
                }).ToList();

            return new Result<List<QuestionResponseModel>>(true, questions);
        }
    }
}
