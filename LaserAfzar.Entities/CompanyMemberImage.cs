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
    public class CompanyMemberImage : ProjectImage
    {
        [ForeignKey("CompanyMember")]
        [DataMember]
        public int CompanyMemberImageId { get; set; }

        public virtual CompanyMember CompanyMember { get; set; }
    }
}
