using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}