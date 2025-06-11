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
using Util_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmdoimk : Form
    {
        BUSNhanVien BUSNhanVien = new BUSNhanVien();
        public frmdoimk()
        {
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            if (!AuthUtil.user.MatKhau.Equals(txtmatkhaucu.Text))
            {
                MessageBox.Show("Mật khẩu hiện tại không đúng");
            }
            // Kiểm tra mật khẩu mới và xác nhận có trùng nhau không
            else if (!txtmatkhaumoi.Text.Equals(txtxacnhanmk.Text))
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không trùng khớp");
            }
            else
            {
                // Thực hiện đổi mật khẩu
                if (BUSNhanVien.ResetMatKhau(AuthUtil.user.Email, txtmatkhaumoi.Text))
                {
                    MessageBox.Show("Đổi mật khẩu thành công");
                    AuthUtil.user.MatKhau = txtmatkhaumoi.Text; // cập nhật lại mật khẩu trong bộ nhớ
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu không thành công");
                }
            }
        }

        private void bamhienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            txtmatkhaucu.UseSystemPasswordChar = !bamhienmatkhau.Checked;
        }

     
    }
}
