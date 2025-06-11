using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyThuVien;

namespace DAL_QuanLyThuVien
{
    public class DALTrangThaiThanhToan
    {
        private List<TrangThaiThanhToan> SelectBySql(string sql, List<object> args)
        {
            var list = new List<TrangThaiThanhToan>();
            try
            {
                using (var reader = DButil.Query(sql, args))
                {
                    while (reader.Read())
                    {
                        var entity = new TrangThaiThanhToan
                        {
                            MaTrangThai = reader.GetString(reader.GetOrdinal("MaTrangThai")),
                            TenTrangThai = reader.GetString(reader.GetOrdinal("TenTrangThai")),
                            NgayTao = reader.GetDateTime(reader.GetOrdinal("NgayTao"))
                        };
                        list.Add(entity);
                    }
                }
            }
            catch (Exception) { throw; }
            return list;
        }

        public List<TrangThaiThanhToan> SelectAll()
        {
            return SelectBySql("SELECT * FROM TrangThaiThanhToan", new List<object>());
        }

        public TrangThaiThanhToan SelectById(string id)
        {
            var list = SelectBySql("SELECT * FROM TrangThaiThanhToan WHERE MaTrangThai = @0", new List<object> { id });
            return list.Count > 0 ? list[0] : null;
        }
    }
}

