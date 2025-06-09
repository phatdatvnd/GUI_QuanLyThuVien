using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyThuVien;

namespace DTO_QuanLyThuVien
{
    public class MuonTraSach
    {
        public string MaMuonTra { get; set; }
        public string MaKhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public string MaTrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
    }
}
