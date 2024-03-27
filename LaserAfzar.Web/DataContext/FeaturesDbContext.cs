using LaserAfzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaserAfzar.Web.DataContext
{
    public class FeaturesDbContext : DbContext
    {
        public FeaturesDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Feature> Features { get; set; }
    }
}