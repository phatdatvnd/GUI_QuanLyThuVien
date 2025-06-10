using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALChiTietMuon
    {
        private List<ChiTietMuon> SelectBySql(string sql, List<object> args)
        {
            List<ChiTietMuon> list = new List<ChiTietMuon>();
            try
            {
                // Giả định DBUtil.Query trả về một SqlDataReader
                using (SqlDataReader reader = DButil.Query(sql, args))
                {
                    while (reader.Read())
                    {
                        ChiTietMuon entity = new ChiTietMuon();
                        entity.MaChiTiet = reader.GetString("MaChiTiet");
                        entity.MaMuonTra = reader.GetString("MaMuonTra");
                        entity.MaSach = reader.GetString("MaSach");
                        entity.SoLuong = reader.GetInt32("SoLuong");
                        entity.NgayTao = reader.GetDateTime("NgayTao");
                        list.Add(entity);
                    }
                }
            }
            catch (Exception)
            {
                // Ném ngoại lệ để lớp BLL hoặc UI xử lý
                throw;
            }
            return list;
        }

        // Lấy tất cả chi tiết mượn sách
        public List<ChiTietMuon> SelectAll()
        {
            string sql = "SELECT * FROM ChiTietMuonSach";
            return SelectBySql(sql, new List<object>());
        }

        // Lấy chi tiết mượn sách theo Mã phiếu mượn
        public List<ChiTietMuon> SelectByMaMuonTra(string maMuonTra)
        {
            string sql = "SELECT * FROM ChiTietMuonSach WHERE MaMuonTra = @0";
            List<object> args = new List<object> { maMuonTra };
            return SelectBySql(sql, args);
        }

        // Tìm kiếm chi tiết
        public List<ChiTietMuon> TimKiem(string keyword)
        {
            string sql = @"SELECT * FROM ChiTietMuonSach 
                           WHERE MaChiTiet LIKE @0 OR MaMuonTra LIKE @1 OR MaSach LIKE @2";
            List<object> args = new List<object> { $"%{keyword}%", $"%{keyword}%", $"%{keyword}%" };
            return SelectBySql(sql, args);
        }

        // Thêm một chi tiết mượn sách mới
        public void Insert(ChiTietMuon ctms)
        {
            try
            {
                string sql = @"INSERT INTO ChiTietMuonSach (MaChiTiet, MaMuonTra, MaSach, SoLuong, NgayTao)
                               VALUES (@0, @1, @2, @3, @4)";
                List<object> args = new List<object>
                {
                    ctms.MaChiTiet,
                    ctms.MaMuonTra,
                    ctms.MaSach,
                    ctms.SoLuong,
                    ctms.NgayTao
                };
                // Giả định DBUtil.Update thực thi các câu lệnh non-query
                DButil.Update(sql, args);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Cập nhật thông tin chi tiết mượn sách
        public void Update(ChiTietMuon ctms)
        {
            try
            {
                string sql = @"UPDATE ChiTietMuonSach 
                               SET MaMuonTra = @1, MaSach = @2, SoLuong = @3
                               WHERE MaChiTiet = @0";
                List<object> args = new List<object>
                {
                    ctms.MaChiTiet,
                    ctms.MaMuonTra,
                    ctms.MaSach,
                    ctms.SoLuong
                };
                DButil.Update(sql, args);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Xóa một chi tiết mượn sách
        public void Delete(string maChiTiet)
        {
            try
            {
                string sql = "DELETE FROM ChiTietMuonSach WHERE MaChiTiet = @0";
                List<object> args = new List<object> { maChiTiet };
                DButil.Update(sql, args);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Tự động tạo mã chi tiết mới
        public string GenerateMaChiTiet()
        {
            string prefix = "CT";
            string sql = "SELECT MAX(MaChiTiet) FROM ChiTietMuonSach";
            // Giả định DBUtil.ScalarQuery trả về một object đơn lẻ
            object result = DButil.ScalarQuery(sql, new List<object>());

            if (result != null && result != DBNull.Value && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(prefix.Length);
                if (int.TryParse(maxCode, out int newNumber))
                {
                    newNumber++;
                    return $"{prefix}{newNumber:D3}"; // Format thành 3 chữ số, ví dụ: CT001, CT012, CT123
                }
            }
            // Nếu không có mã nào hoặc có lỗi, bắt đầu từ 1
            return $"{prefix}001";
        }
    }
}