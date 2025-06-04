using BLL_QuanLyThuVien;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QuanLyThuVien
{
    public partial class frmQuanyLyNhanVien : Form
    {
        BUSNhanVien bus = new BUSNhanVien();

        public frmQuanyLyNhanVien()
        {
            InitializeComponent();
            LoadDSNhanVien();
        }
        private void LoadDSNhanVien()
        {
            var list = bus.GetNhanViens();
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nhân viên để hiển thị!");
                dtgvnhanvien.DataSource = null;
                return;
            }

            dtgvnhanvien.AutoGenerateColumns = false;
            dtgvnhanvien.Columns.Clear();

            dtgvnhanvien.Columns.Add("MaNhanVien", "Mã nhân viên");
            dtgvnhanvien.Columns.Add("Ten", "Họ và tên");
            dtgvnhanvien.Columns.Add("Email", "Email");
            dtgvnhanvien.Columns.Add("MatKhau", "Mật khẩu");
            dtgvnhanvien.Columns.Add("SoDienThoai", "SĐT");
            dtgvnhanvien.Columns.Add("VaiTro", "Vai trò");
            dtgvnhanvien.Columns.Add("TrangThai", "Trạng thái");
            dtgvnhanvien.Columns.Add("NgayTao", "Ngày tạo");

            dtgvnhanvien.Columns["MaNhanVien"].DataPropertyName = "MaNhanVien";
            dtgvnhanvien.Columns["Ten"].DataPropertyName = "Ten";
            dtgvnhanvien.Columns["Email"].DataPropertyName = "Email";
            dtgvnhanvien.Columns["MatKhau"].DataPropertyName = "MatKhau";
            dtgvnhanvien.Columns["SoDienThoai"].DataPropertyName = "SoDienThoai";
            dtgvnhanvien.Columns["VaiTro"].DataPropertyName = "VaiTro";
            dtgvnhanvien.Columns["TrangThai"].DataPropertyName = "TrangThai";
            dtgvnhanvien.Columns["NgayTao"].DataPropertyName = "NgayTao";

            dtgvnhanvien.DataSource = list;

            // Tăng chiều cao của header
            dtgvnhanvien.ColumnHeadersHeight = 30; // Điều chỉnh giá trị này (mặc định thường là 23)
            dtgvnhanvien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            // Tùy chỉnh hiển thị cột VaiTro và TrangThai
            dtgvnhanvien.CellFormatting += dtgvnhanvien_CellFormatting_1;

            // Điều chỉnh kích thước cột
            dtgvnhanvien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }



        private void btThemNhanVien_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien
            {
                Ten = txtHoVaTen.Text,
                Email = txtEmail.Text,
                MatKhau = txtMatKhau.Text,
                SoDienThoai = txtSoDienThoai.Text,
                VaiTro = rdbtquanly.Checked,
                TrangThai = rdbthoatdong.Checked
            };

            string result = bus.Insertnhanvien(nv);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadDSNhanVien();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btSuaNhanVien_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien
            {
                MaNhanVien = txtMaNhanVien.Text,
                Ten = txtHoVaTen.Text,
                Email = txtEmail.Text,
                MatKhau = txtMatKhau.Text,
                SoDienThoai = txtSoDienThoai.Text,
                VaiTro = rdbtquanly.Checked,
                TrangThai = rdbthoatdong.Checked
            };

            string result = bus.UpdateNhanVien(nv);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật nhân viên thành công!");
                LoadDSNhanVien();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btXoaNhanVien_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text;
            DialogResult confirm = MessageBox.Show("Ngài có chắc muốn xoá không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                string result = bus.xoaNhanvien(maNV);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadDSNhanVien();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
        }

        private void btLamMoiBang_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDSNhanVien();
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower();
            var list = bus.GetNhanViens().FindAll(nv =>
                nv.Ten.ToLower().Contains(keyword) ||
                nv.Email.ToLower().Contains(keyword));

            dtgvnhanvien.DataSource = null;
            dtgvnhanvien.DataSource = list;
        }
        private void ClearForm()
        {
            txtMaNhanVien.Clear();
            txtHoVaTen.Clear();
            txtEmail.Clear();
            txtMatKhau.Clear();
            txtSoDienThoai.Clear();
            rdbtquanly.Checked = false;
            rdbthoatdong.Checked = true;
        }

        private void dtgvnhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtgvnhanvien.Rows[e.RowIndex].DataBoundItem is NhanVien nv)
            {
                txtMaNhanVien.Text = nv.MaNhanVien;
                txtHoVaTen.Text = nv.Ten;
                txtEmail.Text = nv.Email;
                txtMatKhau.Text = nv.MatKhau;
                txtSoDienThoai.Text = nv.SoDienThoai;
                rdbtquanly.Checked = nv.VaiTro;
                rdbthoatdong.Checked = nv.TrangThai;
            }
        }

        private void frmQuanyLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadDSNhanVien();
            dtgvnhanvien.AutoGenerateColumns = true;
            dtgvnhanvien.DataSource = bus.GetNhanViens();

        }

        private void dtgvnhanvien_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvnhanvien.Columns[e.ColumnIndex].Name == "VaiTro" && e.Value != null)
            {
                string vaiTroValue = e.Value.ToString().ToLower();
                e.Value = vaiTroValue == "1" || vaiTroValue == "true" ? "Quản Lý" : "Nhân Viên";
                e.FormattingApplied = true;
            }
            else if (dtgvnhanvien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThaiValue = e.Value.ToString().ToLower();
                e.Value = trangThaiValue == "1" || trangThaiValue == "true" ? "Hoạt Động" : "Ngưng Hoạt Động";
                e.FormattingApplied = true;
            }
        }

        
    }
}
