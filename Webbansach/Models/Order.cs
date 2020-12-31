    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Webbansach.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Webbansach.Models
{

    public class Order
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Key]
        public int ID { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
        public string StatusPayment { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }

        public string UserID { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public bool Updatestt(Order order)
        {
            var oder = db.orders.Find(order.ID);
            oder.StatusPayment = "Đã thanh toán";
            db.SaveChanges();
            return true;
        }
        public bool Updatestt2(Order order)
        {
            var oder = db.orders.Find(order.ID);
            oder.Status = "Đang giao";
            db.SaveChanges();
            return true;
        }
        public bool Updatestt3(Order order)
        {
            var oder = db.orders.Find(order.ID);
            oder.Status = "Đã hủy";
            db.SaveChanges();
            return true;
        }
        public bool Updatestt4(Order order)
        {
            var oder = db.orders.Find(order.ID);
            oder.Status = "Đã giao";
            db.SaveChanges();
            return true;
        }
        public Order ViewDT(int id)
        {
            return db.orders.Find(id);
        }

    }
}