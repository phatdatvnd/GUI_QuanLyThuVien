using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSMuonTraSach
    {
        private DALQuanLyMuonTraSach dalMuonTra = new DALQuanLyMuonTraSach();

        public List<MuonTraSach> GetAllMuonTra()
        {
            return dalMuonTra.selectAll();
        }

        public bool CheckKhachHangExists(string maKhachHang)
        {
            return new DALKhachHang().CheckExists(maKhachHang);
        }

        public bool CheckNhanVienExists(string maNhanVien)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM dbo.NhanVien WHERE MaNhanVien = @0";
                List<object> args = new List<object> { maNhanVien };
                object result = DButil.ScalarQuery(sql, args);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckTrangThaiExists(string maTrangThai)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM dbo.TrangThaiThanhToan WHERE MaTrangThai = @0";
                List<object> args = new List<object> { maTrangThai };
                object result = DButil.ScalarQuery(sql, args);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MuonTraSach> TimKiemMuonTra(string keyword)
        {
            return dalMuonTra.TimKiem(keyword);
        }

        public string InsertMuonTra(MuonTraSach mt)
        {
            try
            {
                mt.MaMuonTra = dalMuonTra.generateMaMuonTra();
                if (string.IsNullOrEmpty(mt.MaMuonTra))
                {
                    return "Mã mượn trả không hợp lệ.";
                }

                if (!CheckKhachHangExists(mt.MaKhachHang))
                {
                    return "Mã khách hàng không tồn tại trong cơ sở dữ liệu.";
                }

                if (!CheckNhanVienExists(mt.MaNhanVien))
                {
                    return "Mã nhân viên không tồn tại trong cơ sở dữ liệu.";
                }

                if (!CheckTrangThaiExists(mt.MaTrangThai))
                {
                    return "Mã trạng thái không tồn tại trong cơ sở dữ liệu.";
                }

                dalMuonTra.insert(mt);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi thêm mượn trả: " + ex.Message;
            }
        }

        public string UpdateMuonTra(MuonTraSach mt)
        {
            try
            {
                if (string.IsNullOrEmpty(mt.MaMuonTra))
                {
                    return "Mã mượn trả không hợp lệ.";
                }

                if (!CheckKhachHangExists(mt.MaKhachHang))
                {
                    return "Mã khách hàng không tồn tại trong cơ sở dữ liệu.";
                }

                if (!CheckNhanVienExists(mt.MaNhanVien))
                {
                    return "Mã nhân viên không tồn tại trong cơ sở dữ liệu.";
                }

                if (!CheckTrangThaiExists(mt.MaTrangThai))
                {
                    return "Mã trạng thái không tồn tại trong cơ sở dữ liệu.";
                }

                dalMuonTra.update(mt);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi cập nhật mượn trả: " + ex.Message;
            }
        }

        public string DeleteMuonTra(string maMuonTra)
        {
            try
            {
                if (string.IsNullOrEmpty(maMuonTra))
                {
                    return "Mã mượn trả không hợp lệ.";
                }

                dalMuonTra.delete(maMuonTra);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi xóa mượn trả: " + ex.Message;
            }
        }

        public MuonTraSach GetById(string maMuonTra)
        {
            return dalMuonTra.selectById(maMuonTra);
        }
    }
}