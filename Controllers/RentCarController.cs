using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoveNowB.Models;
using MoveNowB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Controllers
{
    public class RentCarController : Controller
    {
        private readonly IRentCarRepository _rentCarReposity;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userService;
        public RentCarController(IRentCarRepository rentCarReposity, UserManager<ApplicationUser> userManager, IUserRepository userService)
        {
            _rentCarReposity = rentCarReposity;
            _userManager = userManager;
            _userService = userService;
        }
        public IActionResult Index()
        {
            /*var userid = _userService.GetUserId();
            string username = _userManager.Users.FirstOrDefault(x => x.Id == userid).UserName;
            var model = _rentCarReposity.RentsByUser(username);*/
            return View(/*model*/);
        }
        public IActionResult NewRent(int id)
        {
            _rentCarReposity.rentCar(id, "rent");
            return RedirectToAction("Index");
        }
    }
}
