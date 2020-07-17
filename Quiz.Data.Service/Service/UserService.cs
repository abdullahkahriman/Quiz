using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Model.System.Authentication;
using System;
using System.Linq;

namespace Quiz.Data.Service
{
    public class UserService : Repository<User>
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<LoginResponse> SignIn(LoginRequest request)
        {
            Result<LoginResponse> result;

            if (!string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password))
            {
                request.Password = request.Password.ToSHA256();
                //User user = this.GetSingle(c => c.Username.Equals(request.Username) && c.Password.Equals(request.Password));
                User user = this._context.User
                    .Where(c => c.Username.Equals(request.Username) && c.Password.Equals(request.Password)).Include(c => c.UserRoles)
                    .FirstOrDefault();
                if (user != null)
                {
                    result = new Result<LoginResponse>(true, "Login successfully", new LoginResponse()
                    {
                        Token = new Token()
                        {
                            ExpireDate = request.IsRemember ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(20),
                            UserID = user.ID
                        }
                        .ToJson()
                        .ToRijndael(),
                        Go = user.UserRoles.Any(r => r.RoleID == (byte)Core.Infrastructure.Static.Role.Administrator) ? "/admin" : "/"
                    });
                }
                else
                    result = new Result<LoginResponse>(false, "Username or password incorrect");
            }
            else
                result = new Result<LoginResponse>(false, "Username and password required");

            return result;
        }

        /// <summary>
        /// Sign up user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<object> SignUp(RegisterRequest request)
        {
            Result<object> result;

            if (!string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password))
            {
                if (!request.Password.IsPasswordLength())
                {
                    result = new Result<object>(false, "Password must be a min. of 6 characters");
                }
                else
                {
                    if (request.Password == request.RePassword)
                    {
                        if ((this._GetAny<User>(c => c.Username.ToLower().Equals(request.Username.ToLower()))))
                        {
                            result = new Result<object>(false, "Username already exists");
                        }
                        else
                        {
                            request.Password = request.Password.ToSHA256();
                            this._Add(new User()
                            {
                                Username = request.Username,
                                Password = request.Password
                            });

                            result = new Result<object>(true, "Register successfully");
                        }
                    }
                    else
                    {
                        result = new Result<object>(false, "Passwords must be same");
                    }
                }
            }
            else
            {
                result = new Result<object>(false, "Username and password required");
            }

            return result;
        }
    }
}
