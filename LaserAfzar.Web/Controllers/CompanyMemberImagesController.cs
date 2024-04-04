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
    public class CompanyMemberImagesController : Controller
    {
        private CompanyMemberDbContext db = new CompanyMemberDbContext();

        // GET: CompanyMemberImages
        public async Task<ActionResult> Index()
        {
            var companyMemberImages = db.CompanyMemberImages.Include(c => c.CompanyMember);
            return View(await companyMemberImages.ToListAsync());
        }

        // GET: CompanyMemberImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMemberImage companyMemberImage = await db.CompanyMemberImages.FindAsync(id);
            if (companyMemberImage == null)
            {
                return HttpNotFound();
            }
            return View(companyMemberImage);
        }

        // GET: CompanyMemberImages/Create
        public ActionResult Create(int id)
        {
            //ViewBag.CompanyMemberImageId = new SelectList(db.CompanyMembers, "CompanyMemberID", "Name");
            ViewBag.CompanyMemberImageId = id;
            ViewBag.ImageType = PhotoManagerType.Member;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Member);
            return View();
        }

        // POST: CompanyMemberImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyMemberImageId,Title,FileName")] CompanyMemberImage companyMemberImage)
        {
            if (ModelState.IsValid)
            {
                db.CompanyMemberImages.Add(companyMemberImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyMembers");
            }

            ViewBag.CompanyMemberImageId = new SelectList(db.CompanyMembers, "CompanyMemberID", "Name", companyMemberImage.CompanyMemberImageId);
            return View(companyMemberImage);
        }

        // GET: CompanyMemberImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMemberImage companyMemberImage = await db.CompanyMemberImages.FindAsync(id);
            if (companyMemberImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyMemberImageId = new SelectList(db.CompanyMembers, "CompanyMemberID", "Name", companyMemberImage.CompanyMemberImageId);
            ViewBag.ImageType = PhotoManagerType.Member;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Member);
            return View(companyMemberImage);
        }

        // POST: CompanyMemberImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyMemberImageId,Title,FileName")] CompanyMemberImage companyMemberImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyMemberImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyMembers");
            }
            ViewBag.CompanyMemberImageId = new SelectList(db.CompanyMembers, "CompanyMemberID", "Name", companyMemberImage.CompanyMemberImageId);
            return View(companyMemberImage);
        }

        // GET: CompanyMemberImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMemberImage companyMemberImage = await db.CompanyMemberImages.FindAsync(id);
            if (companyMemberImage == null)
            {
                return HttpNotFound();
            }

            ViewBag.ImageType = PhotoManagerType.Member;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Member);
            return View(companyMemberImage);
        }

        // POST: CompanyMemberImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteFileName)
        {
            //delete image file without thumbnail
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Member);
            manager.Delete(deleteFileName, false);

            CompanyMemberImage companyMemberImage = await db.CompanyMemberImages.FindAsync(id);
            db.CompanyMemberImages.Remove(companyMemberImage);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "CompanyMembers");
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
