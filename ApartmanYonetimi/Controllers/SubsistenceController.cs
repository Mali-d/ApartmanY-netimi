using ApartmanYonetimi;
using ApartmanYonetimi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmanYonetimi.Controllers
{
    [Authorize]
    public class SubsistenceController : BaseController
    {
        // GET: Definition
        public ActionResult Index()
        {
            ViewBag.PersonId = new SelectList(db.Person.Where(x => x.IsActive == true && x.PersonTypeId> 2 && x.SiteId == siteId), "Id", "Name");
            return View(db.Subsistence.Where(x => x.IsActive == true && x.SiteId == siteId));
        }

        public ActionResult _CreateEdit(int? id)
        {
            if (id != null)
            {
                var subsistence = db.Subsistence.Find(id);
                ViewBag.PersonId = new SelectList(db.Person.Where(x => x.IsActive == true && x.PersonTypeId > 2 && x.SiteId == siteId), "Id", "Name", subsistence.PersonId);
                return PartialView(subsistence);
            }

            ViewBag.PersonId = new SelectList(db.Person.Where(x => x.IsActive == true && x.PersonTypeId > 2 && x.SiteId == siteId), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult _CreateEdit(Subsistence subsistence)
        {
            if (subsistence.Id > 0)
            {
                var subsistence1 = db.Subsistence.Find(subsistence.Id);
                subsistence1.PersonId = subsistence.PersonId;
                subsistence1.Money = subsistence.Money;
                subsistence1.Date = subsistence.Date;
                subsistence1.Description = subsistence.Description;
                subsistence1.ModifiedDate = DateTime.Now;
                subsistence1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            subsistence.SiteId = siteId;
            subsistence.IsActive = true;
            subsistence.CreatedDate = DateTime.Now;
            subsistence.CreatedUserName = "admin";
            db.Subsistence.Add(subsistence);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Subsistence.Find(id);
                    db2.Subsistence.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var subsistence = db.Subsistence.Find(id);
                subsistence.IsActive = false;
                subsistence.ModifiedDate = DateTime.Now;
                subsistence.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}