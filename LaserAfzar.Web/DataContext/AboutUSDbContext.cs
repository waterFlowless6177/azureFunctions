using LaserAfzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaserAfzar.Web.DataContext
{
    public class AboutUSDbContext : DbContext
    {
        public AboutUSDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<AboutUs> AboutUses { get; set; }
    }
}