using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSThanhToan
    {
        // Khởi tạo các lớp DAL cần thiết
        private readonly DALQuanLyMuonTraSach _dalMuonTra = new DALQuanLyMuonTraSach();
        private readonly DALChiTietMuon _dalChiTiet = new DALChiTietMuon();
        private readonly DALKhachHang _dalKhachHang = new DALKhachHang();
        private readonly DALNhanVien _dalNhanVien = new DALNhanVien();
        private readonly DALTrangThaiThanhToan _dalTrangThai = new DALTrangThaiThanhToan();
        private readonly DALQuanLySach _dalSach = new DALQuanLySach();
        private readonly DALPhiSach _dalPhiSach = new DALPhiSach();

        /// <summary>
        /// Lấy và tổng hợp dữ liệu từ nhiều bảng để hiển thị.
        /// </summary>
        public List<ThanhToan> LayDanhSachHienThi()
        {
            var danhSachHienThi = new List<ThanhToan>();
            List<MuonTraSach> danhSachMuonTra = _dalMuonTra.selectAll();

            // Tải trước toàn bộ danh sách sách để tối ưu hiệu năng,
            // tránh việc gọi vào database liên tục trong vòng lặp.
            List<Sach> tatCaSach = _dalSach.LayDSSach();

            foreach (var mt in danhSachMuonTra)
            {
                var khachHang = _dalKhachHang.SelectById(mt.MaKhachHang);
                var nhanVien = _dalNhanVien.selectById(mt.MaNhanVien);
                var trangThai = _dalTrangThai.SelectById(mt.MaTrangThai);
                var chiTietList = _dalChiTiet.SelectByMaMuonTra(mt.MaMuonTra);

                decimal tongPhiMuon = 0;
                decimal tongPhiPhat = 0;
                var tenSachList = new List<string>();

                foreach (var ct in chiTietList)
                {
                    // ĐÃ SỬA LỖI: Tìm sách trong danh sách đã tải trước đó bằng LINQ
                    // thay vì gọi một phương thức SelectById không tồn tại.
                    var sach = tatCaSach.FirstOrDefault(s => s.MaSach == ct.MaSach);

                    if (sach != null)
                    {
                        tenSachList.Add(sach.TieuDe);
                        var phi = _dalPhiSach.SelectByMaSach(ct.MaSach);
                        if (phi != null)
                        {
                            tongPhiMuon += phi.PhiMuon * ct.SoLuong;
                            // Quy tắc nghiệp vụ: Phí phạt được áp dụng nếu trả sách quá hạn và chưa thanh toán
                            if (DateTime.Now.Date > mt.NgayTra.Date && mt.MaTrangThai != "TT002")
                            {
                                tongPhiPhat += phi.PhiPhat * ct.SoLuong;
                            }
                        }
                    }
                }

                danhSachHienThi.Add(new ThanhToan
                {
                    MaGiaoDich = mt.MaMuonTra,
                    TenKhachHang = khachHang?.TenKhachHang ?? "N/A",
                    TenNhanVien = nhanVien?.Ten ?? "N/A",
                    SachDaMuon = string.Join(", ", tenSachList),
                    TenTrangThai = trangThai?.TenTrangThai ?? "N/A",
                    NgayMuon = mt.NgayMuon,
                    NgayTra = mt.NgayTra,
                    PhiMuon = tongPhiMuon,
                    PhiPhat = tongPhiPhat,
                    TongThanhToan = tongPhiMuon + tongPhiPhat
                });
            }
            return danhSachHienThi;
        }

        public string ThemGiaoDich(MuonTraSach muonTra, List<ChiTietMuon> chiTietList)
        {
            if (muonTra == null || chiTietList == null || !chiTietList.Any())
                return "Thông tin giao dịch không hợp lệ.";
            try
            {
                muonTra.MaMuonTra = _dalMuonTra.generateMaMuonTra();
                muonTra.NgayTao = DateTime.Now;

                _dalMuonTra.insert(muonTra);

                foreach (var chiTiet in chiTietList)
                {
                    chiTiet.MaMuonTra = muonTra.MaMuonTra;
                    chiTiet.MaChiTiet = _dalChiTiet.GenerateMaChiTiet();
                    chiTiet.NgayTao = DateTime.Now;
                    _dalChiTiet.Insert(chiTiet);

                }
                return "Thêm giao dịch thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi thêm giao dịch: {ex.Message}";
            }
        }

        public string SuaGiaoDich(MuonTraSach muonTra)
        {
            if (muonTra == null || string.IsNullOrWhiteSpace(muonTra.MaMuonTra))
                return "Thông tin giao dịch không hợp lệ.";

            try
            {
                _dalMuonTra.update(muonTra);
                return "Cập nhật giao dịch thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi cập nhật: {ex.Message}";
            }
        }

        /// <summary>
        /// Xóa một giao dịch mượn sách (chức năng XÓA).
        /// </summary>
        public string XoaGiaoDich(string maGiaoDich)
        {
            if (string.IsNullOrWhiteSpace(maGiaoDich))
                return "Mã giao dịch không hợp lệ.";

            try
            {
                // Lấy danh sách chi tiết cần xóa để cập nhật lại số lượng sách
                var chiTietList = _dalChiTiet.SelectByMaMuonTra(maGiaoDich);

                // Xóa tất cả các chi tiết mượn sách liên quan trước
                foreach (var chiTiet in chiTietList)
                {
                    _dalChiTiet.Delete(chiTiet.MaChiTiet);
                }

                // Xóa phiếu mượn sách
                _dalMuonTra.delete(maGiaoDich);

                return "Xóa giao dịch thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi xóa giao dịch: {ex.Message}";
            }
        }

        public string CapNhatTrangThai(string maMuonTra, string maTrangThaiMoi)
        {
            try
            {
                var muonTra = _dalMuonTra.selectById(maMuonTra);
                if (muonTra == null) return "Lỗi: Mã giao dịch không tồn tại.";

                muonTra.MaTrangThai = maTrangThaiMoi;
                _dalMuonTra.update(muonTra);
                return "Cập nhật trạng thái thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi cập nhật: {ex.Message}";
            }
        }

        public List<ThanhToan> TimKiemGiaoDich(string tuKhoa)
        {
            var danhSachDayDu = LayDanhSachHienThi();
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return danhSachDayDu;
            }

            string tuKhoaLower = tuKhoa.ToLower();
            return danhSachDayDu.Where(v => v.MaGiaoDich.ToLower().Contains(tuKhoaLower) ||
                                            v.TenKhachHang.ToLower().Contains(tuKhoaLower) ||
                                            v.TenNhanVien.ToLower().Contains(tuKhoaLower))
                                .ToList();
        }

        /// <summary>
        /// Lấy tất cả trạng thái thanh toán để đổ vào ComboBox.
        /// </summary>
        public List<TrangThaiThanhToan> LayTatCaTrangThai()
        {
            return _dalTrangThai.SelectAll();
        }
    }
}