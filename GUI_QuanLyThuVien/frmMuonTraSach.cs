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
    public partial class frmMuonTraSach : Form
    {
        private BUSMuonTraSach bus = new BUSMuonTraSach();
        public frmMuonTraSach()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            dgvQuanLyMuonTraSach.DataSource = bus.GetAllMuonTra();
            dgvQuanLyMuonTraSach.ColumnHeadersHeight = 30;
            dgvQuanLyMuonTraSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            // Điều chỉnh kích thước cột
            dgvQuanLyMuonTraSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvQuanLyMuonTraSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private MuonTraSach GetFormData()
        {
            return new MuonTraSach
            {
                MaMuonTra = txtMaMuonTra.Text,
                MaKhachHang = txtMaKhachHang.Text,
                MaNhanVien = txtMaNhanVien.Text,
                NgayMuon = dtNgayMuon.Value,
                NgayTra = dtNgayTra.Value,
                NgayTao = dtNgayTao.Value,
                MaTrangThai = txtMaTrangThai.Text
            };
        }

        private void ClearForm()
        {
            txtMaMuonTra.Clear();
            txtMaKhachHang.Clear();
            txtMaNhanVien.Clear();
            txtMaTrangThai.Clear();
            dtNgayMuon.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now;
            dtNgayTao.Value = DateTime.Now;
        }


        private void dgvQuanLyMuonTraSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvQuanLyMuonTraSach.Rows.Count)
            {
                DataGridViewRow row = dgvQuanLyMuonTraSach.Rows[e.RowIndex];
                txtMaMuonTra.Text = row.Cells["MaMuonTra"].Value.ToString();
                txtMaKhachHang.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtMaNhanVien.Text = row.Cells["MaNhanVien"].Value.ToString();
                dtNgayMuon.Value = Convert.ToDateTime(row.Cells["NgayMuon"].Value);
                dtNgayTra.Value = Convert.ToDateTime(row.Cells["NgayTra"].Value);
                dtNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
                txtMaTrangThai.Text = row.Cells["MaTrangThai"].Value?.ToString() ?? "";
            }
        }

        private void btThemMuonTra_Click(object sender, EventArgs e)
        {
            var mt = GetFormData();
            var result = bus.InsertMuonTra(mt);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSuaMuonTra_Click(object sender, EventArgs e)
        {
            var mt = GetFormData();
            if (string.IsNullOrEmpty(mt.MaMuonTra))
            {
                MessageBox.Show("Vui lòng chọn mã mượn trả để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = bus.UpdateMuonTra(mt);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btXoaMuonTra_Click(object sender, EventArgs e)
        {
            string ma = txtMaMuonTra.Text;
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn mã mượn trả để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = bus.DeleteMuonTra(ma);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Xóa thành công!");
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btLamMoiBang_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            try
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadData();
                    return;
                }

                var data = bus.TimKiemMuonTra(keyword);
                Console.WriteLine($"Số lượng bản ghi tìm kiếm: {data?.Count ?? 0}");
                dgvQuanLyMuonTraSach.DataSource = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMuonTraSach_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
