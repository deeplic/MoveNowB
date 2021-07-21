using MoveNowB.Data;
using MoveNowB.Models;
using MoveNowB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Services.Repositories
{
    public class CarRepository:ICarRepository
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context)
        {
            _context = context;
        }
        public Car AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return car;
        }

        public Car DeleteCar(int id)
        {
            Car car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
            return car;
        }

        public IEnumerable<Car> GetAllCars()
        {
            var result = _context.Cars;
            return result;
        }

        public List<string> GetBrands()
        {
            var results = _context.Cars.ToList().Select(x => x.BrandName).Distinct().ToList();
            return results;
        }

        public IEnumerable<Car> GetCarByBrand(string name)
        {
            return _context.Cars.Where(x => x.BrandName == name);
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.Find(id);
        }

        public IEnumerable<Car> GetHomeCars()
        {
            return _context.Cars.Where(x => x.ShowType == ShowType.Advert);
        }

        public Car UpdateCar(Car car)
        {
            if (car != null)
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
            }
            return car;
        }
    }
}
