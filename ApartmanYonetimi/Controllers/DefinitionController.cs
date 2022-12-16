using ApartmanYonetimi;
using ApartmanYonetimi.Controllers;
using ApartmanYonetimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ApartmanYonetimi.Controllers
{
    [Authorize]
    public class DefinitionController : BaseController
    {
        // GET: Definition
        public ActionResult  SiteIndex()
        {
            return View(db.Site.ToList());
        }

        public ActionResult _SiteCreateEdit(int? id)
        {
            if (id != null)
            {
                return PartialView(db.Site.Find(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult _SiteCreateEdit(Site site,  HttpPostedFileBase Photo)
        {
            string fileName = null;
            if (Photo!=null)
            {
                fileName = DateTime.Now.Ticks.ToString() + "." + Photo.FileName.Split('.').Last();
                string filePath = HttpContext.Server.MapPath("~/Content/images/" + fileName);
                Photo.SaveAs(filePath);
                site.PhotoName = fileName;
            }
            if (site.Id > 0)
            {
                var site1 = db.Site.Find(site.Id);
                site1.Name = site.Name;
                site1.Domain = site.Domain;
                site1.PhotoName = (fileName !=null? fileName:site1.PhotoName);
                site1.EndDate = site.EndDate;
                site1.ModifiedDate = DateTime.Now;
                site1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            site.IsActive = true;
            site.CreatedDate = DateTime.Now;
            site.CreatedUserName = "admin";
            db.Site.Add(site);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SiteDelete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Site.Find(id);
                    db2.Site.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var site = db.Site.Find(id);
                site.IsActive = false;
                site.ModifiedDate = DateTime.Now;
                site.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SiteManagerIndex()
        {
            ViewBag.SiteId = new SelectList(db.Site.Where(x => x.IsActive == true).ToList(), "Id", "Name");
            return View(db.Person.Where(x => x.PersonTypeId == PersonTypesEnums.Yonetici).ToList());
        }

        public ActionResult _SiteManagerCreateEdit(int? id)
        {
            ViewBag.SiteId = new SelectList(db.Site.Where(x => x.IsActive == true).ToList(), "Id", "Name");
            if (id != null)
            {
                var person = db.Person.Find(id);
                ViewBag.SiteId = new SelectList(db.Site.Where(x => x.IsActive == true).ToList(), "Id", "Name", person.SiteId);
                return PartialView();
            }
            return View();
        }
        [HttpPost]
        public ActionResult _SiteManagerCreateEdit(Person person)
        {
            if (person.Id > 0)
            {
                var person1 = db.Person.Find(person.Id);
                person1.SiteId = person.SiteId;
                person1.Name = person.Name;
                person1.PhoneNumber = person.PhoneNumber;
                person1.StartDate = person.StartDate;
                person1.ModifiedDate = DateTime.Now;
                person1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            person.StartDate = DateTime.Now;
            person.PersonTypeId = PersonTypesEnums.Yonetici;
            person.IsActive = true;
            person.CreatedDate = DateTime.Now;
            person.CreatedUserName = "admin";
            db.Person.Add(person);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SiteManagerDelete(int? id)
        {
            var delete = db.Person.Find(id);
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    db2.Person.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                delete.IsActive = false;
                delete.ModifiedDate = DateTime.Now;
                delete.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult RevenuesTypeIndex()
        {
            return View(db.RevenuesType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList());
        }

        public ActionResult _RevenuesTypeCreateEdit(int? id)
        {
            if (id != null)
            {
                return PartialView(db.RevenuesType.Find(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult _RevenuesTypeCreateEdit(RevenuesType revenuesType)
        {
 
            if (revenuesType.Id > 0)
            {
                var revenuesType1 = db.RevenuesType.Find(revenuesType.Id);
                revenuesType1.Name = revenuesType.Name;
                revenuesType1.ModifiedDate = DateTime.Now;
                revenuesType1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            revenuesType.SiteId = siteId;
            revenuesType.SiteType = GeneralTypesEnums.SiteyeOzel;
            revenuesType.IsActive = true;
            revenuesType.CreatedDate = DateTime.Now;
            revenuesType.CreatedUserName = "admin";
            db.RevenuesType.Add(revenuesType);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult RevenuesTypeDelete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.RevenuesType.Find(id);
                    db2.RevenuesType.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var update = db.RevenuesType.Find(id);
                update.IsActive = false;
                update.ModifiedDate = DateTime.Now;
                update.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PaymentTypeIndex()
        {
            return View(db.PaymentType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList());
        }

        public ActionResult _PaymentTypeCreateEdit(int? id)
        {
            if (id != null)
            {
                return PartialView(db.PaymentType.Find(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult _PaymentTypeCreateEdit(PaymentType paymentType)
        {

            if (paymentType.Id > 0)
            {
                var paymentType1 = db.PaymentType.Find(paymentType.Id);
                paymentType1.Name = paymentType.Name;
                paymentType1.ModifiedDate = DateTime.Now;
                paymentType1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            paymentType.SiteId = siteId;
            paymentType.SiteType = GeneralTypesEnums.SiteyeOzel;
            paymentType.IsActive = true;
            paymentType.CreatedDate = DateTime.Now;
            paymentType.CreatedUserName = "admin";
            db.PaymentType.Add(paymentType);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PaymentTypeDelete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.PaymentType.Find(id);
                    db2.PaymentType.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var update = db.PaymentType.Find(id);
                update.IsActive = false;
                update.ModifiedDate = DateTime.Now;
                update.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PersonTypeIndex()
        {
            return View(db.PersonType.Where(x => x.IsActive == true && x.Type == 2 && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList());
        }

        public ActionResult _PersonTypeCreateEdit(int? id)
        {
            if (id != null)
            {
                return PartialView(db.PersonType.Find(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult _PersonTypeCreateEdit(PersonType personType)
        {

            if (personType.Id > 0)
            {
                var personType1 = db.PersonType.Find(personType.Id);
                personType1.Name = personType.Name;
                personType1.ModifiedDate = DateTime.Now;
                personType1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            personType.SiteId = siteId;
            personType.SiteType = GeneralTypesEnums.SiteyeOzel;
            personType.Type = PersonTypesEnums.Personel;
            personType.IsActive = true;
            personType.CreatedDate = DateTime.Now;
            personType.CreatedUserName = "admin";
            db.PersonType.Add(personType);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PersonTypeDelete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.PersonType.Find(id);
                    db2.PersonType.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var update = db.PersonType.Find(id);
                update.IsActive = false;
                update.ModifiedDate = DateTime.Now;
                update.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SafeTypeIndex()
        {
            return View(db.SafeType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList());
        }

        public ActionResult _SafeTypeCreateEdit(int? id)
        {
            if (id != null)
            {
                return PartialView(db.SafeType.Find(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult _SafeTypeCreateEdit(SafeType safeType)
        {

            if (safeType.Id > 0)
            {
                var safeType1 = db.SafeType.Find(safeType.Id);
                safeType1.Name = safeType.Name;
                safeType1.ModifiedDate = DateTime.Now;
                safeType1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            safeType.SiteId = siteId;
            safeType.SiteType = GeneralTypesEnums.SiteyeOzel;
            
            safeType.IsActive = true;
            safeType.CreatedDate = DateTime.Now;
            safeType.CreatedUserName = "admin";
            db.SafeType.Add(safeType);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SafeTypeDelete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.SafeType.Find(id);
                    db2.SafeType.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var update = db.SafeType.Find(id);
                update.IsActive = false;
                update.ModifiedDate = DateTime.Now;
                update.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}