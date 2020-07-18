using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Create(long? ID)
        {
            if ((ID ?? 0) == 0)
            {
                return RedirectToAction("List");
            }

            ViewBag.ID = ID.Value;
            return View();
        }
    }
}