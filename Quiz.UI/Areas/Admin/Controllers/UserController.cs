﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.UI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}