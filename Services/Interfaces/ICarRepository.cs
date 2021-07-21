using MoveNowB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Services.Interfaces
{
    public interface ICarReposity
    {
        Car AddCar(Car car);
        Car GetCarById(int id);
        IEnumerable<Car> GetCarByBrand(string name);
        IEnumerable<Car> GetAllCars();
        IEnumerable<Car> GetHomeCars();
        List<string> GetBrands();
        Car UpdateCar(Car car);
        Car DeleteCar(int id);
    }
}
