using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaserAfzar.Entities;
using LaserAfzar.Web.DataContext;
using LaserAfzar.Web.Models;

namespace LaserAfzar.Web.Controllers
{
    [AllowAnonymous]
    public class SubServicesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: SubServices
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.CompanyServiceId = id;
            return View(await db.SubServices.Where(c => c.CompanyService.CompanyServiceId == id).ToListAsync());
        }

        // GET: SubServices/Details/5
        public async Task<ActionResult> Details(int? id, int CompanyServiceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubService subService = await db.SubServices.FindAsync(id);
            if (subService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceId = CompanyServiceId;
            return View(subService);
        }

        // GET: SubServices/Create
        public ActionResult Create(int id)
        {
            ViewBag.CompanyServiceId = id;
            return View();
        }

        // POST: SubServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubServiceId,Title,Description")] SubService subService, int CompanyServiceId)
        {
            if (ModelState.IsValid)
            {
                subService.CompanyService = db.CompanyServices.Find(CompanyServiceId);
                db.SubServices.Add(subService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = CompanyServiceId });
            }

            return View(subService);
        }

        // GET: SubServices/Edit/5
        public async Task<ActionResult> Edit(int? id, int CompanyServiceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubService subService = await db.SubServices.FindAsync(id);
            if (subService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceId = CompanyServiceId;
            return View(subService);
        }

        // POST: SubServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubServiceId,Title,Description")] SubService subService, string companyservicesID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "SubServices", new { id = companyservicesID });
            }
            return View(subService);
        }

        // GET: SubServices/Delete/5
        public async Task<ActionResult> Delete(int? id, int CompanyServiceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubService subService = await db.SubServices.FindAsync(id);
            if (subService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceId = CompanyServiceId;
            return View(subService);
        }

        // POST: SubServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int companyservicesID)
        {

            SubService subService = await db.SubServices.FindAsync(id);
            subService.ServiceImage = db.ServiceImages.Where(c => c.SubService.SubServiceId == id).ToList();

            //if we have images for subservice we need to delete them first
            if (subService.ServiceImage != null)
            {
                foreach (var item in subService.ServiceImage.ToList())
                {
                    //delete image files with thumbnail
                    PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Service);
                    manager.Delete(item.FileName, true);

                    //delete image from db
                    db.ServiceImages.Remove(item);
                }
            }



            db.SubServices.Remove(subService);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "SubServices", new { id = companyservicesID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
