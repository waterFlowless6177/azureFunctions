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
    public class CompanyMembersController : Controller
    {
        private CompanyMemberDbContext db = new CompanyMemberDbContext();

        // GET: CompanyMembers
        public async Task<ActionResult> Index()
        {
            var companyMembers = db.CompanyMembers.Include(c => c.CompanyMemberImage);
            return View(await companyMembers.ToListAsync());
        }

        // GET: CompanyMembers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMember companyMember = await db.CompanyMembers.FindAsync(id);
            if (companyMember == null)
            {
                return HttpNotFound();
            }
            return View(companyMember);
        }

        // GET: CompanyMembers/Create
        public ActionResult Create()
        {
            ViewBag.CompanyMemberID = new SelectList(db.CompanyMemberImages, "CompanyMemberImageId", "Title");
            return View();
        }

        // POST: CompanyMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyMemberID,Name,JobTitle,Message")] CompanyMember companyMember)
        {
            if (ModelState.IsValid)
            {
                db.CompanyMembers.Add(companyMember);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyMemberID = new SelectList(db.CompanyMemberImages, "CompanyMemberImageId", "Title", companyMember.CompanyMemberID);
            return View(companyMember);
        }

        // GET: CompanyMembers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMember companyMember = await db.CompanyMembers.FindAsync(id);
            if (companyMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyMemberID = new SelectList(db.CompanyMemberImages, "CompanyMemberImageId", "Title", companyMember.CompanyMemberID);
            return View(companyMember);
        }

        // POST: CompanyMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyMemberID,Name,JobTitle,Message")] CompanyMember companyMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyMemberID = new SelectList(db.CompanyMemberImages, "CompanyMemberImageId", "Title", companyMember.CompanyMemberID);
            return View(companyMember);
        }

        // GET: CompanyMembers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMember companyMember = await db.CompanyMembers.FindAsync(id);
            if (companyMember == null)
            {
                return HttpNotFound();
            }
            return View(companyMember);
        }

        // POST: CompanyMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyMember companyMember = await db.CompanyMembers.FindAsync(id);

            //delete member image id existed
            if(companyMember.CompanyMemberImage != null)
            {
                //delete image file first 
                PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Member);
                manager.Delete(companyMember.CompanyMemberImage.FileName, false);

                //delete image from db
                db.CompanyMemberImages.Remove(companyMember.CompanyMemberImage);
            }

            db.CompanyMembers.Remove(companyMember);
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
