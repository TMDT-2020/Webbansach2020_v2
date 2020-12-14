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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.comments.Include(c => c.SanPham);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        [HttpPost]
        public ActionResult TaoComment(int productId, int UserID, Comment comment2)
        {
            var product = db.sanPhams.Find(productId);
            var user = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == user); 
            Comment comment = new Comment()
            {
                UserID = currentUser.Id,
                NoiDung = comment2.NoiDung,
                SanPhamID = product.ID

            };
            db.comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Details", "SanPhams");

        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NoiDung,SanPhamID,UserID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP", comment.SanPhamID);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP", comment.SanPhamID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NoiDung,SanPhamID,UserID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SanPhamID = new SelectList(db.sanPhams, "ID", "TenSP", comment.SanPhamID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
