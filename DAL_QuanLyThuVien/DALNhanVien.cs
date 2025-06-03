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
                    nv.Ten = reader["Ten"].ToString();
                    nv.Email = reader["Email"].ToString();
                    nv.MatKhau = reader["MatKhau"].ToString();
                    nv.SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString();
                    nv.VaiTro = bool.Parse(reader["VaiTro"].ToString());
                    nv.TrangThai = bool.Parse(reader["TrangThai"].ToString());

                    return nv;
                }
            }
            return null;
        }
        public void ResetMatKhau(string mk, string email)
        {
            try
            {
                string sql = "UPDATE NhanVien SET MatKhau = @0 WHERE Email = @1";
                List<object> thamSo = new List<object>();
                thamSo.Add(mk);
                thamSo.Add(email);
                DButil.Update(sql, thamSo);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void insert(NhanVien entity)
        {
            string sql = "INSERT INTO NhanVien (MaNhanVien, Ten, Email, MatKhau, SoDienThoai, VaiTro ,TrangThai) VALUES (@0, @1, @2, @3, @4, @5, @6)";
            List<object> thamSo = new List<object>();
            thamSo.Add(entity.MaNhanVien);
            thamSo.Add(entity.Ten);
            thamSo.Add(entity.Email);
            thamSo.Add(entity.MatKhau);
            thamSo.Add(entity.SoDienThoai);
            thamSo.Add(entity.VaiTro);
            thamSo.Add(entity.TrangThai);
            DButil.Update(sql, thamSo);
        }
        public void update(NhanVien entity)
        {
            string sql = "UPDATE NhanVien SET Ten=@1, MatKhau=@2, Email=@3, SoDienThoai=@4, VaiTro=@5, TrangThai=@6 WHERE MaNhanVien=@0";
            List<object> thamSo = new List<object>();
            thamSo.Add(entity.MaNhanVien);
            thamSo.Add(entity.Ten);
            thamSo.Add(entity.MatKhau);
            thamSo.Add(entity.Email);
            thamSo.Add(entity.SoDienThoai);
            thamSo.Add(entity.VaiTro);
            thamSo.Add(entity.TrangThai);
            DButil.Update(sql, thamSo);
        }
        public List<NhanVien> selectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                SqlDataReader reader = DButil.Query(sql, args);
                while (reader.Read())
                {
                    NhanVien entity = new NhanVien();
                    entity.MaNhanVien = reader.GetString("MaNhanVien");
                    entity.Ten = reader.GetString("Ten");
                    entity.Email = reader.GetString("Email");
                    entity.MatKhau = reader.GetString("MatKhau");
                    entity.SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? null : reader["SoDienThoai"].ToString();
                    entity.VaiTro = reader.GetBoolean("VaiTro");
                    entity.TrangThai = reader.GetBoolean("TrangThai");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<NhanVien> selectAll()
        {
            String sql = "SELECT * FROM NhanVien";
            return selectBySql(sql, new List<object>());
        }
        public NhanVien selectById(string id)
        {
            String sql = "SELECT * FROM NhanVien WHERE MaNhanVien=@1";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<NhanVien> list = selectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }
        public string taomanhanvien()
        {
            string prefix = "NV";
            string sql = "SELECT MAX(MaNhanVien) FROM NhanVien";
            List<object> thamSo = new List<object>();
            object result = DButil.ScalarQuery(sql, thamSo);

            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxcode = result.ToString().Substring(2);
                int newnumber = int.Parse(maxcode) + 1;
                return $"{prefix}{newnumber:D3}";  // Sửa dấu ngoặc sai thành đúng
            }
            return $"{prefix}001";
        }

        public bool checkemailexits(string email)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE Email = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            object result = DButil.ScalarQuery(sql, thamSo);
            return Convert.ToInt32(result) > 0;
        }
        public void deleteNhanVien(string maNv)
        {
            try
            {
                string sql = "DELETE FROM NhanVien WHERE MaNhanVien = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maNv);
                DButil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public void updateNhanVien(NhanVien nv)
        {
            try
            {
                string sql = @"UPDATE NhanVien 
                   SET Ten = @1, Email = @2, MatKhau = @3, SoDienThoai = @4, VaiTro = @5, TrangThai = @6
                   WHERE MaNhanVien = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(nv.MaNhanVien);
                thamSo.Add(nv.Ten);
                thamSo.Add(nv.Email);
                thamSo.Add(nv.MatKhau);
                thamSo.Add(nv.SoDienThoai);
                thamSo.Add(nv.VaiTro);
                thamSo.Add(nv.TrangThai);
                DButil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
