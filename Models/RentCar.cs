using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Models
{
    public class RentCar
    {
        [Key]
        public int RentId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public bool isReturned { get; set; }
    }
}
