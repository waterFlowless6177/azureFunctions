using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserAfzar.Entities
{
    public class CompanyMember
    {
        public int CompanyMemberID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string JobTitle { get; set; }
        [Required]
        [StringLength(255)]
        public string Message { get; set; }
        
        public virtual CompanyMemberImage CompanyMemberImage { get; set; }
    }
}
