using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmanYonetimi.Controllers
{
    [Authorize]
    public class ApartmentController : BaseController
    {
        // GET: Definition
        public ActionResult Index()
        {
            ViewBag.BlockId = new SelectList(db.Block.Where(x => x.IsActive == true && x.SiteId==siteId), "Id", "Name");
            return View(db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId));
        }

        public ActionResult _CreateEdit(int?id)
        {
            if (id!=null)
            {
                var apartment = db.Apartment.Find(id);
                ViewBag.BlockId = new SelectList(db.Block.Where(x => x.IsActive == true && x.SiteId==siteId), "Id", "Name", apartment.BlockId);
                return PartialView(apartment);
            }

            ViewBag.BlockId = new SelectList(db.Block.Where(x => x.IsActive == true), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult _CreateEdit(Apartment apartment)
        {
            if (apartment.Id>0)
            {
                var apartment1 = db.Apartment.Find(apartment.Id);
                apartment1.BlockId = apartment.BlockId;
                apartment1.HouseSize = apartment.HouseSize;
                apartment1.No = apartment.No.PadLeft(2,'0');
                apartment1.ModifiedDate = DateTime.Now;
                apartment1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            apartment.SiteId = siteId;
            apartment.IsActive = true;
            apartment.CreatedDate = DateTime.Now;
            apartment.CreatedUserName = "admin";
            db.Apartment.Add(apartment);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int?id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Apartment.Find(id);
                    db2.Apartment.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var apartment = db.Apartment.Find(id);
                apartment.IsActive = false;
                apartment.ModifiedDate = DateTime.Now;
                apartment.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult MultipleCreate(int? BlockId, int count, int houseSize)
        {
            string apartment;
            if (BlockId!=null)
            {
                apartment = db.Apartment.Where(x => x.BlockId == BlockId).OrderByDescending(x => x.No).Select(x => x.No).Where(i => SqlFunctions.IsNumeric(i) == 1).FirstOrDefault();
            }
            else
            {
                apartment = db.Apartment.Where(x => x.BlockId == BlockId).OrderByDescending(x => x.No).Select(x => x.No).Where(i => SqlFunctions.IsNumeric(i) == 1).FirstOrDefault();
            }
            int aptNo = 0;
            int.TryParse(apartment, out aptNo);

            var startNo = apartment != null ? aptNo : 0;
            for (int i = 1; i <= count; i++)
            {
                db.Apartment.Add(new Apartment
                {
                    SiteId = siteId,
                    BlockId = BlockId,
                    No = (Convert.ToInt32(startNo) + i).ToString().PadLeft(2, '0'),
                    HouseSize = houseSize,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedUserName = "admin",
                });
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}