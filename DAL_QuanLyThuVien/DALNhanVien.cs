using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_QuanLyThuVien
{
    public class DALNhanVien
    {
        public NhanVien getNhanVien(string email, string password)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            List<object> thamSo = new List<object>() { email, password };
            NhanVien nv = DButil.Value<NhanVien>(sql, thamSo);
            return nv;
        }

        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT TOP 1 * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            List<object> thamSo = new List<object>() { email, password };
            using SqlDataReader reader = DButil.Query(sql, thamSo);
            if (reader.HasRows && reader.Read())
            {
                NhanVien nv = new NhanVien
                {
                    MaNhanVien = reader["MaNhanVien"].ToString(),
                    Ten = reader["Ten"].ToString(),
                    Email = reader["Email"].ToString(),
                    MatKhau = reader["MatKhau"].ToString(),
                    SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString(),
                    VaiTro = Convert.ToBoolean(reader["VaiTro"]),
                    TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                    NgayTao = reader["NgayTao"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["NgayTao"]
                };
                return nv;
            }
            return null;
        }

        public void ResetMatKhau(string mk, string email)
        {
            string sql = "UPDATE NhanVien SET MatKhau = @0 WHERE Email = @1";
            List<object> thamSo = new List<object>() { mk, email };
            DButil.Update(sql, thamSo);
        }

        public void insert(NhanVien entity)
        {
            string sql = "INSERT INTO NhanVien (MaNhanVien, Ten, Email, MatKhau, SoDienThoai, VaiTro, TrangThai, NgayTao) " +
                         "VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            List<object> thamSo = new List<object>()
            {
                entity.MaNhanVien,
                entity.Ten,
                entity.Email,
                entity.MatKhau,
                entity.SoDienThoai,
                entity.VaiTro,
                entity.TrangThai,
                entity.NgayTao
            };
            DButil.Update(sql, thamSo);
        }

        public void update(NhanVien entity)
        {
            string sql = "UPDATE NhanVien SET Ten=@1, MatKhau=@2, Email=@3, SoDienThoai=@4, VaiTro=@5, TrangThai=@6 WHERE MaNhanVien=@0";
            List<object> thamSo = new List<object>()
            {
                entity.MaNhanVien,
                entity.Ten,
                entity.MatKhau,
                entity.Email,
                entity.SoDienThoai,
                entity.VaiTro,
                entity.TrangThai
            };
            DButil.Update(sql, thamSo);
        }

        public List<NhanVien> selectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            using SqlDataReader reader = DButil.Query(sql, args);
            while (reader.Read())
            {
                NhanVien entity = new NhanVien
                {
                    MaNhanVien = reader.GetString(reader.GetOrdinal("MaNhanVien")),
                    Ten = reader.GetString(reader.GetOrdinal("Ten")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                    SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString(),
                    VaiTro = reader.GetBoolean(reader.GetOrdinal("VaiTro")),
                    TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")),
                    NgayTao = reader["NgayTao"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["NgayTao"]
                };
                list.Add(entity);
            }
            return list;
        }

        public List<NhanVien> selectAll()
        {
            string sql = "SELECT * FROM NhanVien";
            return selectBySql(sql, new List<object>());
        }

        public NhanVien selectById(string id)
        {
            string sql = "SELECT * FROM NhanVien WHERE MaNhanVien=@0";
            List<object> thamSo = new List<object>() { id };
            List<NhanVien> list = selectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public string taomanhanvien()
        {
            string prefix = "NV";
            string sql = "SELECT MAX(MaNhanVien) FROM NhanVien";
            object result = DButil.ScalarQuery(sql, new List<object>());
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxcode = result.ToString().Substring(prefix.Length);
                if (int.TryParse(maxcode, out int number))
                {
                    int newnumber = number + 1;
                    return $"{prefix}{newnumber:D3}";
                }
            }
            return $"{prefix}001";
        }

        public bool checkemailexits(string email)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE Email = @0";
            object result = DButil.ScalarQuery(sql, new List<object>() { email });
            return Convert.ToInt32(result) > 0;
        }

        public void deleteNhanVien(string maNv)
        {
            string sql = "DELETE FROM NhanVien WHERE MaNhanVien = @0";
            DButil.Update(sql, new List<object>() { maNv });
        }

        public void updateNhanVien(NhanVien nv)
        {
            string sql = @"UPDATE NhanVien 
                           SET Ten = @1, Email = @2, MatKhau = @3, SoDienThoai = @4, VaiTro = @5, TrangThai = @6
                           WHERE MaNhanVien = @0";
            List<object> thamSo = new List<object>()
            {
                nv.MaNhanVien,
                nv.Ten,
                nv.Email,
                nv.MatKhau,
                nv.SoDienThoai,
                nv.VaiTro,
                nv.TrangThai
            };
            DButil.Update(sql, thamSo);
        }
    }
}
