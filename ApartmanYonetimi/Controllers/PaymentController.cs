using ApartmanYonetimi;
using ApartmanYonetimi.Controllers;
using ApartmanYonetimi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmanYonetimi.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        // GET: Definition
        public ActionResult Index()
        {
            ViewBag.CompanyId = new SelectList(db.Company.Where(x => x.IsActive == true && x.SiteId == siteId), "Id", "Name");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentType.Where(x=> x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name");
            return View(db.Payment.OrderByDescending(x=>x.Date).Where(x=>x.IsActive==true && x.SiteId == siteId));
        }

        public ActionResult _CreateEdit(int? id)
        {
            if (id != null)
            {
                var payment = db.Payment.Find(id);
                ViewBag.CompanyId = new SelectList(db.Company.Where(x => x.IsActive == true && x.SiteId == siteId), "Id", "Name", payment.CompanyId);
                ViewBag.PaymentTypeId = new SelectList(db.PaymentType.Where(x => x.IsActive == true && (x.SiteType == GeneralTypesEnums.Genel || x.SiteId == siteId)).ToList(), "Id", "Name", payment.CompanyId);
                return PartialView(payment);
            }

            ViewBag.PersonId = new SelectList(db.Person.Where(x => x.IsActive == true), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult _CreateEdit(Payment payment, HttpPostedFileBase file)
        {
            if (file!=null)
            {
                string filename = file.FileName;
                string path = Guid.NewGuid().ToString() + "." + filename.Split('.').Last();
                file.SaveAs(Server.MapPath("~/Uploads/_" + path));
                Image image = Image.FromFile(Server.MapPath("~/Uploads/_" + path));

                int width = 0;
                int height = 0;
                int X = image.Width;
                int Y = image.Height;


                if (X >= Y)
                {
                    width = 550;
                    height = Y / (image.Width / width);
                }
                else
                {
                    height = 550;
                    width = X / (image.Height / height);
                }

                Image image2 = resizeImage(image, new Size { Height = height, Width = width });
                image2.Save(Server.MapPath("~/Uploads/" + path));
                image.Dispose();
                System.IO.File.Delete(Server.MapPath("~/Uploads/_" + path));
            }

            var userId = User.Identity.GetUserId();
            var person = db.Person.FirstOrDefault(x => x.AspNetUserId == userId);
            if (payment.Id > 0)
            {
                var payment1 = db.Payment.Find(payment.Id);
                payment1.CreatePersonId = person.Id;
                payment1.CompanyId = payment.CompanyId;
                payment1.PaymentTypeId = payment.PaymentTypeId;
                payment1.Money = payment.Money;
                payment1.Date = payment.Date;
                payment1.Description = payment.Description;
                payment1.ModifiedDate = DateTime.Now;
                payment1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            payment.SiteId = siteId;
            payment.CreatePersonId = person.Id;
            payment.IsActive = true;
            payment.CreatedDate = DateTime.Now;
            payment.CreatedUserName = "admin";
            db.Payment.Add(payment);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Payment.Find(id);
                    db2.Payment.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var payment = db.Payment.Find(id);
                payment.IsActive = false;
                payment.ModifiedDate = DateTime.Now;
                payment.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

    }
}