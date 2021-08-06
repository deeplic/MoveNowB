using Microsoft.AspNetCore.Identity;
using MoveNowB.Data;
using MoveNowB.Models;
using MoveNowB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Services.Repositories
{
    public class RentCarRepository:IRentCarRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        public RentCarRepository(AppDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
        {
            _context = context;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public void AvailabilityChange(int id, string type)
        {
            var car = _context.Cars.Find(id);
            int original;
            if (type == "rent")
            {
                original = car.Amount;
                _context.Attach(car);
                _context.Entry(car).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                car.Amount = original - 1;
                _context.SaveChanges();
            }
            else
            {
                original = car.Amount;
                _context.Attach(car);
                _context.Entry(car).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                car.Amount = original + 1;
                _context.SaveChanges();
            }
        }
        public int Availability(int carId)
        {
            int result;
            if (_context.Cars.Find(carId)==null)
            {
                result = 0;
            }
            else
            {
                result = _context.Cars.Find(carId).Amount;
            }
            return result;
        }

        public IEnumerable<RentCar> GetAllRents()
        {
            var result = _context.RentCars.OrderByDescending(x => x.RentId).ToList();
            return result;
        }

        public Car rentCar(int id, string type)
        {
            AvailabilityChange(id, type);
            var car = _context.Cars.Find(id);
            var userid = _userRepository.GetUserId();
            string username = _userManager.Users.FirstOrDefault(x => x.Id == userid).UserName;
            string fullname = _userManager.Users.FirstOrDefault(x => x.Id == userid).FullName;

            RentCar rentCar = new RentCar
            {
                UserName = username,
                FullName = fullname,
                CarId = id,
                CarName = car.BrandName + " " + car.ModelName,
                isReturned = false

            };
            _context.RentCars.Add(rentCar);
            _context.SaveChanges();
            return car;
        }
        public RentCar returnCar(int rentId, int carId, string type)
        {
            AvailabilityChange(carId, type);
            var rentRecord = _context.RentCars.Find(rentId);
            _context.Attach(rentRecord);
            _context.Entry(rentRecord).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            rentRecord.isReturned = true;
            _context.SaveChanges();
            return rentRecord;
        }

        public IEnumerable<RentCar> RentsByUser(string username)
        {
            var userRecords = _context.RentCars.Where(x => x.UserName == username).OrderByDescending(x => x.RentId).ToList();
            return userRecords;
        }
    }
}
