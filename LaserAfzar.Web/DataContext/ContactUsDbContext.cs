using LaserAfzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaserAfzar.Web.DataContext
{
    public class ContactUsDbContext : DbContext
    {
        public ContactUsDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<ContactUs> ContactUses { get; set; }
    }
}