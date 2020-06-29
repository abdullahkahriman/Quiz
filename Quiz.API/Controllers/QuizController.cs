using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.Response;
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

        [HttpGet]
        public ActionResult<Result<List<QuestionResponseModel>>> Get()
        {
            try
            {
                return this._quizService.Questions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}