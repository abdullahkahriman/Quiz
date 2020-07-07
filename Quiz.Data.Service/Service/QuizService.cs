using Quiz.Core;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.Request;
using Quiz.Data.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Data.Service
{
    public class QuizService : Repository<Question>
    {
        public QuizService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Result<List<QuestionResponseModel>> Questions(QuizRequestModel model)
        {
            var questions = this._context.Question
                .Where(c => !c.IsDeleted && (model == null || model.ID == null || c.ID > model.ID))
                .Select(c => new QuestionResponseModel()
                {
                    ID = c.ID,
                    Question = c.Text,
                    Answers = c.QuestionAnswers.Where(qa => !qa.Answer.IsDeleted).Select(qa => new AnswerResponseModel()
                    {
                        ID = qa.AnswerID,
                        Text = qa.Answer.Text,
                        IsTrue = qa.Answer.IsTrue
                    }).ToList()
                })
                .Take(1)
                .ToList();

            return new Result<List<QuestionResponseModel>>(true, questions);
        }

        /// <summary>
        /// Answer the Question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<object> AnswerTheQuestion(AnswerTheQuestionModel model)
        {
            Result<object> result;

            try
            {
                var findQuestionAndAnswer = this._context.QuestionAnswer
                        .Where(c => c.QuestionID == model.QuestionID && c.AnswerID == model.AnswerID)
                        .FirstOrDefault();

                if (findQuestionAndAnswer == null)
                    result = new Result<object>(false, "Question or Answer not found");
                else
                {
                    bool isUserAnswer = this._context.UserAnswer.Any(c => c.QuestionID == model.QuestionID &&  c.UserID == model.UserID);
                    if (isUserAnswer)
                    {
                        result = new Result<object>(false, "You already answered");
                    }
                    else
                    {
                        this._context.UserAnswer.Add(new UserAnswer()
                        {
                            QuestionID = model.QuestionID,
                            AnswerID = model.AnswerID,
                            UserID = model.UserID
                        });
                        this._context.SaveChanges();
                        result = new Result<object>(true, "Answer saved");
                    }
                }
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, ex.Message);
            }

            return result;
        }
    }
}
