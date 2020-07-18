using Microsoft.AspNetCore.Mvc;
using Quiz.API.Static;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserService userService;

        public UserController(IRepository<User> userService)
        {
            this.userService = (UserService)userService;
        }

        [HttpGet("[action]/{id?}")]
        public ActionResult<Result<object>> Get(long? id)
        {
            if (id == null)
                return this.userService.Get();
            else
                return this.userService.GetByID(id.Value);
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> Save([FromBody] User model)
        {
            foreach (UserRole ur in model.UserRoles)
                ur.UserID = Current.User.ID;

            return this.userService.Save(model);
        }
    }
}