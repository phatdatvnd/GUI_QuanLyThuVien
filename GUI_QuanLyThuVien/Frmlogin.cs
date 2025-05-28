using BLL_QuanLyThuVien;
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
    public partial class Frmlogin : Form
    {
        public Frmlogin()
        {
            BUSNhanVien busnv = new BUSNhanVien();
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            string username = txtnguoidung.Text.Trim();
            string password = txtmatkhau.Text;

            BUSNhanVien busnv = new BUSNhanVien();
            // Giả sử bạn có phương thức kiểm tra đăng nhập trong BUSNhanVien
            bool isValid = busnv.DangNhap(username, password) != null;

            if (isValid)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Mở form chính hoặc thực hiện hành động tiếp theo
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtnguoidung.Clear();
                txtmatkhau.Focus();
            }

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bamhienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            txtmatkhau.UseSystemPasswordChar = !bamhienmatkhau.Checked;
        }
    }
}
