using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class AccountManagerController : Controller
    {
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult TheoDoiDonHang()
        {
            return View();
        }
    }
}