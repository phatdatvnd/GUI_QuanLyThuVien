using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLyThuVien
{
    public class BUSNhanVien
    {
        DALNhanVien dalNhanVien = new DALNhanVien();

        public NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            return dalNhanVien.getNhanVien1(username, password);
        }

    }
}
