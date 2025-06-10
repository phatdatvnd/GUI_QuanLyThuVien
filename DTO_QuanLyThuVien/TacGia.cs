using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DTO_QuanLyThuVien
{
    public class TacGia
    {
        public string MaTacGia { get; set; } = string.Empty;
        public string TenTacGia { get; set; } = string.Empty;
        public string QuocTich { get; set; } = string.Empty;
        public bool TrangThai { get; set; }
        public DateOnly NgayTao { get; set; } = DateOnly.MinValue;
    }
}
