using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmThanhToan : Form
    {
        private readonly BUSThanhToan _busThanhToan = new BUSThanhToan();
        private readonly BUSKhachHang _busKhachHang = new BUSKhachHang();
        private readonly BUSNhanVien _busNhanVien = new BUSNhanVien();
        public frmThanhToan()
        {
            InitializeComponent();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            LamMoiBang();
            LoadComboBoxes();
            ConfigureForm();
        }

        private void LamMoiBang()
        {
            // Tải danh sách hiển thị lên DataGridView
            dgvQuanLyThanhToan.DataSource = _busThanhToan.LayDanhSachHienThi();
            ClearFormInputs();
        }

        private void LoadComboBoxes()
        {
            // Tải dữ liệu cho các ComboBox
            // Đã sửa lại tên phương thức cho nhất quán với lớp BUSKhachHang của bạn
            cbKhachHang.DataSource = _busKhachHang.GetKhachHangs();
            cbKhachHang.DisplayMember = "TenKhachHang";
            cbKhachHang.ValueMember = "MaKhachHang";

            // Giả định lớp BUSNhanVien có phương thức GetNhanViens() tương tự
            cbNhanVien.DataSource = _busNhanVien.GetNhanViens();
            cbNhanVien.DisplayMember = "Ten";
            cbNhanVien.ValueMember = "MaNhanVien";

            cbTrangThai.DataSource = _busThanhToan.LayTatCaTrangThai();
            cbTrangThai.DisplayMember = "TenTrangThai";
            cbTrangThai.ValueMember = "MaTrangThai";
        }

        private void ConfigureForm()
        {
            // Cấu hình các cột cho DataGridView
            dgvQuanLyThanhToan.Columns["MaGiaoDich"].HeaderText = "Mã Giao Dịch";
            dgvQuanLyThanhToan.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
            dgvQuanLyThanhToan.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
            dgvQuanLyThanhToan.Columns["SachDaMuon"].HeaderText = "Sách Mượn";
            dgvQuanLyThanhToan.Columns["TenTrangThai"].HeaderText = "Trạng Thái";
            dgvQuanLyThanhToan.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
            dgvQuanLyThanhToan.Columns["NgayTra"].HeaderText = "Ngày Trả";
            dgvQuanLyThanhToan.Columns["PhiMuon"].HeaderText = "Phí Mượn";
            dgvQuanLyThanhToan.Columns["PhiPhat"].HeaderText = "Phí Phạt";
            dgvQuanLyThanhToan.Columns["TongThanhToan"].HeaderText = "Tổng Cộng";

            // Chỉ cho phép đọc ở một số ô
            txtMaGiaoDich.ReadOnly = true;
            txtPhiMuon.ReadOnly = true;
            txtPhiPhat.ReadOnly = true;
            txtTongThanhToan.ReadOnly = true;
        }

        private void ClearFormInputs()
        {
            txtMaGiaoDich.Clear();
            txtPhiMuon.Clear();
            txtPhiPhat.Clear();
            txtTongThanhToan.Clear();
            txtTimKiem.Clear();

            cbKhachHang.SelectedIndex = -1;
            cbNhanVien.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;

            dtNgayMuon.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(7); // Mặc định hẹn trả sau 7 ngày

            cbSachDaMuon.DataSource = null;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            // Chức năng THÊM yêu cầu một form riêng để chọn sách và các thông tin chi tiết.
            // Mã ở đây chỉ mang tính minh họa.
            MessageBox.Show("Chức năng 'Thêm' cần một giao diện chi tiết hơn để chọn sách. Vui lòng tạo một form mới cho chức năng này.", "Thông báo");

            /* // Logic ví dụ nếu có form thêm:
            var muonTra = new MuonTraSach {
                MaKhachHang = cboKhachHang.SelectedValue.ToString(),
                MaNhanVien = cboNhanVien.SelectedValue.ToString(),
                MaTrangThai = cboTrangThai.SelectedValue.ToString(),
                NgayMuon = dtpNgayMuon.Value,
                NgayTra = dtpNgayTra.Value
            };
            // Cần có List<ChiTietMuonSach> từ form chọn sách
            List<ChiTietMuonSach> chiTietList = ...; 
            string result = _busThanhToan.ThemGiaoDich(muonTra, chiTietList);
            MessageBox.Show(result);
            if (result.Contains("thành công")) LamMoiBang();
            */
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaGiaoDich.Text))
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để sửa.", "Cảnh báo");
                return;
            }

            // Tạo đối tượng MuonTraSach từ thông tin trên form
            var muonTra = new MuonTraSach
            {
                MaMuonTra = txtMaGiaoDich.Text,
                MaKhachHang = cbKhachHang.SelectedValue.ToString(),
                MaNhanVien = cbNhanVien.SelectedValue.ToString(),
                MaTrangThai = cbTrangThai.SelectedValue.ToString(),
                NgayMuon = dtNgayMuon.Value,
                NgayTra = dtNgayTra.Value
            };

            string result = _busThanhToan.SuaGiaoDich(muonTra);
            MessageBox.Show(result, "Thông báo");

            if (result.Contains("thành công"))
            {
                LamMoiBang();
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaGiaoDich.Text))
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để xóa.", "Cảnh báo");
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này? Hành động này không thể hoàn tác.", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string maGiaoDich = txtMaGiaoDich.Text;
                string result = _busThanhToan.XoaGiaoDich(maGiaoDich);
                MessageBox.Show(result, "Thông báo");

                if (result.Contains("thành công"))
                {
                    LamMoiBang();
                }
            }
        }

        private void btLamMoiBang_Click(object sender, EventArgs e)
        {
            LamMoiBang();
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            dgvQuanLyThanhToan.DataSource = _busThanhToan.TimKiemGiaoDich(tuKhoa);
        }

        private void dgvQuanLyThanhToan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQuanLyThanhToan.Rows[e.RowIndex];

                // Lấy dữ liệu từ đối tượng ThanhToanView
                var thanhToanView = row.DataBoundItem as ThanhToan;
                if (thanhToanView == null) return;

                // Hiển thị lên các controls
                txtMaGiaoDich.Text = thanhToanView.MaGiaoDich;
                dtNgayMuon.Value = thanhToanView.NgayMuon;
                dtNgayTra.Value = thanhToanView.NgayTra;
                txtPhiMuon.Text = thanhToanView.PhiMuon.ToString("N0");
                txtPhiPhat.Text = thanhToanView.PhiPhat.ToString("N0");
                txtTongThanhToan.Text = thanhToanView.TongThanhToan.ToString("N0");

                // Chọn giá trị trong ComboBox
                cbKhachHang.Text = thanhToanView.TenKhachHang;
                cbNhanVien.Text = thanhToanView.TenNhanVien;
                cbTrangThai.Text = thanhToanView.TenTrangThai;

                // Hiển thị danh sách sách mượn
                var sachList = thanhToanView.SachDaMuon.Split(new[] { ", " }, StringSplitOptions.None);
                cbSachDaMuon.DataSource = sachList;
            }
        }
    }
}
