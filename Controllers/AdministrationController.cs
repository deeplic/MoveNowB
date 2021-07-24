using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Controllers
{
    public class AdministrationController : Controller
    {
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
    }
}
