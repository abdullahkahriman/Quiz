using Microsoft.AspNetCore.Mvc;
using Quiz.Core;
using Quiz.Data.Service;
using Quiz.Data.Service.Interface;

namespace Quiz.API.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly DashboardService dashboardService;

        public DashboardController(IDashboardService _dashboardService)
        {
            this.dashboardService = (DashboardService)_dashboardService;
        }

        [HttpGet("[action]")]
        public ActionResult<Result<object>> GetCount()
        {
            return this.dashboardService.GetCount();
        }
    }
}