using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace BLL_QuanLyThuVien
{
    public class BUSTACGIA
    {
        DALTacGia dalTacGia = new DALTacGia();
        public List<TacGia> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<TacGia> list = new List<TacGia>();
            try
            {
                SqlDataReader reader = DButil.Query(sql, args);
                while (reader.Read())
                {
                    TacGia duong = new TacGia();
                    duong.MaTacGia = reader["MaTacGia"].ToString();
                    duong.TenTacGia = reader["TenTacGia"].ToString();
                    duong.QuocTich = reader["QuocTich"].ToString();
                    duong.TrangThai = Convert.ToBoolean(reader["TrangThai"]);
                    duong.NgayTao = reader["NgayTao"] == DBNull.Value ? DateOnly.MinValue : DateOnly.FromDateTime((DateTime)reader["NgayTao"]);
                    list.Add(duong);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<TacGia> SelectAll()
        {
            string sql = "SELECT * FROM TacGia";
            return SelectBySql(sql, new List<object>());
        }
        public List<TacGia> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return SelectAll();

            string sql = "SELECT * FROM TacGia WHERE MaTacGia LIKE @0 OR TenTacGia LIKE @0";
            List<object> thamSo = new List<object> { $"%{keyword}%" };
            return SelectBySql(sql, thamSo);
        }
        public string generateSanPham()
        {
            string prefix = "TG";
            string sql = "SELECT MAX(MâTcGia) FROM TacGia";
            List<object> thamSo = new List<object>();
            object result = DButil.ScalarQuery(sql, thamSo);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(3);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }

        public void insertSanPham(TacGia the)
        {
            try
            {
                string sql = @"INSERT INTO TacGia (MaTacGia, TenTacGia, QuocTich,TrangThai,NgayTao) 
                   VALUES (@0, @1, @2,@3,@4)";
                List<object> thamSo = new List<object>();
                thamSo.Add(the.MaTacGia);
                thamSo.Add(the.TenTacGia);
                thamSo.Add(the.QuocTich);
                thamSo.Add(the.TrangThai);
                thamSo.Add(the.NgayTao.ToDateTime(new TimeOnly(0, 0, 0)));
                DButil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updateTacGia(TacGia the)
        {
            try
            {
                string sql = @"UPDATE TacGia 
                   SET TenTacGia = @1, QuocTich = @2,TrangThai = @3, NgayTao = @4
                   WHERE MaTacGia = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(the.MaTacGia);
                thamSo.Add(the.TenTacGia);
                thamSo.Add(the.QuocTich);
                thamSo.Add(the.TrangThai);
                thamSo.Add(the.NgayTao.ToDateTime(new TimeOnly(0, 0, 0)));
                DButil.Update(sql, thamSo);
            }
            catch (Exception e) { throw; }
        }

        public void deleteSanPham(string maThe)
        {
            try
            {
                string sql = "DELETE FROM TacGia WHERE MaTacGia = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maThe);
                DButil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
