using MoveNowB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Services.Interfaces
{
    public interface IRentCarRepository
    {
        
        IEnumerable<RentCar> GetAllRents();
        IEnumerable<RentCar> RentsByUser(string username);
        Car rentCar(int id, string type);
        RentCar returnCar(int rentId, int carId, string type);
        void AvailabilityChange(int id, string type);
        int Availability(int carId);
    }
}
