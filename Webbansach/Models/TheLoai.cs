using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class TheLoai
    {
        [Key]
        public int ID { get; set; }
        public string TenTheLoai { get; set; }
    }
}