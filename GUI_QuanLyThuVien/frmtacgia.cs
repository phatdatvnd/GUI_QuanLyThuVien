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
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmtacgia : Form
    {
        BUSTACGIA bUSTACGIA = new BUSTACGIA();
        DALTacGia DALTacGia1 = new DALTacGia();
        public frmtacgia()
        {
            InitializeComponent();
        }

        private void btThemNhanVien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) || string.IsNullOrWhiteSpace(txtHoVaTen.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMaNhanVien == null || txtHoVaTen == null || txtMatKhau == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) ||
                !txtMaNhanVien.Text.Contains("TG") ||
                !txtMaNhanVien.Text.Contains("00"))
            {
                MessageBox.Show("Vui lòng nhập mã hợp lệ! Mã phải chứa 'TG' và '00'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string loai = DALTacGia1.generateSanPham();

                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã tác giả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TacGia duog = new TacGia
                {
                    MaTacGia = txtMaNhanVien.Text.Trim(),
                    TenTacGia = txtHoVaTen.Text.Trim(),
                    QuocTich = txtMatKhau.Text.Trim(),
                    TrangThai = rdbthoatdong.Checked,
                    NgayTao = DateOnly.FromDateTime(dtpNgayTao.Value) // Chuyển đổi DateTime sang DateOnly
                };

                DALTacGia1.insertSanPham(duog);
                MessageBox.Show($"Thêm thành công! Mã tác giả: {loai}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                clearform2(); // Xóa dữ liệu trong form sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tác giả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadData()
        {
            List<TacGia> tacGias = DALTacGia1.SelectAll();
            dtgvnhanvien.DataSource = tacGias;
        }

        private void btSuaNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                TacGia entity = new TacGia
                {
                    MaTacGia = txtMaNhanVien.Text.Trim(),
                    TenTacGia = txtHoVaTen.Text.Trim(),
                    QuocTich = txtMatKhau.Text.Trim(),
                    TrangThai = rdbthoatdong.Checked,
                    NgayTao = DateOnly.FromDateTime(dtpNgayTao.Value) // Chuyển đổi DateTime sang DateOnly
                };

                DALTacGia1.updateTacGia(entity);
                LoadData();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }
        private void clearform2()
        {
            txtMaNhanVien.Text = string.Empty;
            txtHoVaTen.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            rdbthoatdong.Checked = true;
            dtpNgayTao.Value = DateTime.Now;
        }

        private void btXoaNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                string maThe = txtMaNhanVien.Text;
                DALTacGia1.deleteSanPham(maThe);
                LoadData();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                List<TacGia> list = DALTacGia1.Search(keyword);
                dtgvnhanvien.DataSource = list;
                if (list.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btLamMoiBang_Click(object sender, EventArgs e)
        {
            dtgvnhanvien.Refresh();
        }

        private void dtgvnhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvnhanvien.Rows[e.RowIndex];
                txtMaNhanVien.Text = row.Cells["MaTacGia"].Value?.ToString();
                txtHoVaTen.Text = row.Cells["TenTacGia"].Value?.ToString();
                txtMatKhau.Text = row.Cells["QuocTich"].Value?.ToString();
                rdbthoatdong.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                dtpNgayTao.Value = row.Cells["NgayTao"].Value is DateTime dateTime ? dateTime : DateTime.Now; // Kiểm tra và gán giá trị ngày tạo
            }
        }

        private void frmtacgia_Load(object sender, EventArgs e)
        {
            LoadData();
        }

       
    }
}
