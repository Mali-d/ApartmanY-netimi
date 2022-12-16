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
    public class CompanyController : BaseController
    {
        // GET: Definition
        public ActionResult Index()
        {
            return View(db.Company.Where(x => x.IsActive == true && x.SiteId == siteId));
        }

        public ActionResult _CreateEdit(int?id)
        {
            if (id!=null)
            {
                var company = db.Company.Find(id);                
                return PartialView(company);
            }

            return View();
        }
        [HttpPost]
        public ActionResult _CreateEdit(Company company)
        {
            if (company.Id>0)
            {
                var company1 = db.Company.Find(company.Id);
                company1.Name = company.Name;
                company1.Adress = company.Adress;
                company1.PersonName = company.PersonName;
                company1.PhoneNumber = company.PhoneNumber;
                company1.ModifiedDate = DateTime.Now;
                company1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            company.SiteId = siteId;
            company.IsActive = true;
            company.CreatedDate = DateTime.Now;
            company.CreatedUserName = "admin";
            db.Company.Add(company);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int?id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Company.Find(id);
                    db2.Company.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var company = db.Company.Find(id);
                company.IsActive = false;
                company.ModifiedDate = DateTime.Now;
                company.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}