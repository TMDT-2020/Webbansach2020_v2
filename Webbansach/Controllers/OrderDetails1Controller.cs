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
    public class OrderDetails1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails1
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.SanPham);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails1/Details/5
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
        public ActionResult Editor(int id)
        {
            var oder = new Order().ViewDT(id);
            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.ID == id).ToList();

            return View(orderDetails);
        }

        [HttpPost]
        public ActionResult Editor(Order order)
        {
            if (ModelState.IsValid)
            {
                var ud = new Order();

                var result = ud.Updatestt2(order);
                if (result)
                {
                    return RedirectToAction("Index", "OrderDetails1");
                }
                else
                {
                    ModelState.AddModelError("", "Thay doi");
                }
            }
            return RedirectToAction("Index", "OrderDetails1");
        }

        public ActionResult Editor2(int id)
        {
            var oder = new Order().ViewDT(id);
            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.ID == id).ToList();

            return View(orderDetails);
        }

        [HttpPost]
        public ActionResult Editor2(Order order)
        {
            if (ModelState.IsValid)
            {
                var ud = new Order();

                var result = ud.Updatestt4(order);
                if (result)
                {
                    return RedirectToAction("Index", "OrderDetails1");
                }
                else
                {
                    ModelState.AddModelError("", "Thay doi");
                }
            }
            return RedirectToAction("Index", "OrderDetails1");
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
                    return RedirectToAction("Index", "OrderDetails1");
                }
                else
                {
                    ModelState.AddModelError("", "Thay doi");
                }
            }
            return RedirectToAction("Index", "OrderDetails1");
        }

        public ActionResult Editor4(int id)
        {
            var oder = new Order().ViewDT(id);
            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.ID == id).ToList();

            return View(orderDetails);
        }

        [HttpPost]
        public ActionResult Editor4(Order order)
        {
            if (ModelState.IsValid)
            {
                var ud = new Order();

                var result = ud.Updatestt(order);
                if (result)
                {
                    return RedirectToAction("Index", "OrderDetails1");
                }
                else
                {
                    ModelState.AddModelError("", "Thay doi");
                }
            }
            return RedirectToAction("Index", "OrderDetails1");
        }

        // GET: OrderDetails1/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.orders, "ID", "OrderName");
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP");
            return View();
        }

        // POST: OrderDetails1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderID,SanPhamID,Gia,SoLuong")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.orders, "ID", "OrderName", orderDetail.OrderID);
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP", orderDetail.SanPhamID);
            return View(orderDetail);
        }

        // GET: OrderDetails1/Edit/5
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

        // POST: OrderDetails1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderID,SanPhamID,Gia,SoLuong")] OrderDetail orderDetail)
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

        // GET: OrderDetails1/Delete/5
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

        // POST: OrderDetails1/Delete/5
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
