using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Model.System.Authentication;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(IRepository<User> userService)
        {
            this._userService = (UserService)userService;
        }

        [HttpPost("login")]
        [ResponseCache(Duration = 30)]
        public ActionResult<Result<LoginResponse>> Login([FromBody]LoginRequest request)
        {
            return this._userService.SignIn(request);
        }

        [HttpPost("register")]
        [ResponseCache(Duration = 30)]
        public ActionResult<Result<object>> Register([FromBody]RegisterRequest request)
        {
            return this._userService.SignUp(request);
        }
    }
}