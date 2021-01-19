using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaHang
{
    public partial class Edit_NV : Form
    {
        int id = -1;

        crud connect = new crud();
        public Edit_NV(int id)
        {
            InitializeComponent();
            this.id = id;
            DataTable dt = connect.readdata("Select * from USERS Where ID = " + id);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_HoTen.Text = dr["HoTen"].ToString();
                    if (dr["GioiTinh"].ToString().Equals("Nam"))
                    {
                        rb_Nam.Checked = true;
                    }
                    if (dr["GioiTinh"].ToString().Equals("Nữ"))
                    {
                        rb_Nu.Checked = true;
                    }
                    dtp_NgaySinh.Value = DateTime.Parse(dr["NgaySinh"].ToString());
                    txt_DiaChi.Text = dr["DiaChi"].ToString();
                    txt_SDT.Text = dr["SDT"].ToString();
                    dtp_NgayBatDauLam.Value = DateTime.Parse(dr["NgayBatDauLam"].ToString());
                    cb_CaLamViec.Text = dr["CaLamViec"].ToString();
                    cb_ChucVu.Text = dr["ChucVu"].ToString();
                    txt_TenTaiKhoan.Text = dr["TaiKhoan"].ToString();
                    txt_MatKhau.Text = dr["MatKhau"].ToString();
                }
            }
        }

        private void btn_SuaNV_Click(object sender, EventArgs e)
        {
            string gt = "";
            //Xử lý dữ liệu radio của trường giới tính         
            if (rb_Nam.Checked == true)
            {
                gt = rb_Nam.Text;
            }
            if (rb_Nu.Checked == true)
            {
                gt = rb_Nu.Text;
            }
            //Thực hiện thêm dữ liệu
            if (txt_HoTen.Text != "" || txt_TenTaiKhoan.Text != "" || txt_MatKhau.Text != "" || cb_ChucVu.Text != "" || cb_CaLamViec.Text != "")
            {
                if (connect.exedata("Execute sp_EditNhanVien " + this.id + ", N'" + txt_HoTen.Text + "', '" + dtp_NgaySinh.Value.Date.ToString("yyyy-MM-dd") + "', N'" + gt + "', N'" + txt_DiaChi.Text + "', '" + txt_SDT.Text + "', '" + dtp_NgayBatDauLam.Value.Date.ToString("yyyy-MM-dd") + "', " + Convert.ToInt32(cb_CaLamViec.Text.Trim().ToString()) + ", N'" + txt_TenTaiKhoan.Text + "', N'" + txt_MatKhau.Text + "', N'" + cb_ChucVu.Text + "'") == true)
                {
                    DialogResult dlr = MessageBox.Show("Đã sửa dữ liệu thành công");
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

        private void btn_HuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
