using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace LaserAfzar.Entities
{
    [DataContract]
    public class SubService
    {
        [DataMember]
        public int SubServiceId { get; set; }
        [Required]
        [StringLength(255)]
        [DataMember]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        [AllowHtml]
        [DataMember]
        public string Description { get; set; }

        public virtual CompanyService CompanyService { get; set; }
        [DataMember]
        public virtual ICollection<ServiceImage> ServiceImage { get; set; }
    }
}
