using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class KhuyenMai
    {
        [Key]
        public int ID { get; set; }
        public string TenKM { get; set; }
        public string Mota { get; set; }
        public int PTKM { get; set; }
        public string HinhAnh { get; set; }

        [DataType(DataType.Date)]
        public DateTime ThoiGianBatDau { get; set; }

        [DataType(DataType.Date)]
        public DateTime ThoiGianKetThuc { get; set; }

    }
}