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
    public class BlockController : BaseController
    {
        // GET: Definition
        public ActionResult Index()
        {
            return View(db.Block.Where(x => x.IsActive == true && x.SiteId == siteId));
        }

        public ActionResult _CreateEdit(int? id)
        {
            if (id != null)
            {
                return PartialView(db.Block.Find(id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult _CreateEdit(Block block)
        {
            if (block.Id > 0)
            {
                var block1 = db.Block.Find(block.Id);
                block1.Name = block.Name;
                block1.ModifiedDate = DateTime.Now;
                block1.ModifyUserName = "admin";
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            block.SiteId = siteId;
            block.IsActive = true;
            block.CreatedDate = DateTime.Now;
            block.CreatedUserName = "admin";
            db.Block.Add(block);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                using (ApartmanYonetimiEntities db2 = new ApartmanYonetimiEntities())
                {
                    var delete = db2.Block.Find(id);
                    db2.Block.Remove(delete);
                    db2.SaveChanges();
                }
            }
            catch (Exception)
            {
                var block = db.Block.Find(id);
                block.IsActive = false;
                block.ModifiedDate = DateTime.Now;
                block.ModifyUserName = "admin";
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}