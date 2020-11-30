using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        public int SanPhamID { get; set; }
        public virtual SanPham SanPham { get; set; }

        public int Gia { get; set; }
        public int SoLuong { get; set; }
    }
}