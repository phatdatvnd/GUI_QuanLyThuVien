using BLL_QuanLyThuVien;
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
    public partial class frmQuanlySach : Form
    {
        BUSQuanLySach BUSQuanLySach = new BUSQuanLySach();
        public frmQuanlySach()
        {
            InitializeComponent();
            LoadDataToGrid();
        }
        private void LoadDataToGrid()
        {
            var ds = BUSQuanLySach.GetAllSach();
            dtgvsach.DataSource = null;
            dtgvsach.AutoGenerateColumns = false; // Tắt tự động tạo cột

            // Xóa các cột cũ để tránh trùng lặp
            dtgvsach.Columns.Clear();

            // Tạo các cột thủ công với Name và DataPropertyName
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSach", DataPropertyName = "MaSach", HeaderText = "Mã sách" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "TieuDe", DataPropertyName = "TieuDe", HeaderText = "Tên sách" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaTheLoai", DataPropertyName = "MaTheLoai", HeaderText = "Mã thể loại" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaTacGia", DataPropertyName = "MaTacGia", HeaderText = "Tác giả" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "NhaXuatBan", DataPropertyName = "NhaXuatBan", HeaderText = "Nhà xuất bản" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongTon", DataPropertyName = "SoLuongTon", HeaderText = "Số lượng tồn" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", DataPropertyName = "TrangThai", HeaderText = "Trạng thái" });
            dtgvsach.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayTao", DataPropertyName = "NgayTao", HeaderText = "Ngày tạo" });

            dtgvsach.DataSource = ds;

            // Đảm bảo tiêu đề cột hiển thị
            dtgvsach.ColumnHeadersVisible = true;

            // Đặt font cho toàn bộ DataGridView
            dtgvsach.Font = new Font("Arial", 10);
            dtgvsach.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);
        }
        private Sach GetSachFromForm()
        {
            return new Sach
            {
                MaSach = txtmasach.Text.Trim(),
                TieuDe = txttieude.Text.Trim(),
                MaTheLoai = txttheloai.Text.Trim(),
                MaTacGia = txtmatacgia.Text.Trim(),
                NhaXuatBan = txtnhaxuatban.Text.Trim(),
                SoLuongTon = int.TryParse(txtsoluongton.Text.Trim(), out int sl) ? sl : 0,
                TrangThai = rdbtcon.Checked,
                NgayTao = dtNgayTao.Value
            };
        }
        private void ClearForm()
        {
            txtmasach.Clear();
            txttieude.Clear();
            txttheloai.Clear();
            txtmatacgia.Clear();
            txtnhaxuatban.Clear();
            txtsoluongton.Clear();
            rdbtcon.Checked = false;
            dtNgayTao.Value = DateTime.Now;
        }
        private void btThemsach_Click(object sender, EventArgs e)
        {
            var sach = GetSachFromForm();
            string err = BUSQuanLySach.InsertSach(sach);
            if (string.IsNullOrEmpty(err))
            {
                MessageBox.Show("Thêm sách thành công!", "Thông báo");
                LoadDataToGrid();
                ClearForm();
            }
            else
            {
                MessageBox.Show(err, "Lỗi");
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            var sach = GetSachFromForm();
            string err = BUSQuanLySach.UpdateSach(sach);
            if (string.IsNullOrEmpty(err))
            {
                MessageBox.Show("Cập nhật sách thành công!", "Thông báo");
                LoadDataToGrid();
                ClearForm();
            }
            else
            {
                MessageBox.Show(err, "Lỗi");
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string maSach = txtmasach.Text.Trim();
            if (string.IsNullOrEmpty(maSach))
            {
                MessageBox.Show("Vui lòng nhập mã sách để xóa.", "Cảnh báo");
                return;
            }
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa sách mã '{maSach}'?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                string err = BUSQuanLySach.DeleteSach(maSach);
                if (string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("Xóa sách thành công!", "Thông báo");
                    LoadDataToGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(err, "Lỗi");
                }
            }
        }

        private void btLamMoiBang_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDataToGrid();
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                LoadDataToGrid();
                return;
            }

            var ds = BUSQuanLySach.TimKiemSach(tuKhoa);
            dtgvsach.DataSource = null;
            dtgvsach.DataSource = ds;
        }

        private void dtgvsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // tránh click vào header hoặc ngoài vùng dữ liệu
            {
                DataGridViewRow row = dtgvsach.Rows[e.RowIndex];

                txtmasach.Text = row.Cells["MaSach"].Value?.ToString();
                txttieude.Text = row.Cells["TieuDe"].Value?.ToString();
                txttheloai.Text = row.Cells["MaTheLoai"].Value?.ToString();
                txtmatacgia.Text = row.Cells["MaTacGia"].Value?.ToString();
                txtnhaxuatban.Text = row.Cells["NhaXuatBan"].Value?.ToString();
                txtsoluongton.Text = row.Cells["SoLuongTon"].Value?.ToString();

                if (bool.TryParse(row.Cells["TrangThai"].Value?.ToString(), out bool trangThai))
                    rdbtcon.Checked = trangThai;
                else
                    rdbthet.Checked = false;

                if (DateTime.TryParse(row.Cells["NgayTao"].Value?.ToString(), out DateTime ngayTao))
                    dtNgayTao.Value = ngayTao;
                else
                    dtNgayTao.Value = DateTime.Now;
            }
        }
    }
}
