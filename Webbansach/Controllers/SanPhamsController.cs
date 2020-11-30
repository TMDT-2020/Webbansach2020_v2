using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;
using System.Net.Mail;
using System.Web.Helpers;

namespace Webbansach.Controllers
{
    public class SanPhamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SanPhams
        public ActionResult Index()
        {
            return View(db.sanPhams.ToList());
        }
        
        public ActionResult Searching(string searchString)
        {
            var Loc = from p in db.sanPhams
                      select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                Loc = Loc.Where(s => s.TenSP.Contains(searchString));
            }
            return View(Loc);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        public static string getString(string s)
        {
            if (s.Length > 20)
            {
                return s.Substring(0, 20) + "...";
            }
            else
                return s;
        }

        public ActionResult AddToCart(int productId, string url)
        {
            if(Session["cart"] == null)
            {
                List<Product> cart = new List<Product>();
                var product = db.sanPhams.Find(productId);
                cart.Add(new Product()
                {
                    SanPham = product,
                    SoLuong = 1
                });
                Session["cart"] = cart;

            }
            else
            {
                List<Product> cart = (List<Product>)Session["cart"];
                var product = db.sanPhams.Find(productId);
                foreach(var item in cart)
                {
                    if(item.SanPham.ID == productId)
                    {
                        int PrevQ = item.SoLuong;
                        cart.Remove(item);
                        cart.Add(new Product()
                        {
                            SanPham = product,
                            SoLuong = PrevQ + 1
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new Product()
                        {
                            SanPham = product,
                            SoLuong = 1
                        });
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Checkout");
        }
        public ActionResult Remove(int productId)
        {
            List<Product> cart = (List<Product>)Session["cart"];
            //var product = db.sanPhams.Find(productId);
            foreach(var item in cart)
            {
                if(item.SanPham.ID == productId)
                {
                    cart.Remove(item);
                    break;
                }

            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        public ActionResult DecreaseQ(int? productId)
        {
            if (Session["cart"] != null)
            {
                List<Product> cart = (List<Product>)Session["cart"];
                var product = db.sanPhams.Find(productId);
                foreach (var item in cart)
                {
                    if (item.SanPham.ID == productId)
                    {
                        int PrevQ = item.SoLuong;
                        if (PrevQ > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Webbansach.Models.Product()
                            {
                                SanPham = product,
                                SoLuong = PrevQ - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Checkout");
        }

        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetail()
        {
            return View();
        }
        public ActionResult ProcessOrder(int? productId, int? UserId, Gmail gmail)
        {
            List<Product> cart = (List<Product>)Session["cart"];
            var product = db.sanPhams.Find(productId);
            var user = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == user);

            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                Name = currentUser.UserName,
                Phone = currentUser.SDT,
                Email = currentUser.Email,
                Adress = currentUser.Adress,
                PaymentType = "Cash",
                Status = "Hoan thanh",
                UserID = currentUser.Id

            };
            db.orders.Add(order);
            db.SaveChanges();

            foreach(var item in cart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderID = order.ID,
                    SanPhamID = item.SanPham.ID,
                    Gia = Convert.ToInt32(item.SanPham.GiaKM),
                    SoLuong = item.SoLuong
                };
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            gmail = new Gmail()
            {
                To = currentUser.Email,
                Subject = "Hoa don dien tu",
                Body = ("Don hang " + order.ID + " Da dat thanh cong. Ban co the tra cuu don hang website!")
            };
            gmail.SendMail();


            return RedirectToAction("Index","OrderDetails");
        }

        public ActionResult HoanthanhOrder()
        {
            return View();
        }
        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaTG = new SelectList(db.tacGias, "ID", "TenTacGia");
            ViewBag.MaNXB = new SelectList(db.nXBs, "ID", "TenNXB");
            ViewBag.MaLoai = new SelectList(db.theLoais, "ID", "TenTheLoai");
            ViewBag.MaKM = new SelectList(db.khuyenMais, "ID", "TenKM", "PTKM");

            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenSP,MaTG,MaNXB,NamXB,MaLoai,MaKM,DanhGia,BinhLuan,Mota,ChieuCao,ChieuRong,SoTrang,SoLuongSach,HinhAnh,GiaSP,PTKM")] SanPham sanPham, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    string _file = Path.GetFileName(img.FileName);
                    sanPham.HinhAnh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.sanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTG = new SelectList(db.tacGias, "ID", "TenTacGia", sanPham.MaTG);
            ViewBag.MaNXB = new SelectList(db.nXBs, "ID", "TenNXB", sanPham.MaNXB);
            ViewBag.MaLoai = new SelectList(db.theLoais, "ID", "TenTheLoai", sanPham.MaLoai);
            ViewBag.MaKM = new SelectList(db.khuyenMais, "ID", "TenKM", "PTKM", sanPham.MaKM);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenSP,MaTG,MaNXB,NamXB,MaLoai,MaKM,DanhGia,BinhLuan,Mota,ChieuCao,ChieuRong,SoTrang,SoLuongSach,HinhAnh,GiaSP,PTKM")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.sanPhams.Find(id);
            db.sanPhams.Remove(sanPham);
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
