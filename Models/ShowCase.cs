using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoveNowB.Models
{
    public class ShowCase
    {
        [Key]
        public int ShowID { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Display(Name = "Show Type")]
        public ShowType? ShowType { get; set; }
        public string PhotoPath { get; set; }
    }
}
