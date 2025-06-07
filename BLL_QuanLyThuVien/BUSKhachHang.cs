using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLyThuVien
{
    public class BUSKhachHang
    {
        private readonly DALKhachHang dalKhachHang = new DALKhachHang();

        public List<KhachHang> GetKhachHangs()
        {
            return dalKhachHang.SelectAll();
        }

        public string InsertKhachHang(KhachHang kh)
        {
            if (kh == null)
                return "Khách hàng không hợp lệ.";

            try
            {
                kh.MaKhachHang = dalKhachHang.TaoMaKhachHang();

                if (string.IsNullOrEmpty(kh.MaKhachHang))
                    return "Mã khách hàng không hợp lệ.";

                if (dalKhachHang.CheckEmailTonTai(kh.Email))
                    return "Email đã tồn tại.";

                kh.NgayTao = DateTime.Now;
                dalKhachHang.Insert(kh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi thêm khách hàng: " + ex.Message;
            }
        }

        public string XoaKhachHang(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH))
                return "Mã khách hàng không hợp lệ.";

            try
            {
                dalKhachHang.Delete(maKH);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateKhachHang(KhachHang kh)
        {
            if (kh == null || string.IsNullOrEmpty(kh.MaKhachHang))
                return "Mã khách hàng không hợp lệ.";

            try
            {
                dalKhachHang.Update(kh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public KhachHang? GetKhachHangById(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH))
                return null;

            return dalKhachHang.SelectById(maKH);
        }
    }
}
