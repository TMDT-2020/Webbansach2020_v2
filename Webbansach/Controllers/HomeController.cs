using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;


namespace Webbansach.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Send(Gmail gmail)
        {
            gmail.SendMail();
            return View();
        }

    }
}