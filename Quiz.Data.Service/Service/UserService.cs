using Microsoft.EntityFrameworkCore;
using Quiz.Core;
using Quiz.Core.Infrastructure;
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

        /// <summary>
        /// All list
        /// </summary>
        /// <returns></returns>
        public Result<object> Get()
        {
            Result<object> result;

            try
            {
                var list = this._GetWhere(c => !c.IsDeleted && c.UserRoles.Any(r => r.RoleID != (byte)Static.Role.Administrator))
                    .Select(c => new
                    {
                        ID = c.ID,
                        Username = c.Username,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    });
                result = new Result<object>(true, string.Empty, list);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// Single user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public Result<object> GetByID(long id)
        {
            Result<object> result;

            try
            {
                User user;

                if (id == 0)
                    user = new User() { };
                else
                {
                    user = this._context.User.Where(c => !c.IsDeleted && c.ID == id)
                    .Include(c => c.UserRoles)
                    .FirstOrDefault();

                    if (user == null)
                        return new Result<object>(false, "User not found!");
                }

                var roles = this._context.Role.Where(c => !c.IsDeleted)
                   .Select(c => new
                   {
                       ID = c.ID,
                       Name = c.Name,
                       Checked = user.UserRoles == null ? false : user.UserRoles.Select(ur => ur.RoleID).ToArray().Any(ur => ur == c.ID)
                   });

                var find = new
                {
                    ID = user.ID,
                    Username = user.Username,
                    Roles = roles
                };
                result = new Result<object>(true, string.Empty, find);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// User save or update
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns></returns>
        public Result<object> Save(User model)
        {
            Result<object> result;

            try
            {
                if (string.IsNullOrEmpty(model.Username))
                    return new Result<object>(false, "Username is required");

                if (model.UserRoles == null || model.UserRoles.Count == 0)
                    return new Result<object>(false, "You must choose a role");

                User user = this._context.User.Where(c => c.ID == model.ID).Include(c => c.UserRoles).FirstOrDefault();
                if (user == null)
                {
                    if (string.IsNullOrEmpty(model.Password))
                        return new Result<object>(false, "Password is required");

                    this._Add(model);
                }
                else
                {
                    if (this._GetAny<User>(c => c.ID != model.ID && c.Username.ToLower().Equals(model.Username.ToLower())))
                        return new Result<object>(false, "Username already exists");
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Password))
                            user.Password = model.Password.ToSHA256();

                        user.Username = model.Username;
                        user.UpdatedAt = DateTime.Now;
                        user.UserRoles = model.UserRoles;
                        this._Update(user);
                    }
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
        /// User delete
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public Result<object> Delete(long id)
        {
            Result<object> result;

            try
            {
                User user = this._context.User.Where(c => c.ID == id).FirstOrDefault();
                if (user == null)
                    return new Result<object>(false, "User not found!");
                else
                {
                    user.IsDeleted = true;
                    user.UpdatedAt = DateTime.Now;
                    this._context.Entry(user).State = EntityState.Modified;
                    this._context.SaveChanges();

                    result = new Result<object>(true, "Updated");
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