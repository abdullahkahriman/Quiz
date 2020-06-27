using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Model.System.Authentication;
using Quiz.Data.Service;
using System;

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
            if (!string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password))
            {
                request.Password = request.Password.ToSHA256();
                User user = this._userService.GetSingle(c => c.Username.Equals(request.Username) && c.Password.Equals(request.Password));
                if (user != null)
                {
                    return new Result<LoginResponse>(true, "Login successfully", new LoginResponse()
                    {
                        Token = new Token()
                        {
                            ExpireDate = request.IsRemember ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(20),
                            UserID = user.ID
                        }
                        .ToJson()
                        .ToRijndael()
                    });
                }
                else
                {
                    return new Result<LoginResponse>(false, "Username or password incorrect");
                }
            }
            return StatusCode(400);
        }

        [HttpPost("register")]
        [ResponseCache(Duration = 30)]
        public ActionResult<Result<object>> Register([FromBody]RegisterRequest request)
        {
            if (request.Password == request.RePassword)
            {
                request.Password = request.Password.ToSHA256();
                this._userService.Add(new User()
                {
                    Username = request.Username,
                    Password = request.Password,
                    //RoleID = (long)Quiz.Core.Infrastructure.Static.Role.User
                });

                return new Result<object>(true, "Register successfully");
            }
            else
            {
                return new Result<object>(false, "Passwords must be same");
            }
        }
    }
}