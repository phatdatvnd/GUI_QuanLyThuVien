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
    public partial class frmmain : Form
    {
        public frmmain()
        {
            InitializeComponent();
        }
        private Form curentFormChild;
        private void openChilForm(Form formChild)
        {
            if (curentFormChild != null)
            {
                curentFormChild.Close();
            }
            curentFormChild = formChild;
            formChild.TopLevel = false;
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.Dock = DockStyle.Fill;
            panelmain.Controls.Add(formChild);
            panelmain.Tag = formChild;
            formChild.BringToFront();
            formChild.Show();
        }

        private void btQuanLySach_Click(object sender, EventArgs e)
        {
            openChilForm(new frmQuanlySach());
        }

        private void btQuanLyNhanVien_Click(object sender, EventArgs e)
        {
           openChilForm(new frmQuanyLyNhanVien());
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
               
            }
        }
    }
}
