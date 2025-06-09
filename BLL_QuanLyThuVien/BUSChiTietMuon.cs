using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSChiTietMuon
    {
        private readonly DALChiTietMuon dalChiTietMuon = new DALChiTietMuon();

        public List<ChiTietMuon> LayTatCaChiTiet()
        {
            return dalChiTietMuon.SelectAll();
        }

        public List<ChiTietMuon> LayChiTietTheoMaMuonTra(string maMuonTra)
        {
            if (string.IsNullOrWhiteSpace(maMuonTra))
            {
                return new List<ChiTietMuon>(); // Trả về danh sách rỗng nếu không có mã
            }
            return dalChiTietMuon.SelectByMaMuonTra(maMuonTra);
        }

        public List<ChiTietMuon> TimKiem(string keyword)
        {
            return dalChiTietMuon.TimKiem(keyword);
        }

        public string ThemChiTiet(ChiTietMuon ctms)
        {
            // Kiểm tra nghiệp vụ
            if (string.IsNullOrWhiteSpace(ctms.MaMuonTra) || string.IsNullOrWhiteSpace(ctms.MaSach))
            {
                return "Mã mượn trả và mã sách không được để trống.";
            }
            if (ctms.SoLuong <= 0)
            {
                return "Số lượng sách phải lớn hơn 0.";
            }

            // Logic kiểm tra xem sách có đủ số lượng tồn không (nếu cần)
            // ...

            try
            {
                // Gán mã chi tiết và ngày tạo trước khi thêm
                ctms.MaChiTiet = dalChiTietMuon.GenerateMaChiTiet();
                ctms.NgayTao = DateTime.Now;
                dalChiTietMuon.Insert(ctms);
                return "Thêm chi tiết thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi thêm chi tiết: {ex.Message}";
            }
        }

        public string SuaChiTiet(ChiTietMuon ctms)
        {
            // Kiểm tra nghiệp vụ
            if (string.IsNullOrWhiteSpace(ctms.MaChiTiet))
            {
                return "Vui lòng chọn một chi tiết để cập nhật.";
            }
            if (ctms.SoLuong <= 0)
            {
                return "Số lượng sách phải lớn hơn 0.";
            }
            // ...

            try
            {
                dalChiTietMuon.Update(ctms);
                return "Cập nhật thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi cập nhật: {ex.Message}";
            }
        }

        public string XoaChiTiet(string maChiTiet)
        {
            if (string.IsNullOrWhiteSpace(maChiTiet))
            {
                return "Mã chi tiết không hợp lệ.";
            }

            try
            {
                dalChiTietMuon.Delete(maChiTiet);
                return "Xóa thành công.";
            }
            catch (Exception ex)
            {
                // Có thể cần kiểm tra lỗi khóa ngoại ở đây
                return $"Lỗi khi xóa: {ex.Message}";
            }
        }
    }
}
