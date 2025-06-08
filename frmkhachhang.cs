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
    public partial class frmkhachhang : Form
    {
        BUSKhachHang bus = new BUSKhachHang();

        public frmkhachhang()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            var khachHangs = bus.GetKhachHangs();
            dtgvkhachhang.DataSource = khachHangs?.Select(kh => new
            {
                MaKhachHang = kh.MaKhachHang,
                TenKhachHang = kh.TenKhachHang,
                Email = kh.Email,
                CCCD = kh.CCCD,
                SoDienThoai = kh.SoDienThoai,
                TrangThai = kh.TrangThai ? "Hoạt động" : "Ngừng hoạt động",
                NgayTao = kh.NgayTao
            }).ToList();

            // Định nghĩa các cột thủ công
            dtgvkhachhang.AutoGenerateColumns = false; // Tắt tự động tạo cột
            dtgvkhachhang.Columns.Clear();

            // Thêm cột cho DataGridView
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKhachHang",
                HeaderText = "Mã Khách Hàng",
                DataPropertyName = "MaKhachHang"
            });
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKhachHang",
                HeaderText = "Tên Khách Hàng",
                DataPropertyName = "TenKhachHang"
            });
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email"
            });
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CCCD",
                HeaderText = "CCCD",
                DataPropertyName = "CCCD"
            });
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoDienThoai",
                HeaderText = "Số Điện Thoại",
                DataPropertyName = "SoDienThoai"
            });
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng Thái",
                DataPropertyName = "TrangThai"
            });
            dtgvkhachhang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayTao",
                HeaderText = "Ngày Tạo",
                DataPropertyName = "NgayTao"
            });

            // Tăng chiều cao của header
            dtgvkhachhang.ColumnHeadersHeight = 30;
            dtgvkhachhang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            // Điều chỉnh kích thước cột
            dtgvkhachhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void ClearForm()
        {
            txtmakhachhang.Text = "";
            txttenkhachhang.Text = "";
            txtEmail.Text = "";
            txtcccd.Text = "";
            txtSoDienThoai.Text = "";
            rdbthoatdong.Checked = true;
            dtpNgayTao.Value = DateTime.Now;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang
            {
                TenKhachHang = txttenkhachhang.Text,
                Email = txtEmail.Text,
                CCCD = txtcccd.Text,
                SoDienThoai = txtSoDienThoai.Text,
                TrangThai = rdbthoatdong.Checked,
                NgayTao = dtpNgayTao.Value
            };

            string result = bus.InsertKhachHang(kh);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang
            {
                MaKhachHang = txtmakhachhang.Text,
                TenKhachHang = txttenkhachhang.Text,
                Email = txtEmail.Text,
                CCCD = txtcccd.Text,
                SoDienThoai = txtSoDienThoai.Text,
                TrangThai = rdbthoatdong.Checked,
                NgayTao = dtpNgayTao.Value
            };

            string result = bus.UpdateKhachHang(kh);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtmakhachhang.Text;

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string result = bus.XoaKhachHang(maKH);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
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
            LoadData();

        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            var list = bus.GetKhachHangs();
            var result = list.Where(kh => kh.TenKhachHang.ToLower().Contains(keyword)).ToList();
            dtgvkhachhang.DataSource = result;
        }

        private void frmkhachhang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtgvkhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvkhachhang.Rows[e.RowIndex];
                txtmakhachhang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txttenkhachhang.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtcccd.Text = row.Cells["CCCD"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
                string trangThaiText = row.Cells["TrangThai"].Value.ToString();
                rdbthoatdong.Checked = trangThaiText == "Hoạt động";
                rdbtngunghoatdong.Checked = trangThaiText == "Ngừng hoạt động";
            }
        }
    }
}
