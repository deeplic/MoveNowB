using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoveNowB.Data;
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
        private readonly AppDbContext _context;
        public HomeController(ICarRepository carReposity,IWebHostEnvironment hostingEnvironment, AppDbContext context)
        {
            _carReposity = carReposity;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCar()
        {
            CarViewModel cVM = new CarViewModel();
            cVM.pageHeader = "New Car(s) to Collection";
            return View(cVM);
        }
        [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            DetailViewModel dVM = new DetailViewModel();
            dVM.Car = _carReposity.GetCarById(id);
            dVM.pageHeader = _carReposity.GetCarById(id).ModelName;
            return View(dVM);
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _carReposity.GetHomeCars();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var car = _carReposity.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("CarID,BrandName,ModelName,Year,Description,Amount,ShowType,PhotoPath")] Car car)
        {
            if (id != car.CarID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    _context.SaveChanges();
                    RedirectToAction("Details", "Home");
                }
                finally
                {
                    
                    //return RedirectToAction("Details", new { id });
                }
                return RedirectToAction("Details", new { id });
            }
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Car car = _context.Cars.Find(id);
            return View(car);
        }
        // POST: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _carReposity.DeleteCar(id);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AllCars()
        {
            var model = _carReposity.GetAllCars();
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            var model = _context.Cars.Where(j => j.BrandName.Contains(SearchPhrase) || j.ModelName.Contains(SearchPhrase)).ToList();
            return View("AllCars", model);
        }
        [AllowAnonymous]
        public IActionResult BrandCars(string name)
        {
            var model = _carReposity.GetCarByBrand(name);
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
