using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LaserAfzar.Entities
{
    [DataContract]
    public class ProjectImage
    {
        [Required]
        [StringLength(255)]
        [DataMember]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        [DataMember]
        public string FileName { get; set; }
    }
}
