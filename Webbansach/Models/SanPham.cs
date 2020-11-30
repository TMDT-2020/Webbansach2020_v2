using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class SanPham
    {
        [Key]
        public int ID { get; set; }
        public string TenSP { get; set; }
        public int MaTG { get; set; }
        public virtual TacGia TacGia { get; set; }
        public int MaNXB { get; set; }
        public virtual NXB NXB { get; set; }

        [DataType(DataType.Date)]
        public DateTime NamXB { get; set; }
        public int MaLoai { get; set; }
        public virtual TheLoai TheLoai { get; set; }
        public int MaKM { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
        public string DanhGia { get; set; }
        public string BinhLuan { get; set; }
        public string Mota { get; set; }
        public int ChieuCao { get; set; }
        public int ChieuRong { get; set; }
        public int SoTrang { get; set; }
        public int SoLuongSach { get; set; }
        public string HinhAnh { get; set; }
        public double GiaSP { get; set; }
        public double PTKM { get; set; }
        public double GiaKM
        {
            get
            {
                return (GiaSP - (GiaSP * (PTKM / 100)));
            }
        }
    }
}