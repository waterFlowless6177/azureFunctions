using LaserAfzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaserAfzar.Web.DataContext
{
    public class CompanyServiceDbContext : DbContext
    {
        public CompanyServiceDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<CompanyService> CompanyServices { get; set; }
        public DbSet<ServiceTitleImage> ServiceTitleImages { get; set; }
        public DbSet<SubService> SubServices { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
    }
}