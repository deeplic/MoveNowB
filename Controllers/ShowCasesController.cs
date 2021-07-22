using Microsoft.AspNetCore.Mvc;
using MoveNowB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Controllers
{
    public class ShowCasesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddShow()
        {
            ShowCaseViewModel sVM = new ShowCaseViewModel();
            sVM.pageHeader = "New Car(s) to Collection";
            return View(sVM);
        }
    }
}
