using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyThuVien;

namespace DAL_QuanLyThuVien
{
    public class DALPhiSach
    {
        private List<PhiSach> SelectBySql(string sql, List<object> args)
        {
            var list = new List<PhiSach>();
            try
            {
                using (var reader = DButil.Query(sql, args))
                {
                    while (reader.Read())
                    {
                        var entity = new PhiSach
                        {
                            MaPhiSach = reader.GetString(reader.GetOrdinal("MaPhiSach")),
                            MaSach = reader.GetString(reader.GetOrdinal("MaSach")),
                            PhiMuon = reader.GetDecimal(reader.GetOrdinal("PhiMuon")),
                            PhiPhat = reader.GetDecimal(reader.GetOrdinal("PhiPhat")),
                            TrangThai = reader.GetBoolean(reader.GetOrdinal("TrangThai")),
                            NgayTao = reader.GetDateTime(reader.GetOrdinal("NgayTao"))
                        };
                        list.Add(entity);
                    }
                }
            }
            catch (Exception) { throw; }
            return list;
        }

        public PhiSach SelectByMaSach(string maSach)
        {
            var list = SelectBySql("SELECT * FROM PhiSach WHERE MaSach = @0 AND TrangThai = 1", new List<object> { maSach });
            return list.Count > 0 ? list[0] : null;
        }
    }
}
