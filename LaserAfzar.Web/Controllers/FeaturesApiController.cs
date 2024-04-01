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
    public class FeaturesApiController : ApiController
    {
        private FeaturesDbContext db = new FeaturesDbContext();

        // GET: api/FeaturesApi
        public IQueryable<Feature> GetFeatures()
        {
            return db.Features;
        }

        // GET: api/FeaturesApi/5
        [ResponseType(typeof(Feature))]
        public async Task<IHttpActionResult> GetFeature(int id)
        {
            Feature feature = await db.Features.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }

            return Ok(feature);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeatureExists(int id)
        {
            return db.Features.Count(e => e.FeatureId == id) > 0;
        }
    }
}