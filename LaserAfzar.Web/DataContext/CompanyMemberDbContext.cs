using LaserAfzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaserAfzar.Web.DataContext
{
    public class CompanyMemberDbContext : DbContext
    {
        public CompanyMemberDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<CompanyMember> CompanyMembers { get; set; }
        public DbSet<CompanyMemberImage> CompanyMemberImages { get; set; }
    }
}