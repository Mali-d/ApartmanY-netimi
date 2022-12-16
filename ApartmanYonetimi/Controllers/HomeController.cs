using ApartmanYonetimi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmanYonetimi.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            var site = db.Site.FirstOrDefault(x => x.Domain == Request.Url.Host);
            if (site == null)
            {
                site = db.Site.FirstOrDefault(x => x.Id == 2);
            }
            return View(site);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}