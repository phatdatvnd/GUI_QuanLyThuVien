using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;


namespace DAL_QuanLyThuVien
{
    public class DALQuanLyMuonTraSach
    {
        public List<MuonTraSach> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<MuonTraSach> list = new List<MuonTraSach>();
            try
            {
                SqlDataReader reader = DButil.Query(sql, args);
                while (reader.Read())
                {
                    MuonTraSach entity = new MuonTraSach();
                    entity.MaMuonTra = reader.GetString("MaMuonTra");
                    entity.MaKhachHang = reader.GetString("MaKhachHang");
                    entity.MaNhanVien = reader.GetString("MaNhanVien");
                    entity.NgayMuon = reader.GetDateTime("NgayMuon");
                    entity.NgayTra = reader.GetDateTime("NgayTra");
                    entity.NgayTao = reader.GetDateTime("NgayTao");
                    entity.MaTrangThai = reader.GetString("MaTrangThai");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<MuonTraSach> selectAll()
        {
            string sql = "SELECT * FROM MuonTraSach";
            return SelectBySql(sql, new List<object>());
        }

        public List<MuonTraSach> TimKiem(string keyword)
        {
            string sql = @"SELECT * FROM MuonTraSach 
                   WHERE MaMuonTra LIKE @0 OR MaKhachHang LIKE @1 OR MaNhanVien LIKE @2";
            List<object> args = new List<object>
    {
        $"%{keyword}%", $"%{keyword}%", $"%{keyword}%"
    };
            return SelectBySql(sql, args);
        }


        public MuonTraSach selectById(string id)
        {
            string sql = "SELECT * FROM MuonTraSach WHERE MaMuonTra = @0";
            List<object> args = new List<object> { id };
            List<MuonTraSach> list = SelectBySql(sql, args);
            return list.Count > 0 ? list[0] : null;
        }

        public void insert(MuonTraSach mt)
        {
            try
            {
                string sql = @"INSERT INTO MuonTraSach (MaMuonTra, MaKhachHang, MaNhanVien, NgayMuon, NgayTra, NgayTao, MaTrangThai)
                               VALUES (@0, @1, @2, @3, @4, @5, @6)";
                List<object> args = new List<object>
                {
                    mt.MaMuonTra, mt.MaKhachHang, mt.MaNhanVien,
                    mt.NgayMuon, mt.NgayTra, mt.NgayTao, mt.MaTrangThai
                };
                DButil.Update(sql, args);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(MuonTraSach mt)
        {
            try
            {
                string sql = @"UPDATE MuonTraSach 
                               SET MaKhachHang = @1, MaNhanVien = @2, NgayMuon = @3, NgayTra = @4, NgayTao = @5, MaTrangThai = @6
                               WHERE MaMuonTra = @0";
                List<object> args = new List<object>
                {
                    mt.MaMuonTra, mt.MaKhachHang, mt.MaNhanVien,
                    mt.NgayMuon, mt.NgayTra, mt.NgayTao, mt.MaTrangThai
                };
                DButil.Update(sql, args);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void delete(string maMuonTra)
        {
            try
            {
                string sql = "DELETE FROM MuonTraSach WHERE MaMuonTra = @0";
                List<object> args = new List<object> { maMuonTra };
                DButil.Update(sql, args);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string generateMaMuonTra()
        {
            string prefix = "MT";
            string sql = "SELECT MAX(MaMuonTra) FROM MuonTraSach";
            object result = DButil.ScalarQuery(sql, new List<object>());
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(2);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }
    }
}
