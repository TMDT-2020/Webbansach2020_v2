using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult AssignRole()
        {
            return View();
        }
        public ActionResult QuanLi()
        {
            return View();
        }
        public ActionResult QuanLiDoanhThu()
        {
            return View();
        }
        public ActionResult ChoXetDuyet()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
        public ActionResult DonHuy()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
        public ActionResult DangGiao()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
        public ActionResult DaGiao()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
        public ActionResult ChuaThanhToan()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
        public ActionResult DaThanhToan()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
    }
}