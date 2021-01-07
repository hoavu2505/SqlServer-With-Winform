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
    public partial class Add_NV : Form
    {
        public Add_NV()
        {
            InitializeComponent();
        }

        crud connect = new crud();

        string gt;
        private void btn_them_Click(object sender, EventArgs e)
        {
            //Xử lý dữ liệu radio của trường giới tính
            
            if (rb_nam.Checked == true)
            {
                gt = rb_nam.Text;
            }
            if (rb_nu.Checked == true)
            {
                gt = rb_nu.Text;
            }
            //Thực hiện thêm dữ liệu
            if (connect.exedata("insert into USERS (HoTen,NgaySinh,GioiTinh,DiaChi,SDT,NgayBatDauLam,CaLamViec,TaiKhoan,MatKhau,ChucVu) values (N'" + txt_hoten.Text + "', N'" + dtp_ngaysinh.Value + "', N'" + gt + "', N'" + txt_diachi.Text + "', N'" + txt_sdt.Text + "', N'" + dtp_ngaylamviec.Value + "', N'" + cb_calamviec.Text + "', N'" + txt_taikhoan.Text + "', N'" + txt_matkhau.Text + "', N'" + txt_chucvu.Text + "')") == true)
            {
                DialogResult dlr = MessageBox.Show("Đã thêm dữ liệu thành công");
                if(dlr == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Không thể thêm dữ liệu");
            }

        }

        //Không cho nhập ký tự vào textbox sđt
        private void txt_sdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            e.Handled = true;
        }
    }
}
