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
    public class ServiceImagesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: ServiceImages
        public async Task<ActionResult> Index(int id, int CompanyServiceId)
        {
            ViewBag.SubServiceId = id;
            ViewBag.CompanyServiceId = CompanyServiceId;
            return View(await db.ServiceImages.Where( s => s.SubService.SubServiceId == id).ToListAsync());
        }

        // GET: ServiceImages/Details/5
        public async Task<ActionResult> Details(int? id, int SubServiceId, int CompanyServiceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceImage serviceImage = await db.ServiceImages.FindAsync(id);
            if (serviceImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubServiceId = SubServiceId;
            ViewBag.CompanyServiceId = CompanyServiceId;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);
            return View(serviceImage);
        }

        // GET: ServiceImages/Create
        public ActionResult Create(int id, int CompanyServiceId)
        {
            ViewBag.SubServiceId = id;
            ViewBag.CompanyServiceId = CompanyServiceId;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);

            return View();
        }

        // POST: ServiceImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ServiceImageId,Title,FileName")] ServiceImage serviceImage, int SubServiceId, int CompanyServiceId)
        {
            if (ModelState.IsValid)
            {
                serviceImage.SubService = db.SubServices.Find(SubServiceId);
                db.ServiceImages.Add(serviceImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = SubServiceId, CompanyServiceId = CompanyServiceId });
            }

            return View(serviceImage);
        }

        // GET: ServiceImages/Edit/5
        public async Task<ActionResult> Edit(int? id, int SubServiceId, int CompanyServiceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceImage serviceImage = await db.ServiceImages.FindAsync(id);
            if (serviceImage == null)
            {
                return HttpNotFound();
            }

            ViewBag.SubServiceId = SubServiceId;
            ViewBag.CompanyServiceId = CompanyServiceId;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);
            return View(serviceImage);
        }

        // POST: ServiceImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServiceImageId,Title,FileName")] ServiceImage serviceImage, int SubServiceId, int CompanyServiceId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = SubServiceId, CompanyServiceId = CompanyServiceId } );
            }
            return View(serviceImage);
        }

        // GET: ServiceImages/Delete/5
        public async Task<ActionResult> Delete(int? id, int SubServiceId, int CompanyServiceId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceImage serviceImage = await db.ServiceImages.FindAsync(id);
            if (serviceImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubServiceId = SubServiceId;
            ViewBag.CompanyServiceId = CompanyServiceId;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);
            return View(serviceImage);
        }

        // POST: ServiceImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteFileName, int SubServiceId, int CompanyServiceId)
        {
            //delete image file first
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Service);
            manager.Delete(deleteFileName, true);

            ServiceImage serviceImage = await db.ServiceImages.FindAsync(id);
            db.ServiceImages.Remove(serviceImage);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = SubServiceId, CompanyServiceId = CompanyServiceId });
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
