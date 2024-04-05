using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LaserAfzar.Entities;
using LaserAfzar.Web.DataContext;

namespace LaserAfzar.Web.Controllers
{
    public class AboutUsApiController : ApiController
    {
        private AboutUSDbContext db = new AboutUSDbContext();

        // GET: api/AboutUsApi
        public IQueryable<AboutUs> GetAboutUses()
        {
            return db.AboutUses;
        }

        // GET: api/AboutUsApi/5
        [ResponseType(typeof(AboutUs))]
        public async Task<IHttpActionResult> GetAboutUs(int id)
        {
            AboutUs aboutUs = await db.AboutUses.FindAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return Ok(aboutUs);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AboutUsExists(int id)
        {
            return db.AboutUses.Count(e => e.AboutUsId == id) > 0;
        }
    }
}