using Microsoft.AspNetCore.Http;
using MoveNowB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.ViewModels
{
    public class CarViewModel
    {
        public int CarID { get; set; }
        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Required]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        [Display(Name = "Show Type")]
        public ShowType? ShowType { get; set; }
        public IFormFile Photo { get; set; }
        public string pageHeader { get; set; }
    }
}
