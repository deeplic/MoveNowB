using Microsoft.AspNetCore.Http;
using MoveNowB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.ViewModels
{
    public class ShowCaseViewModel
    {
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Display(Name = "Show Type")]
        public ShowType? ShowType { get; set; }
        public IFormFile Photo { get; set; }
        public string pageHeader { get; set; }
    }
}
