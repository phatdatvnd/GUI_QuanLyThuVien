using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyThuVien
{
    public class DALQuanLySach
    {
        public List<Sach> LayDSSach()
        {
            List<Sach> ds = new List<Sach>();
            string sql = "SELECT * FROM Sach";
            SqlDataReader reader = DButil.Query(sql, new List<object>(), CommandType.Text);

            while (reader.Read())
            {
                ds.Add(new Sach
                {
                    MaSach = reader["MaSach"].ToString(),
                    TieuDe = reader["TieuDe"].ToString(),
                    MaTheLoai = reader["MaTheLoai"].ToString(),
                    MaTacGia = reader["MaTacGia"].ToString(),
                    NhaXuatBan = reader["NhaXuatBan"].ToString(),
                    SoLuongTon = Convert.ToInt32(reader["SoLuongTon"]),
                    TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                    NgayTao = Convert.ToDateTime(reader["NgayTao"])
                });
            }

            reader.Close();
            return ds;
        }

        public void ThemSach(Sach s)
        {
            string sql = "INSERT INTO Sach VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            List<object> args = new List<object>
            {
                s.MaSach, s.TieuDe, s.MaTheLoai, s.MaTacGia,
                s.NhaXuatBan, s.SoLuongTon, s.TrangThai, s.NgayTao
            };
            DButil.Update(sql, args);
        }

        public void CapNhatSach(Sach s)
        {
            string sql = @"UPDATE Sach SET TieuDe=@1, MaTheLoai=@2, MaTacGia=@3,
                           NhaXuatBan=@4, SoLuongTon=@5, TrangThai=@6 WHERE MaSach=@0";
            List<object> args = new List<object>
            {
                s.MaSach, s.TieuDe, s.MaTheLoai, s.MaTacGia,
                s.NhaXuatBan, s.SoLuongTon, s.TrangThai
            };
            DButil.Update(sql, args);
        }

        public void XoaSach(string maSach)
        {
            string sql = "DELETE FROM Sach WHERE MaSach=@0";
            DButil.Update(sql, new List<object> { maSach });
        }

        public List<Sach> TimKiemTheoTieuDe(string tuKhoa)
        {
            List<Sach> ds = new List<Sach>();
            string sql = "SELECT * FROM Sach WHERE TieuDe LIKE '%' + @0 + '%'";
            SqlDataReader reader = DButil.Query(sql, new List<object> { tuKhoa });

            while (reader.Read())
            {
                ds.Add(new Sach
                {
                    MaSach = reader["MaSach"].ToString(),
                    TieuDe = reader["TieuDe"].ToString(),
                    MaTheLoai = reader["MaTheLoai"].ToString(),
                    MaTacGia = reader["MaTacGia"].ToString(),
                    NhaXuatBan = reader["NhaXuatBan"].ToString(),
                    SoLuongTon = Convert.ToInt32(reader["SoLuongTon"]),
                    TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                    NgayTao = Convert.ToDateTime(reader["NgayTao"])
                });
            }

            reader.Close();
            return ds;
        }
        public bool KiemTraTrungMaSach(string maSach)
        {
            string sql = "SELECT COUNT(*) FROM Sach WHERE MaSach = @0";
            object result = DButil.ScalarQuery(sql, new List<object> { maSach });
            int count = Convert.ToInt32(result);
            return count > 0;
        }
        public List<Sach> TimKiemSach(string tuKhoa)
        {
            List<Sach> ds = new List<Sach>();
            string sql = "SELECT * FROM Sach WHERE TieuDe LIKE '%' + @0 + '%'";
            using (SqlDataReader reader = DButil.Query(sql, new List<object> { tuKhoa }, CommandType.Text))
            {
                while (reader.Read())
                {
                    ds.Add(new Sach
                    {
                        MaSach = reader["MaSach"].ToString(),
                        TieuDe = reader["TieuDe"].ToString(),
                        MaTheLoai = reader["MaTheLoai"].ToString(),
                        MaTacGia = reader["MaTacGia"].ToString(),
                        NhaXuatBan = reader["NhaXuatBan"].ToString(),
                        SoLuongTon = Convert.ToInt32(reader["SoLuongTon"]),
                        TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                        NgayTao = Convert.ToDateTime(reader["NgayTao"])
                    });
                }
            }
            return ds;
        }

    }
}
