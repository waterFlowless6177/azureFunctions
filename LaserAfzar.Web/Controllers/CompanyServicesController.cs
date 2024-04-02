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
    [Authorize]
    public class CompanyServicesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: CompanyServices
        public async Task<ActionResult> Index()
        {
            var companyServices = db.CompanyServices.Include(c => c.ServiceTitleImage);
            return View(await companyServices.ToListAsync());
        }

        // GET: CompanyServices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            if (companyService == null)
            {
                return HttpNotFound();
            }
            return View(companyService);
        }

        // GET: CompanyServices/Create
        public ActionResult Create()
        {
            ViewBag.CompanyServiceId = new SelectList(db.ServiceTitleImages, "ServiceTitleImageId", "Title");
            return View();
        }

        // POST: CompanyServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyServiceId,Title,BriefDescription,FullDescription")] CompanyService companyService)
        {
            if (ModelState.IsValid)
            {
                db.CompanyServices.Add(companyService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyServiceId = new SelectList(db.ServiceTitleImages, "ServiceTitleImageId", "Title", companyService.CompanyServiceId);
            return View(companyService);
        }

        // GET: CompanyServices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            if (companyService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceId = new SelectList(db.ServiceTitleImages, "ServiceTitleImageId", "Title", companyService.CompanyServiceId);
            return View(companyService);
        }

        // POST: CompanyServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyServiceId,Title,BriefDescription,FullDescription")] CompanyService companyService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyServiceId = new SelectList(db.ServiceTitleImages, "ServiceTitleImageId", "Title", companyService.CompanyServiceId);
            return View(companyService);
        }

        // GET: CompanyServices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            if (companyService == null)
            {
                return HttpNotFound();
            }
            return View(companyService);
        }

        // POST: CompanyServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            //delete service image title
            if (companyService.ServiceTitleImage != null)
            {
                PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.ServiceTitle);
                manager.Delete(companyService.ServiceTitleImage.FileName, false);

                db.ServiceTitleImages.Remove(companyService.ServiceTitleImage);
            }

            //delete all subservices + subservices images
            if (companyService.SubServices != null)
            {
                List<SubService> subServicesList = companyService.SubServices.ToList();
                PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Service);
                foreach (var item in subServicesList)
                {
                    //remove all subservice images
                    List<ServiceImage> serviceImagesList = item.ServiceImage.ToList();
                    foreach (var imageItem in serviceImagesList)
                    {
                        //remove subservice image file
                        manager.Delete(imageItem.FileName, true);

                        //remove subservice image from db
                        db.ServiceImages.Remove(imageItem);
                    }

                    //remove subservice
                    db.SubServices.Remove(item);
                }
            }
            

            db.CompanyServices.Remove(companyService);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
