using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LaserAfzar.Entities
{
    [DataContract]
    public class ServiceImage : ProjectImage
    {
        [DataMember]
        public int ServiceImageId { get; set; }

        public virtual SubService SubService { get; set; }
    }
}
