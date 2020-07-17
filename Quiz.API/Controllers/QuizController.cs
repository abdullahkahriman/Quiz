using System;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.Static;
using Quiz.Core;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.Request;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    public class QuizController : BaseController
    {
        private readonly QuizService quizService;

        public QuizController(IRepository<Question> _quizService)
        {
            this.quizService = (QuizService)_quizService;
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> Get([FromBody] RequestQuizModel model)
        {
            try
            {
                if (model == null)
                    model = new RequestQuizModel();

                model.UserID = Current.User.ID;
                return this.quizService.Questions(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> AnswerTheQuestion([FromBody] RequestAnswerTheQuestionModel model)
        {
            try
            {
                model.UserID = Current.User.ID;
                return this.quizService.AnswerTheQuestion(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}