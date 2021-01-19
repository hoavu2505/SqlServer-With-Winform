using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;

namespace QLNhaHang
{
    public partial class Add_KH : Form
    {
        public Add_KH()
        {
            InitializeComponent();
        }

        crud connect = new crud();

        string gt;

        private void btn_themkh_Click(object sender, EventArgs e)
        {
            if (rb_nam.Checked == true)
            {
                gt = rb_nam.Text;
            }
            if (rb_nu.Checked == true)
            {
                gt = rb_nu.Text;
            }

            if (txt_tenKhachHang.Text != "" || txt_SDT.Text != "")
            {
                if (connect.exedata(" insert into KHACHHANG (HoTen,SDT,NgaySinh,GioiTinh,DiaChi,NgayMoThe,DiemTichLuy) values (N'"+txt_tenKhachHang.Text+"', '"+txt_SDT.Text+"', '"+dt_NgaySinh.Value.Date.ToString("yyyy-MM-dd") +"', N'"+gt+"', N'"+txt_DiaChi.Text+"', getdate(), '0') " ) == true)
                {
                    DialogResult dlr = MessageBox.Show("Đã thêm dữ liệu thành công");
                    if (dlr == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể thêm dữ liệu");
            }
        }

        private void btn_huybo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            e.Handled = true;
        }
    }
}
