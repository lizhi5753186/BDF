using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Bdf.Sample.Web.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected Guid UserId
        {
            get
            {
                if (Session["UserId"] != null)
                    return (Guid)Session["UserId"];
                else
                {
                    var id = new Guid();
                    Session["UserId"] = id;
                    return id;
                }
            }
        }

        protected ActionResult RedirectToSuccess(string pageTitle, string action = "Index", string controller = "Home", int waitSeconds = 3)
        {
            return this.RedirectToAction("SuccessPage", "Home", new { pageTitle = pageTitle, retAction = action, retController = controller, waitSeconds = waitSeconds });
        }
    }
}