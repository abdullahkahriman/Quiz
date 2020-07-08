using System;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.Static;
using Quiz.Core;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.Request;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(IRepository<Question> quizService)
        {
            this._quizService = (QuizService)quizService;
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> Get([FromBody] QuizRequestModel model)
        {
            try
            {
                if (model == null)
                    model = new QuizRequestModel();

                model.UserID = Current.User.ID;
                return this._quizService.Questions(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> AnswerTheQuestion([FromBody] AnswerTheQuestionModel model)
        {
            try
            {
                model.UserID = Current.User.ID;
                return this._quizService.AnswerTheQuestion(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}