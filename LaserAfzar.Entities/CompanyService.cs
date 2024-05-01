using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LaserAfzar.Entities
{
    public class CompanyService
    {
        public int CompanyServiceId { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string BriefDescription { get; set; }
        [Required]
        [StringLength(5000)]
        [AllowHtml]
        public string FullDescription { get; set; }

        public virtual ServiceTitleImage ServiceTitleImage { get; set; }
        public virtual ICollection<SubService> SubServices { get; set; }

    }
}
