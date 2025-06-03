using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyThuVien
{
    public class NhanVien
    {
        public string MaNhanVien { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }

        public string SoDienThoai { get; set; }
        public bool VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao
        {
            get; set;
        }
    }
}
