using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyThuVien
{
    public class DALKhachHang
    {
        public void Insert(KhachHang kh)
        {
            string sql = @"INSERT INTO KhachHang (MaKhachHang, TenKhachHang, Email, SoDienThoai, CCCD, TrangThai, NgayTao)
                           VALUES (@0, @1, @2, @3, @4, @5, @6)";
            List<object> parameters = new List<object>
            {
                kh.MaKhachHang,
                kh.TenKhachHang,
                kh.Email,
                kh.SoDienThoai,
                kh.CCCD,
                kh.TrangThai,
                kh.NgayTao
            };
            DButil.Update(sql, parameters);
        }

        public void Update(KhachHang kh)
        {
            string sql = @"UPDATE KhachHang 
                           SET TenKhachHang = @1, Email = @2, SoDienThoai = @3, CCCD = @4, TrangThai = @5
                           WHERE MaKhachHang = @0";
            List<object> parameters = new List<object>
            {
                kh.MaKhachHang,
                kh.TenKhachHang,
                kh.Email,
                kh.SoDienThoai,
                kh.CCCD,
                kh.TrangThai
            };
            DButil.Update(sql, parameters);
        }

        public void Delete(string maKH)
        {
            string sql = "DELETE FROM KhachHang WHERE MaKhachHang = @0";
            DButil.Update(sql, new List<object> { maKH });
        }

        public KhachHang? SelectById(string maKH)
        {
            string sql = "SELECT * FROM KhachHang WHERE MaKhachHang = @0";
            List<object> args = new List<object> { maKH };
            List<KhachHang> list = SelectBySql(sql, args);
            return list.Count > 0 ? list[0] : null;
        }

        public List<KhachHang> SelectAll()
        {
            string sql = "SELECT * FROM KhachHang";
            return SelectBySql(sql, new List<object>());
        }

        public List<KhachHang> SelectBySql(string sql, List<object> args)
        {
            List<KhachHang> list = new List<KhachHang>();
            using SqlDataReader reader = DButil.Query(sql, args);
            while (reader.Read())
            {
                KhachHang kh = new KhachHang
                {
                    MaKhachHang = reader["MaKhachHang"].ToString(),
                    TenKhachHang = reader["TenKhachHang"].ToString(),
                    Email = reader["Email"].ToString(),
                    SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString(),
                    CCCD = reader["CCCD"].ToString(),
                    TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                    NgayTao = reader["NgayTao"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["NgayTao"]
                };
                list.Add(kh);
            }
            return list;
        }

        public string TaoMaKhachHang()
        {
            string prefix = "KH";
            string sql = "SELECT MAX(MaKhachHang) FROM KhachHang";
            object result = DButil.ScalarQuery(sql, new List<object>());
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(prefix.Length);
                if (int.TryParse(maxCode, out int number))
                {
                    int newNumber = number + 1;
                    return $"{prefix}{newNumber:D3}";
                }
            }
            return $"{prefix}001";
        }

        public bool CheckEmailTonTai(string email)
        {
            string sql = "SELECT COUNT(*) FROM KhachHang WHERE Email = @0";
            object result = DButil.ScalarQuery(sql, new List<object> { email });
            return Convert.ToInt32(result) > 0;
        }
        public bool CheckExists(string maKhachHang)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM dbo.KhachHang WHERE MaKhachHang = @0";
                List<object> args = new List<object> { maKhachHang };
                Console.WriteLine($"Executing SQL: {sql} with MaKhachHang = {maKhachHang}");
                object result = DButil.ScalarQuery(sql, args);
                Console.WriteLine($"Result: {result}");
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi kiểm tra khách hàng: {ex.Message}");
                throw;
            }
        }
    }
}
