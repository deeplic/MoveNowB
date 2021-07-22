using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MoveNowB.Models;
using MoveNowB.Services.Interfaces;
using MoveNowB.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Controllers
{
    public class ShowCasesController : Controller
    {
        public readonly IShowCaseRepository _showCaseReposity;
        public readonly IWebHostEnvironment _hostingEnvironment;
        public ShowCasesController(IShowCaseRepository showCaseReposity, IWebHostEnvironment hostingEnvironment)
        {
            _showCaseReposity = showCaseReposity;
            _hostingEnvironment = hostingEnvironment;
        }
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
        [HttpPost]
        public IActionResult AddShow(ShowCaseViewModel showModel)
        {
            ShowCaseViewModel sVM = new ShowCaseViewModel();
            sVM.Photo = showModel.Photo;
            sVM.pageHeader = "New Show Case";
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (sVM.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + sVM.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    sVM.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                ShowCase newShow = new ShowCase
                {
                    ShortDescription = showModel.ShortDescription,
                    BrandName = showModel.BrandName,
                    LongDescription = showModel.LongDescription,
                    ShowType = showModel.ShowType,
                    PhotoPath = uniqueFileName
                };
                _showCaseReposity.AddShow(newShow);
                return RedirectToAction("Details", new { id = newShow.ShowID });
            }

            return View(showModel);
        }
    }
}
