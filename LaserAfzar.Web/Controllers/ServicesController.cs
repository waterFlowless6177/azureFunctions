using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaserAfzar.Web.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Details(int id)
        {
            ViewBag.serviceId = id;
            return View();
        }
    }
}