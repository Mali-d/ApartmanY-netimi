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
    public class ResidentController : BaseController
    {
        // GET: Definition
        public ActionResult Index()
        {
            ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive==true && x.SiteId == siteId).Select(x=> new {Id=x.Id,Name=(x.Block.Name + " - " + x.No) }), "Id", "Name");
            ViewBag.PersonTypeId = new SelectList(db.PersonType.Where(x => x.Id < 3), "Id", "Name");
            return View(db.Person.Where(x => x.IsActive == true && x.PersonTypeId < 3 && x.SiteId == siteId));
        }

        public ActionResult _CreateEdit(int? id)
        {
            if (id != null)
            {
                var person = db.Person.Find(id);
                ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = (x.Block.Name + " - " + x.No) }), "Id", "Name", person.ApartmentId);
                ViewBag.PersonTypeId = new SelectList(db.PersonType.Where(x => x.Id < 3), "Id", "Name", person.PersonTypeId);
                return PartialView(person);
            }

            ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = (x.Block.Name + " - " + x.No) }), "Id", "Name");
            ViewBag.PersonTypeId = new SelectList(db.PersonType.Where(x => x.Id < 3), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult _CreateEdit(Person person)
        {
            if (person.Id > 0)
            {
                var person1 = db.Person.Find(person.Id);
                person1.PersonTypeId = person.PersonTypeId;
                person1.Name = person.Name;
                person1.PhoneNumber = person.PhoneNumber;
                person1.StartDate = person.StartDate;
                person1.EndDate = person.EndDate;
                person1.ModifiedDate = DateTime.Now;
                person1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            person.SiteId = siteId;
            person.IsActive = true;
            person.CreatedDate = DateTime.Now;
            person.CreatedUserName = "admin";
            db.Person.Add(person);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Person.Find(id);
                    db2.Person.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var person = db.Person.Find(id);
                person.IsActive = false;
                person.ModifiedDate = DateTime.Now;
                person.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}