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
    public class ServicesApiController : ApiController
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        public ServicesApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/ServicesApi
        public IQueryable<CompanyService> GetCompanyServices()
        {
            return db.CompanyServices.Include(c => c.ServiceTitleImage);
        }

        // GET: api/ServicesApi/5
        [ResponseType(typeof(CompanyService))]
        public async Task<IHttpActionResult> GetCompanyService(int id)
        {
            CompanyService companyService = await db.CompanyServices.Include(c => c.ServiceTitleImage)
                                                                    .FirstOrDefaultAsync(e => e.CompanyServiceId == id);
            if (companyService == null)
            {
                return NotFound();
            }

            companyService.SubServices = db.SubServices.Include(i => i.ServiceImage)
                                                       .Where(s => s.CompanyService.CompanyServiceId == id)
                                                       .ToList();

            return Ok(companyService);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyServiceExists(int id)
        {
            return db.CompanyServices.Count(e => e.CompanyServiceId == id) > 0;
        }
    }
}