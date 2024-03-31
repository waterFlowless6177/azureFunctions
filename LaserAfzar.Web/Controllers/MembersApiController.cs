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
    public class MembersApiController : ApiController
    {
        private CompanyMemberDbContext db = new CompanyMemberDbContext();

        public MembersApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/MembersApi
        public IQueryable<CompanyMember> GetCompanyMembers()
        {
            return db.CompanyMembers.Include(c => c.CompanyMemberImage);
        }

        // GET: api/MembersApi/5
        [ResponseType(typeof(CompanyMember))]
        public async Task<IHttpActionResult> GetCompanyMember(int id)
        {
            CompanyMember companyMember = await db.CompanyMembers.FindAsync(id);
            if (companyMember == null)
            {
                return NotFound();
            }

            return Ok(companyMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyMemberExists(int id)
        {
            return db.CompanyMembers.Count(e => e.CompanyMemberID == id) > 0;
        }
    }
}