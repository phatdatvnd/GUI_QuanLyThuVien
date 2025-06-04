using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLyThuVien
{
    public class BUSQuanLySach
    {
        private DALQuanLySach dalSach = new DALQuanLySach();

        public List<Sach> GetAllSach()
        {
            return dalSach.LayDSSach();
        }

        public string InsertSach(Sach s)
        {
            try
            {
                if (string.IsNullOrEmpty(s.MaSach))
                {
                    return "Mã sách không hợp lệ.";
                }

                if (dalSach.KiemTraTrungMaSach(s.MaSach))
                {
                    return "Mã sách đã tồn tại.";
                }

                dalSach.ThemSach(s);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi thêm sách: " + ex.Message;
            }
        }

        public string UpdateSach(Sach s)
        {
            try
            {
                if (string.IsNullOrEmpty(s.MaSach))
                {
                    return "Mã sách không hợp lệ.";
                }

                dalSach.CapNhatSach(s);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi cập nhật sách: " + ex.Message;
            }
        }

        public string DeleteSach(string maSach)
        {
            try
            {
                if (string.IsNullOrEmpty(maSach))
                {
                    return "Mã sách không hợp lệ.";
                }

                dalSach.XoaSach(maSach);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi xóa sách: " + ex.Message;
            }
        }

        public List<Sach> TimKiemSach(string tuKhoa)
        {
            return dalSach.TimKiemSach(tuKhoa);
        }
    }
}
