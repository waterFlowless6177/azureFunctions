using LaserAfzar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaserAfzar.Web.Controllers
{
    [Authorize]
    public class ProjectImagesController : Controller
    {
        // GET: ProjectImages
        public ActionResult Create()
        {
            return View();
        }



        //upload async action
        public ActionResult AsyncUpload()
        {
            return View();
        }

        //upload async action post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncUpload(IEnumerable<HttpPostedFileBase> files, PhotoManagerType imageType)
        {
            PhotoFileManager manager = new PhotoFileManager(files, imageType);

            if(imageType == PhotoManagerType.Service)
                return manager.Upload(true, 300);

            return manager.Upload(false, null);
        }

        public ActionResult AsyncDelete()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncDelete(string deleteFileName, PhotoManagerType DeleteImageType)
        {
            PhotoFileManager manager = new PhotoFileManager(DeleteImageType);

            if (DeleteImageType == PhotoManagerType.Service)
                return manager.Delete(deleteFileName, true);

            return manager.Delete(deleteFileName, false);
        }

    }
}