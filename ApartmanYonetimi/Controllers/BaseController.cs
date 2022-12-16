using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ApartmanYonetimi.Controllers
{

    public class BaseController : Controller
    {
        public ApartmanYonetimiEntities db = new ApartmanYonetimiEntities();
        public int siteId;
        // GET: Base
        public BaseController()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (userId!=null)
            {
                if (userId!="c7e6b42d-1c94-497c-a0e9-60d47a34f55d")
                {
                    var user = db.Person.FirstOrDefault(x => x.AspNetUserId == userId);
                    siteId = user.SiteId;
                }
                else
                {
                    siteId = 3;
                }
   
            }
            else
            {
                siteId = 3;
            }
       
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
           //var userId= requestContext.HttpContext.User.Identity.GetUserId();
           // var user = db.Person.FirstOrDefault(x => x.AspNetUserId == userId);
           // siteId = user.SiteId;
            //if (requestContext.HttpContext.Session["UserId"] == null)
            //{
            //    requestContext.HttpContext.Response.Clear();
            //    requestContext.HttpContext.Response.Redirect(Url.Action("Login", "Account"));
            //    requestContext.HttpContext.Response.End();
            //}
            //else
            //{
            //    siteId = Convert.ToInt32(Session["UserId"]);
            //}
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
        }
    }
}