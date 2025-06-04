using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyThuVien
{
    public class Sach
    {
        public string MaSach { get; set; }
        public string TieuDe { get; set; }
        public string MaTheLoai { get; set; }
        public string MaTacGia { get; set; }
        public string NhaXuatBan { get; set; }
        public int SoLuongTon { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao
        {
            get; set;
        }
    }
}
