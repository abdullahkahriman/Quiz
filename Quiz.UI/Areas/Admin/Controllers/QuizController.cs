﻿using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Areas.Admin.Controllers
{
    public class QuizController : BaseController
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Create(long? ID)
        {
            ViewBag.ID = ID ?? 0;
            return View();
        }
    }
}