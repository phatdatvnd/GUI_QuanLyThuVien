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
            BUSNhanVien BUSNhanVien = new BUSNhanVien();
            InitializeComponent();
        }

    }
}
