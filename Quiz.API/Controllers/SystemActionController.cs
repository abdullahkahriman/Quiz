using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Model.System;
using Quiz.Data.Service;

namespace Quiz.API.Controllers
{
    public class SystemActionController : BaseController
    {
        private readonly SystemActionService systemActionService;

        public SystemActionController(IRepository<SystemAction> _systemActionService)
        {
            this.systemActionService = (SystemActionService)_systemActionService;
        }

        [HttpGet("[action]/{id?}")]
        public ActionResult<Result<object>> Get(long? id)
        {
            if (id == null)
                return this.systemActionService.Get();
            else
                return this.systemActionService.GetByID(id.Value);
        }

        [HttpPost("[action]")]
        public ActionResult<Result<object>> Save([FromBody] SystemAction model)
        {
            return this.systemActionService.Save(model);
        }
    }
}