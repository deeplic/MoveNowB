using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoveNowB.Models;
using MoveNowB.Services.Interfaces;
using MoveNowB.Services.Repositories;
using MoveNowB.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carReposity;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ICarRepository carReposity,IWebHostEnvironment hostingEnvironment)
        {
            _carReposity = carReposity;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult AddCar()
        {
            CarViewModel cVM = new CarViewModel();
            cVM.pageHeader = "New Car(s) to Collection";
            return View(cVM);
        }
        [HttpPost]
        public IActionResult AddCar(CarViewModel carModel)
        {
            CarViewModel cVM = new CarViewModel();
            cVM.Photo = carModel.Photo;
            cVM.pageHeader = "New Car(s) to Collection";
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (cVM.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + cVM.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    cVM.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Car newCar = new Car
                {
                    Amount = carModel.Amount,
                    BrandName = carModel.BrandName,
                    ModelName = carModel.ModelName,
                    Year = carModel.Year,
                    Description = carModel.Description,
                    ShowType = carModel.ShowType,
                    PhotoPath = uniqueFileName
                };
                _carReposity.AddCar(newCar);
                return RedirectToAction("Details", new { id = newCar.CarID });
            }
            return View(carModel);
        }
        public IActionResult Details(int id)
        {
            DetailViewModel dVM = new DetailViewModel();
            dVM.Car = _carReposity.GetCarById(id);
            dVM.pageHeader = _carReposity.GetCarById(id).ModelName;
            return View(dVM);
        }
        public IActionResult Index()
        {
            var model = _carReposity.GetHomeCars();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
