using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult _403()
        {
            return View();
        }
    }
}