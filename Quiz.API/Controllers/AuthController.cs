using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Model.System.Authentication;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserService userService;

        public AuthController(IRepository<User> _userService)
        {
            this.userService = (UserService)_userService;
        }

        [HttpPost("login")]
        [ResponseCache(Duration = 30)]
        public ActionResult<Result<LoginResponse>> Login([FromBody]LoginRequest request)
        {
            return this.userService.SignIn(request);
        }

        [HttpPost("register")]
        [ResponseCache(Duration = 30)]
        public ActionResult<Result<object>> Register([FromBody]RegisterRequest request)
        {
            return this.userService.SignUp(request);
        }
    }
}