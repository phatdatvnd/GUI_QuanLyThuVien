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
    public partial class frmChiTietMuonTraSach : Form
    {
        private readonly BUSChiTietMuon busChiTietMuon = new BUSChiTietMuon();
        public frmChiTietMuonTraSach()
        {
            InitializeComponent();
        }

        // Tải hoặc tải lại dữ liệu lên DataGridView
        private void LoadData()
        {
            dgvChiTietMuonTra.DataSource = busChiTietMuon.LayTatCaChiTiet();
            dgvChiTietMuonTra.ClearSelection();
            ClearFormInputs();

        }

        // Cấu hình ban đầu cho các controls
        private void ConfigureForm()
        {
            // Mã chi tiết và ngày tạo không cho phép người dùng sửa trực tiếp
            txtMaChiTiet.Enabled = false;
            dtNgayTao.Enabled = false;
            // Đặt tên cột cho dễ nhìn
            dgvChiTietMuonTra.Columns["MaChiTiet"].HeaderText = "Mã Chi Tiết";
            dgvChiTietMuonTra.Columns["MaMuonTra"].HeaderText = "Mã Phiếu Mượn";
            dgvChiTietMuonTra.Columns["MaSach"].HeaderText = "Mã Sách";
            dgvChiTietMuonTra.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTietMuonTra.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            // Tăng chiều cao của header
            dgvChiTietMuonTra.ColumnHeadersHeight = 30;
            dgvChiTietMuonTra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            // Điều chỉnh kích thước cột
            dgvChiTietMuonTra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvChiTietMuonTra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Xóa trắng các ô nhập liệu
        private void ClearFormInputs()
        {
            txtMaChiTiet.Text = "";
            txtMaMuonTra.Text = "";
            txtMaSach.Text = "";
            txtSoLuong.Text = "";
            dtNgayTao.Value = DateTime.Now;
        }


        private void btThemNhanVien_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng DTO từ dữ liệu người dùng nhập
            ChiTietMuon ctms = new ChiTietMuon
            {
                MaMuonTra = txtMaMuonTra.Text.Trim(),
                MaSach = txtMaSach.Text.Trim(),
                SoLuong = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0
            };

            // Gọi BLL để xử lý việc thêm mới
            string result = busChiTietMuon.ThemChiTiet(ctms);
            MessageBox.Show(result, "Thông báo");

            // Nếu thành công, tải lại dữ liệu
            if (result.Contains("thành công"))
            {
                LoadData();
            }
        }

        private void btSuaMuonTra_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (string.IsNullOrWhiteSpace(txtMaChiTiet.Text))
            {
                MessageBox.Show("Vui lòng chọn một chi tiết để sửa.", "Cảnh báo");
                return;
            }

            ChiTietMuon ctms = new ChiTietMuon
            {
                MaChiTiet = txtMaChiTiet.Text,
                MaMuonTra = txtMaMuonTra.Text.Trim(),
                MaSach = txtMaSach.Text.Trim(),
                SoLuong = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0
            };

            // Gọi BLL để xử lý việc cập nhật
            string result = busChiTietMuon.SuaChiTiet(ctms);
            MessageBox.Show(result, "Thông báo");

            if (result.Contains("thành công"))
            {
                LoadData();
            }
        }

        private void btXoaMuonTra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaChiTiet.Text))
            {
                MessageBox.Show("Vui lòng chọn một chi tiết để xóa.", "Cảnh báo");
                return;
            }

            // Xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string maChiTiet = txtMaChiTiet.Text;
                string result = busChiTietMuon.XoaChiTiet(maChiTiet);
                MessageBox.Show(result, "Thông báo");

                if (result.Contains("thành công"))
                {
                    LoadData();
                }
            }
        }


        private void btLamMoiBang_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadData();
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo");
                return;
            }

            List<ChiTietMuon> ketQua = busChiTietMuon.TimKiem(keyword);
            dgvChiTietMuonTra.DataSource = ketQua;

            if (ketQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả nào.", "Thông báo");
            }
        }

        private void frmChiTietMuonTraSach_Load(object sender, EventArgs e)
        {
            LoadData();
            ConfigureForm();
        }

        private void dgvChiTietMuonTra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo người dùng click vào một dòng hợp lệ
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChiTietMuonTra.Rows[e.RowIndex];
                txtMaChiTiet.Text = row.Cells["MaChiTiet"].Value.ToString();
                txtMaMuonTra.Text = row.Cells["MaMuonTra"].Value.ToString();
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                dtNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            }
        }
    }
}
