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
    public class ServiceTitleImagesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: ServiceTitleImages
        public async Task<ActionResult> Index()
        {
            var serviceTitleImages = db.ServiceTitleImages.Include(s => s.CompanyService);
            return View(await serviceTitleImages.ToListAsync());
        }

        // GET: ServiceTitleImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTitleImage serviceTitleImage = await db.ServiceTitleImages.FindAsync(id);
            if (serviceTitleImage == null)
            {
                return HttpNotFound();
            }
            return View(serviceTitleImage);
        }

        // GET: ServiceTitleImages/Create
        public ActionResult Create(int id)
        {
            //ViewBag.ServiceTitleImageId = new SelectList(db.CompanyServices, "CompanyServiceId", "Title");
            ViewBag.ServiceTitleImageId = id;
            ViewBag.ImageType = PhotoManagerType.ServiceTitle;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.ServiceTitle);
            return View();
        }

        // POST: ServiceTitleImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ServiceTitleImageId,Title,FileName")] ServiceTitleImage serviceTitleImage)
        {
            if (ModelState.IsValid)
            {
                db.ServiceTitleImages.Add(serviceTitleImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyServices");
            }

            ViewBag.ServiceTitleImageId = new SelectList(db.CompanyServices, "CompanyServiceId", "Title", serviceTitleImage.ServiceTitleImageId);
            return View(serviceTitleImage);
        }

        // GET: ServiceTitleImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTitleImage serviceTitleImage = await db.ServiceTitleImages.FindAsync(id);
            if (serviceTitleImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceTitleImageId = new SelectList(db.CompanyServices, "CompanyServiceId", "Title", serviceTitleImage.ServiceTitleImageId);
            ViewBag.ImageType = PhotoManagerType.ServiceTitle;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.ServiceTitle);
            return View(serviceTitleImage);
        }

        // POST: ServiceTitleImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServiceTitleImageId,Title,FileName")] ServiceTitleImage serviceTitleImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceTitleImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyServices");
            }
            ViewBag.ServiceTitleImageId = new SelectList(db.CompanyServices, "CompanyServiceId", "Title", serviceTitleImage.ServiceTitleImageId);
            return View(serviceTitleImage);
        }

        // GET: ServiceTitleImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTitleImage serviceTitleImage = await db.ServiceTitleImages.FindAsync(id);
            if (serviceTitleImage == null)
            {
                return HttpNotFound();
            }


            ViewBag.ImageType = PhotoManagerType.ServiceTitle;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.ServiceTitle);
            return View(serviceTitleImage);
        }

        // POST: ServiceTitleImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteFileName)
        {
            //delete image file without thumbnail
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.ServiceTitle);
            manager.Delete(deleteFileName, false);

            ServiceTitleImage serviceTitleImage = await db.ServiceTitleImages.FindAsync(id);
            db.ServiceTitleImages.Remove(serviceTitleImage);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "CompanyServices");
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
