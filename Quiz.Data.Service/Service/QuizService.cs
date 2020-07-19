using Microsoft.EntityFrameworkCore;
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

        public Result<object> Questions(RequestQuizModel model)
        {
            //kullanıcının cevap verdiği soru id'lerini bul
            long[] findQuestionIDS = this._context.UserAnswer
                        .Where(c => c.UserID == model.UserID)
                        .Select(c => c.QuestionID)
                        .ToArray();

            bool isFinish = false;

            int questionTotalCount = this._Count(c => !c.IsDeleted),
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
                            Text = qa.Answer.Text
                        }).ToList(),
                    })
                    .Take(1)
                    .ToList(),
                QuestionTotalCount = questionTotalCount,
                QuestionTotalAnswered = findQuestionIDS.Length,
                IsFinish = isFinish
            };

            return new Result<object>(true, questions);
        }

        /// <summary>
        /// Answer the Question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<object> AnswerTheQuestion(RequestAnswerTheQuestionModel model)
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
                    else if (this._context.Answer.Any(c => c.ID == model.AnswerID && !c.IsTrue))
                    {
                        result = new Result<object>(false, "Wrong answer");
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

        /// <summary>
        /// Get question list
        /// </summary>
        /// <returns></returns>
        public Result<object> GetAdmin()
        {
            Result<object> result;

            try
            {
                var list = this._context.Question.Where(c => !c.IsDeleted)
                    .OrderByDescending(c => c.UpdatedAt)
                    .OrderByDescending(c => c.CreatedAt)
                    .Include(c => c.QuestionAnswers)
                    .Select(c => new
                    {
                        ID = c.ID,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        AnswerCount = c.QuestionAnswers.Count
                    }).ToList();
                result = new Result<object>(true, string.Empty, list);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// Get question by id
        /// </summary>
        /// <param name="id">Question ID</param>
        /// <returns></returns>
        public Result<object> GetAdminByID(long id)
        {
            Result<object> result;

            try
            {
                Question question = this._context.Question.Where(c => !c.IsDeleted && c.ID == id)
                    .Include("QuestionAnswers.Answer")
                    .FirstOrDefault();

                if (id == 0)
                {
                    question = new Question()
                    {
                        QuestionAnswers = new List<QuestionAnswer>()
                    };
                }
                else if (question == null)
                    return new Result<object>(false, "Question not found!");

                result = new Result<object>(true, string.Empty, question);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// Question save or update
        /// </summary>
        /// <param name="model">Question model</param>
        /// <returns></returns>
        public Result<object> Save(Question model)
        {
            Result<object> result;

            try
            {
                if (string.IsNullOrEmpty(model.Text))
                    return new Result<object>(false, "Question is required");

                if (model.QuestionAnswers == null || model.QuestionAnswers.Count == 0 || model.QuestionAnswers.Any(c => string.IsNullOrEmpty(c.Answer?.Text)))
                    return new Result<object>(false, "Answer is required");

                if (model.QuestionAnswers.Count(c => c.Answer.IsTrue) == 0)
                    return new Result<object>(false, "You must mark at least one correct answer");

                Question question = this._GetSingle(c => !c.IsDeleted && c.ID == model.ID);

                if (question == null)
                    this._Add(model);
                else
                {
                    question.Text = model.Text;
                    question.QuestionAnswers = model.QuestionAnswers;
                    this._Update(question);
                }

                result = new Result<object>(true, string.Empty);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// Question delete
        /// </summary>
        /// <param name="id">Question ID</param>
        /// <returns></returns>
        public Result<object> Delete(long id)
        {
            Result<object> result;

            try
            {
                Question question = this._GetSingle(c => !c.IsDeleted && c.ID == id);
                if (question == null)
                    return new Result<object>(false, "Question not found!");
                else
                {
                    this._Remove(id);

                    result = new Result<object>(true, "Deleted");
                }
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }
    }
}