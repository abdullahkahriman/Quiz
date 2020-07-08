using Quiz.Core;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.Request;
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

        public Result<object> Questions(QuizRequestModel model)
        {
            //kullanıcının cevap verdiği soru id'lerini bul
            long[] findQuestionIDS = this._context.UserAnswer
                        .Where(c => c.UserID == model.UserID)
                        .Select(c => c.QuestionID)
                        .ToArray();

            bool isFinish = false;

            int questionTotalCount = this.Count(c => !c.IsDeleted),
                questionTotalAnswer = 0;

            //kullanıcı kaç soruya cevap vermiş bul
            questionTotalAnswer = (from qa in this._context.QuestionAnswer where findQuestionIDS.Contains(qa.QuestionID) orderby qa.QuestionID select qa.QuestionID)
                .Distinct()
                .Count();

            //toplam soru sayısı ve kullanıcının verdiği soru sayıları eşitse tümüne cevap verilmiş demektir
            if (questionTotalCount > 0)
                isFinish = questionTotalAnswer == questionTotalCount;

            var questions = new
            {
                List = this._context.Question
                    .Where(c => !c.IsDeleted && !findQuestionIDS.Contains(c.ID) && (model.ID == null || c.ID > model.ID))
                    .Select(x => new
                    {
                        ID = x.ID,
                        Question = x.Text,
                        Answers = x.QuestionAnswers.Where(qa => !qa.Answer.IsDeleted).Select(qa => new
                        {
                            ID = qa.AnswerID,
                            Text = qa.Answer.Text,
                            IsTrue = qa.Answer.IsTrue
                        }).ToList(),
                    })
                    .Take(1)
                    .ToList(),
                QuestionTotalCount = questionTotalCount,
                QuestionTotalAnswered = findQuestionIDS.Length,
                IsFinish = isFinish
            };

            //var questions = this._context.Question
            //    .Where(c => !c.IsDeleted && !findQuestionIDS.Contains(c.ID) && (model.ID == null || c.ID > model.ID))
            //    .Select(c => new
            //    {
            //        ID = c.ID,
            //        Question = c.Text,
            //        Answers = c.QuestionAnswers.Where(qa => !qa.Answer.IsDeleted).Select(qa => new AnswerResponseModel()
            //        {
            //            ID = qa.AnswerID,
            //            Text = qa.Answer.Text,
            //            IsTrue = qa.Answer.IsTrue
            //        }).ToList(),
            //        QuestionTotalCount = questionTotalCount,
            //        QuestionTotalAnswered = findQuestionIDS.Length,
            //        IsFinish = isFinish
            //    })
            //    .Take(1)
            //    .ToList();

            return new Result<object>(true, questions);
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
                    bool isUserAnswer = this._context.UserAnswer
                        .Any(c => c.QuestionID == model.QuestionID && c.UserID == model.UserID);
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
