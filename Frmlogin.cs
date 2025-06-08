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
using Util_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class Frmlogin : Form
    {
        BUSNhanVien busnv = new BUSNhanVien();
        public Frmlogin()
        { 
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            string username = txtnguoidung.Text.Trim();
            string password = txtmatkhau.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(this, "Vui lòng nhập đầy đủ email và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NhanVien nv = busnv.DangNhap(username, password);

            if (nv == null)
            {
                MessageBox.Show(this, "Tài khoản hoặc mật khẩu không chính xác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!nv.TrangThai)
            {
                MessageBox.Show(this, "Tài khoản đang bị khóa, vui lòng liên hệ quản trị viên!", "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                AuthUtil.user = nv;
                frmmain frmchinh = new frmmain();
                frmchinh.Show();
            this.Hide();

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
