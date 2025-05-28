using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyThuVien
{
    public class DALNhanVien
    {
        public NhanVien getNhanVien(string email, string password)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            thamSo.Add(password);
            NhanVien nv = DButil.Value<NhanVien>(sql, thamSo);
            return nv;
        }

        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT Top 1 * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            thamSo.Add(password);
            SqlDataReader reader = DButil.Query(sql, thamSo);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    NhanVien nv = new NhanVien();
                    nv.MaNhanVien = reader["MaNhanVien"].ToString();
                    nv.HoTen = reader["Ten"].ToString();
                    nv.Email = reader["Email"].ToString();
                    nv.MatKhau = reader["MatKhau"].ToString();
                    nv.VaiTro = bool.Parse(reader["VaiTro"].ToString());
                    nv.TrangThai = bool.Parse(reader["TrangThai"].ToString());

                    return nv;
                }
            }
            return null;
        }
    }
}
