using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    public class RoleController : BaseController
    {
        private readonly RoleService roleService;

        public RoleController(IRepository<Role> _roleService)
        {
            this.roleService = (RoleService)_roleService;
        }

        [HttpGet("[action]/{id?}")]
        public ActionResult<Result<object>> Get(long? id)
        {
            if (id==null)
                return this.roleService.Get();
            else
                return this.roleService.GetByID(id.Value);
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> Save([FromBody] Role model)
        {
            return this.roleService.Save(model);
        }
    }
}