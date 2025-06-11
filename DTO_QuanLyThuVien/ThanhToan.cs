using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyThuVien
{
    public class ThanhToan
    {
        public string MaGiaoDich { get; set; }
        public string TenKhachHang { get; set; }
        public string TenNhanVien { get; set; }
        public string SachDaMuon { get; set; }
        public string TenTrangThai { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public decimal PhiMuon { get; set; }
        public decimal PhiPhat { get; set; }
        public decimal TongThanhToan { get; set; }
    }
}
