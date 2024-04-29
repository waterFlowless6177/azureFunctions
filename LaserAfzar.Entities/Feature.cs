using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserAfzar.Entities
{
    public class Feature
    {
        public int FeatureId { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name ="FontAwesome Icon")]
        public string Icon { get; set; }
    }
}
