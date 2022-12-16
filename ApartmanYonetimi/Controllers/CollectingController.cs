using ApartmanYonetimi;
using ApartmanYonetimi.Controllers;
using ApartmanYonetimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace ApartmanYonetimi.Controllers
{
    [Authorize]
    public class CollectingController : BaseController
    {
        // GET: Definition
        public ActionResult Index(DateTime? startDate, DateTime? endDate, int? apartmentId)
        {
            startDate = startDate == null ? DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01") : startDate;
            endDate = endDate == null ? DateTime.Parse(DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-01").AddDays(-1) : endDate;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.SafeTypeId = new SelectList(db.SafeType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name");
            ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId).Select(x => new { Id = x.Id, Name = x.Block.Name + " - " + x.No }), "Id", "Name");
            return View(db.Collecting.Where(x => x.IsActive == true && x.SiteId == siteId && (x.ApartmentId == (apartmentId != null ? apartmentId : x.ApartmentId)) && x.Date >= startDate && x.Date <= endDate).ToList());
        }

        public ActionResult _CreateEdit(int? id,int? RevenusId)
        {
            decimal TotalRenevues = 0;
            if (RevenusId!=null)
            {
                TotalRenevues = db.Revenues.FirstOrDefault(x => x.Id == RevenusId).Money;
            }
            else
            {
                TotalRenevues = db.Revenues.FirstOrDefault(x => x.Id == RevenusId).Money;
            }
            ViewBag.TotalRenevues = 0;
            if (id != null)
            {
                var collecting = db.Collecting.Find(id);
                ViewBag.SafeTypeId = new SelectList(db.SafeType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name", collecting.SafeTypeId);
                ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId).Select(x => new { Id = x.Id, Name = x.Block.Name + " - " + x.No }), "Id", "Name", collecting.ApartmentId);
                return PartialView(collecting);
            }

            ViewBag.SafeTypeId = new SelectList(db.SafeType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name");
            ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId).Select(x => new { Id = x.Id, Name = x.Block.Name + " - " + x.No }), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult _CreateEdit(Collecting collecting)
        {
            var revenues = db.Revenues.Where(x => x.SiteId == siteId && x.ApartmentId == collecting.ApartmentId && x.IsRender == collecting.IsRender).ToList();
            if (revenues.Sum(x=>x.Money)<collecting.Money)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (collecting.Id > 0)
            {
                var collecting1 = db.Collecting.Find(collecting.Id);
                collecting1.ApartmentId = collecting.ApartmentId;
                collecting1.Date = collecting.Date;
                collecting1.Money = collecting.Money;
                collecting1.Description = collecting.Description;
                collecting1.IsRender = collecting.IsRender;
                collecting1.SafeTypeId = collecting.SafeTypeId;
                collecting1.ModifiedDate = DateTime.Now;
                collecting1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            collecting.SiteId = siteId;
            collecting.IsActive = true;
            collecting.CreatedDate = DateTime.Now;
            collecting.CreatedUserName = "admin";
            db.Collecting.Add(collecting);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Collecting.Find(id);
                    db2.Collecting.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var collecting = db.Collecting.Find(id);
                collecting.IsActive = false;
                collecting.ModifiedDate = DateTime.Now;
                collecting.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Receipt(int id)
        {
            ViewBag.RevenuesTypeId = new SelectList(db.RevenuesType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name");
            ViewBag.PersonId = new SelectList(db.Person.Where(x => x.IsActive == true && x.PersonTypeId < 3 && x.SiteId == siteId).ToList(), "Id", "Name");
            ViewBag.SiteManagerName = db.Person.FirstOrDefault(x => x.PersonTypeId == GeneralPersonTypesEnums.Yonetici && x.IsActive == true && x.SiteId == siteId).Name;
            return View(db.Collecting.FirstOrDefault(x => x.Id == id && x.IsActive == true));
        }

        [HttpPost]
        public ActionResult _ReceiptCreate(Receipt receipt)
        {
            receipt.IsActive = true;
            receipt.CreatedDate = DateTime.Now;
            receipt.CreatedUserName = "admin";
            db.Receipt.Add(receipt);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ReceiptDelete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Receipt.Find(id);
                    db2.Receipt.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var receipt = db.Receipt.Find(id);
                receipt.IsActive = false;
                receipt.ModifiedDate = DateTime.Now;
                receipt.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult _ReceiptEdit(int id,string description)
        {
            var renevuesCollecting_RS = db.RenevuesCollecting_RS.Find(id);
            renevuesCollecting_RS.Description = description;
            renevuesCollecting_RS.ModifiedDate = DateTime.Now;
            renevuesCollecting_RS.ModifyUserName = "admin";
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult AddCollecting(Collecting collecting)
        {
            var person = db.Person.FirstOrDefault(x => x.ApartmentId == collecting.ApartmentId && x.SiteId == siteId && x.PersonTypeId == (collecting.IsRender == true ? GeneralPersonTypesEnums.Kiracı : GeneralPersonTypesEnums.EvSahibi));
            if (person==null)
            {
                ViewBag.Description = "Bu daireye " + (collecting.IsRender==true?"Kiracı":"Ev Sahibi") + " kaydı yapılmamış. Ödeme kaydedilemiyor.";
                return View("../Home/Error", null);
            }

            var renevues = db.Revenues.Where(x => x.IsActive == true && x.ApartmentId == collecting.ApartmentId && x.CollectingStatus < RenevusCollectingStatusEnums.Odendi);
            var renevuesKısmiOdendi = db.Revenues.Where(x => x.IsActive == true && x.ApartmentId == collecting.ApartmentId && x.CollectingStatus == RenevusCollectingStatusEnums.KısmiOdendi);
            var renevuesKısmiOdendiIds = renevuesKısmiOdendi.Select(x => x.Id).ToList();
            var totalRenevuesMoney = renevues.Sum(x => x.Money);
            if (renevuesKısmiOdendiIds.Count()>0)
            {
                totalRenevuesMoney = renevues.Sum(x => x.Money) - db.RenevuesCollecting_RS.Where(x => renevuesKısmiOdendiIds.Contains(x.RevenuesId)).Sum(x => x.Money);
            }
            if (totalRenevuesMoney < collecting.Money)
            {
                ViewBag.Description = "Ödeme tutarı ("+ collecting.Money+"TL) toplam borçdan("+ totalRenevuesMoney + " TL) daha fazla olamaz";
                return View("../Home/Error", null);
            }

            collecting.SiteId = siteId;
            collecting.IsActive = true;
            collecting.CreatedDate = DateTime.Now;
            collecting.CreatedUserName = "admin";
            db.Collecting.Add(collecting);
            db.SaveChanges();
            var collectingMoney = collecting.Money;
            foreach (var item in renevues)
            {
                var renevuesCollecting_Rs = db.RenevuesCollecting_RS.Where(x => x.RevenuesId == item.Id);
                var renevuesCollecting_RsMoney = renevuesCollecting_Rs.FirstOrDefault() != null ? renevuesCollecting_Rs.Sum(x => x.Money) : 0;

                if (collectingMoney >= (item.Money - renevuesCollecting_RsMoney))
                {
                    item.CollectingStatus = RenevusCollectingStatusEnums.Odendi;
                }
                else
                {
                    item.CollectingStatus = RenevusCollectingStatusEnums.KısmiOdendi;
                }
                collectingMoney = collectingMoney - (item.Money - renevuesCollecting_RsMoney);

                db.RenevuesCollecting_RS.Add(new RenevuesCollecting_RS
                {
                    Money = collectingMoney >= 0 ? (item.Money - renevuesCollecting_RsMoney) : (item.Money - renevuesCollecting_RsMoney) + collectingMoney,
                    RevenuesId = item.Id,
                    CollectingId = collecting.Id,
                    Description = collecting.Description,
                    PersonId = person.Id,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedUserName = "admin"
                });

                if (collectingMoney <= 0) break;
            }
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DeleteColecting(int id)
        {
            var collecting = db.Collecting.Find(id);
            var revenuesCollecting = db.RenevuesCollecting_RS.Where(x => x.CollectingId == id);
            List<int> revenuesId = revenuesCollecting.GroupBy(x => x.RevenuesId).Select(x=> x.Key).ToList();
            var renevues = db.Revenues.Where(x => revenuesId.Contains(x.Id));
            var collectingMoney = collecting.Money;
            foreach (var item in renevues)
            {
                if (item.CollectingStatus > RenevusCollectingStatusEnums.Odenmedi)
                {
                    var totalRenevuesCollecting_RsMoney = db.RenevuesCollecting_RS.Where(x => x.RevenuesId == item.Id).Sum(x=>x.Money);
                    var renevuesCollecting_Rs = db.RenevuesCollecting_RS.Where(x => x.RevenuesId == item.Id && x.CollectingId == id);
                    var renevuesCollecting_RsMoney = renevuesCollecting_Rs.FirstOrDefault() != null ? renevuesCollecting_Rs.Sum(x => x.Money) : 0;

                    if (totalRenevuesCollecting_RsMoney == renevuesCollecting_RsMoney)
                    {
                        item.CollectingStatus = RenevusCollectingStatusEnums.Odenmedi;
                    }
                    else
                    {
                        item.CollectingStatus = RenevusCollectingStatusEnums.KısmiOdendi;
                    }
              
                    collectingMoney = collectingMoney - (item.Money - renevuesCollecting_RsMoney);
                    foreach (var item2 in renevuesCollecting_Rs)
                    {
                        db.RenevuesCollecting_RS.Remove(item2);
                    }
                }
                    db.Collecting.Remove(collecting);
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}