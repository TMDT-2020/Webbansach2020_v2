using Microsoft.AspNet.Identity;
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
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index(Order order)
        {
            var user = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == user);

            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.Order.UserID == currentUser.Id).ToList();
            return View(orderDetails);
        }
        public ActionResult Index2()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }
        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }
        public ActionResult Editor3(int id)
        {
            var oder = new Order().ViewDT(id);
            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.ID == id).ToList();

            return View(orderDetails);
        }

        [HttpPost]
        public ActionResult Editor3(Order order)
        {
            if (ModelState.IsValid)
            {
                var ud = new Order();

                var result = ud.Updatestt3(order);
                if (result)
                {
                    return RedirectToAction("Index", "OrderDetails");
                }
                else
                {
                    ModelState.AddModelError("", "Thay doi");
                }
            }
            return RedirectToAction("Index", "OrderDetails");
        }
        // GET: OrderDetails/Create

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: OrderDetails/Edit/5




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.orders, "ID", "OrderName", orderDetail.OrderID);
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP", orderDetail.SanPhamID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.orders, "ID", "OrderName", orderDetail.OrderID);
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP", orderDetail.SanPhamID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
