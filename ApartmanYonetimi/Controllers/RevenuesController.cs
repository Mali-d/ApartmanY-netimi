using ApartmanYonetimi;
using ApartmanYonetimi.Controllers;
using ApartmanYonetimi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmanYonetimi.Controllers
{
    [Authorize]
    public class RevenuesController : BaseController
    {
        public ActionResult Index(DateTime? startDate, DateTime? endDate,int? apartmentId)
        {
            startDate = startDate==null? DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01"):startDate;
            endDate = endDate == null? DateTime.Parse(DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-01").AddDays(-1): endDate;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId).Select(x=> new {Id=x.Id, Name=x.Block.Name + " - " + x.No }), "Id", "Name");
            ViewBag.RevenuesTypeId = new SelectList(db.RevenuesType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name");
            return View(db.Revenues.Where(x => x.IsActive == true && x.SiteId == siteId && (x.ApartmentId == (apartmentId !=null? apartmentId : x.ApartmentId)) && x.Date >= startDate && x.Date <= endDate).ToList());
        }

        public ActionResult _CreateEdit(int?id)
        {
            if (id!=null)
            {
                var revenues = db.Revenues.Find(id);
                ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.Block.Name + " - " + x.No }), "Id", "Name", revenues.ApartmentId);
                ViewBag.RevenuesTypeId = new SelectList(db.RevenuesType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name", revenues.RevenuesTypeId);
                return PartialView(revenues);
            }

            ViewBag.ApartmentId = new SelectList(db.Apartment.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.Block.Name + " - " + x.No }), "Id", "Name");
            ViewBag.RevenuesTypeId = new SelectList(db.RevenuesType, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult _CreateEdit(Revenues revenues)
        {
            if (revenues.Id>0)
            {
                var revenues1 = db.Revenues.Find(revenues.Id);
                revenues1.ApartmentId = revenues.ApartmentId;
                revenues1.RevenuesTypeId = revenues.RevenuesTypeId;
                revenues1.Description = revenues.Description;
                revenues1.Date = revenues.Date;
                revenues1.Money = revenues.Money;
                revenues1.IsRender = revenues.IsRender;
                revenues1.ModifiedDate = DateTime.Now;
                revenues1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            revenues.SiteId = siteId;
            revenues.CollectingStatus = RenevusCollectingStatusEnums.Odenmedi;
            revenues.IsActive = true;
            revenues.CreatedDate = DateTime.Now;
            revenues.CreatedUserName = "admin";
            db.Revenues.Add(revenues);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int id)
        {
            
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Revenues.Find(id);
                    db2.Revenues.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var revenues = db.Revenues.Find(id);
                revenues.IsActive = false;
                revenues.ModifiedDate = DateTime.Now;
                revenues.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult AllDelete(List<int> checkId)
        {
            if (checkId.Count()>0)
            {
                try
                {
                    using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                    {
                        foreach (var item in checkId)
                        {
                            var delete = db2.Revenues.Find(item);
                            db2.Revenues.Remove(delete);
                        }
                        db2.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    //var revenues = db.Revenues.Find(id);
                    //revenues.IsActive = false;
                    //revenues.ModifiedDate = DateTime.Now;
                    //revenues.ModifyUserName = "admin";
                    //db.SaveChanges();
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult AllCreate(Revenues revenues,bool HouseSize)
        {
            foreach (var item in db.Apartment.Where(x=>x.IsActive==true && x.SiteId== siteId).ToList())
            {
                var money = HouseSize==true? Convert.ToDecimal((revenues.Money * (decimal)((decimal)item.HouseSize / (decimal)100)).ToString("N2")): revenues.Money;
                db.Revenues.Add(new Revenues
                {
                    ApartmentId = item.Id,
                    SiteId = siteId,
                    RevenuesTypeId = revenues.RevenuesTypeId,
                    Description = revenues.Description,
                    Date = revenues.Date,
                    Money = money,
                    IsRender = revenues.IsRender,
                    CollectingStatus = RenevusCollectingStatusEnums.Odenmedi,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedUserName = "admin"
                });
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult ShareAllCreate(Revenues revenues, bool HouseSize)
        {
            var apartment = db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId).ToList();
            var apartmnetCount = apartment.Count();
            var totalHouseSize = apartment.Sum(x=>x.HouseSize);
            foreach (var item in apartment)
            {
                decimal money = HouseSize == true ? Convert.ToDecimal((revenues.Money * (decimal)((decimal)item.HouseSize / (decimal)totalHouseSize)).ToString("N2")) : Convert.ToDecimal(Convert.ToDecimal(revenues.Money) / Convert.ToDecimal(apartmnetCount));
                db.Revenues.Add(new Revenues
                {
                    ApartmentId = item.Id,
                    SiteId = siteId,
                    RevenuesTypeId = revenues.RevenuesTypeId,
                    Description = revenues.Description,
                    Date = revenues.Date,
                    Money = RaundDecimalUp(money),
                    IsRender = revenues.IsRender,
                    CollectingStatus = RenevusCollectingStatusEnums.Odenmedi,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedUserName = "admin"
                });
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult MultipleCreate(Revenues revenues, List<string> ids)
        {

            foreach (var item in ids)
            {
                var row = item.Split(';');
                db.Revenues.Add(new Revenues
                {
                    ApartmentId = Convert.ToInt32(row[0]),
                    SiteId = siteId,
                    RevenuesTypeId = revenues.RevenuesTypeId,
                    Description = revenues.Description,
                    Date = revenues.Date,
                    Money = Convert.ToDecimal(row[1]),
                    IsRender = revenues.IsRender,
                    CollectingStatus = RenevusCollectingStatusEnums.Odenmedi,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedUserName = "admin"
                });
            }
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult YearCreate(Revenues revenues, bool HouseSize, int Months)
        {
            for (int i = 0; i < Months; i++)
            {
                DateTime date = i == 0 ? revenues.Date : revenues.Date.AddMonths(+i);
                foreach (var item in db.Apartment.Where(x => x.IsActive == true && x.SiteId == siteId).ToList())
                {
                    var money = HouseSize == true ? Convert.ToDecimal((revenues.Money * (decimal)((decimal)item.HouseSize / (decimal)100)).ToString("N2")) : revenues.Money;
                    db.Revenues.Add(new Revenues
                    {
                        ApartmentId = item.Id,
                        SiteId = siteId,
                        RevenuesTypeId = revenues.RevenuesTypeId,
                        Description = revenues.Description,
                        Date = date,
                        Money = money,
                        IsRender = revenues.IsRender,
                        CollectingStatus = RenevusCollectingStatusEnums.Odenmedi,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserName = "admin"
                    });
                }
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Table(int? year, int? month)
        {
            year = year != null ? year : DateTime.Now.Year;
            month = month != null ? month : DateTime.Now.Month;
            var startDate = DateTime.Parse(year.ToString()+"-" + month.ToString() + "-01");
            var yearStr = month == 12 ? (year + 1).ToString() : year.ToString();
            var monthStr = month != null ? (month==12?"1": (month + 1).ToString()) : DateTime.Now.Month.ToString();
            var endDate = DateTime.Parse(yearStr + "-" + monthStr + "-01");

            var revenues = db.Revenues.Where(x => x.IsActive == true && x.SiteId == siteId).ToList();
            List<ApartmentDebt> apartmentDebts = revenues.Where(x=>x.IsActive==true && x.SiteId==siteId && x.Date <= startDate).Select(x => new ApartmentDebt { ApartmentId = x.ApartmentId, ApartmentNo = (x.Apartment.Block!=null? x.Apartment.Block.Name + " - " :"" ) + x.Apartment.No, Date = x.Date, Id = x.Id, Money = x.Money, IsRender = x.IsRender, Description = x.RevenuesType.Name + (x.Description != null ? " (" + x.Description + ")" : "") }).ToList();
            List<ApartmentDebt> collectings = db.Collecting.Where(x => x.IsActive == true && x.SiteId == siteId).Select(x => new ApartmentDebt { ApartmentId = x.ApartmentId, ApartmentNo = x.Apartment.Block.Name + " - " + x.Apartment.No, Date = x.Date, Id = x.Id, Money = x.Money * -1, IsRender = x.IsRender, Description = "Ödeme" }).ToList();
            apartmentDebts.AddRange(collectings);
            ViewBag.ApartmentDebt = apartmentDebts;

            ViewBag.Revenues = revenues.Where(x => x.Date>=startDate&&x.Date<endDate).ToList();
            ViewBag.RevenuesDate = startDate;
            return View(db.Apartment.Where(x=>x.SiteId == siteId).OrderBy(x=>x.Block.Name).ThenBy(x=>x.No).Where(x=>x.IsActive==true).ToList());
        }

        public ActionResult ApartmentDebt(int id)
        {
            List<ApartmentDebt> apartmentDebts = db.Revenues.Where(x => x.IsActive==true && x.ApartmentId == id).Select(x=> new ApartmentDebt { ApartmentId=x.ApartmentId, ApartmentNo=x.Apartment.Block.Name + " - "+ x.Apartment.No, Date =x.Date, Id=x.Id, Money=x.Money, IsRender=x.IsRender, Description=x.RevenuesType.Name + (x.Description != null ?" ("+ x.Description + ")" : "") }).ToList();
            List<ApartmentDebt> collectings = db.Collecting.Where(x => x.IsActive==true && x.ApartmentId == id).Select(x=> new ApartmentDebt { ApartmentId = x.ApartmentId, ApartmentNo = x.Apartment.Block.Name + " - " + x.Apartment.No, Date =x.Date, Id=x.Id, Money=x.Money*-1,IsRender=x.IsRender, Description= "Ödeme" }).ToList();
            apartmentDebts.AddRange(collectings);
            return View(apartmentDebts.OrderBy(x=>x.Date).ToList());
        }

        public decimal  RaundDecimalUp(decimal number)
        {
            if (number.ToString().Contains(","))
            {
                int numberInt = Convert.ToInt32(number.ToString().Split(',')[0]);
                decimal numberDecimal = Convert.ToDecimal(number.ToString().Split(',')[1]);
                numberInt = numberDecimal > 5 ? numberInt + 1 : numberInt;
                numberDecimal = numberDecimal > 5 ? 0 : Convert.ToDecimal(1) / Convert.ToDecimal(2);
                return Convert.ToDecimal(numberInt) + Convert.ToDecimal(numberDecimal);
            }

            return number;
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
        }

    }
}