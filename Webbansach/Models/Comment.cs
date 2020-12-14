using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string NoiDung { get; set; }
        public int SanPhamID { get; set; }
        public virtual SanPham SanPham { get; set; }
        public string UserID { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}