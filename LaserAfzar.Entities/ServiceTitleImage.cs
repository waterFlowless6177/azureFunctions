using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LaserAfzar.Entities
{
    [DataContract]
    public class ServiceTitleImage : ProjectImage
    {
        [ForeignKey("CompanyService")]
        [DataMember]
        public int ServiceTitleImageId { get; set; }

        public virtual CompanyService CompanyService { get; set; }
    }
}
