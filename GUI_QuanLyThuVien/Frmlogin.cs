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
            string username = txtnguoidung.Text;
            string password = txtmatkhau.Text;
            NhanVien nv = busnv.DangNhap(username, password);
            if (nv == null)
            {
                MessageBox.Show(this, "Tài khoản hoặc mật khẩu không chính xác");
            }
            else
            {
                if (nv.TrangThai == false)
                {
                    MessageBox.Show(this, "Tài khoản đang tạm khóa, vui lòng viên hệ QTV!!!");
                    return;
                }
                AuthUtil.user = nv;

                frmQuanyLyNhanVien mainchinh = new frmQuanyLyNhanVien();
                mainchinh.Show();
                this.Hide();
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
