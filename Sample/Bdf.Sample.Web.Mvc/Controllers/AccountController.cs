using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bdf.Sample.Web.Mvc.ViewModels;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.User;

namespace Bdf.Sample.Web.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}