using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LaserAfzar.Entities
{
    public class AboutUs
    {
        public int AboutUsId { get; set; }
        [Required]
        [StringLength(5000)]
        [AllowHtml]
        public string Description { get; set; }
    }
}
