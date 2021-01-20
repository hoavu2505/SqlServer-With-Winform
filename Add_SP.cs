using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data;

namespace QLNhaHang
{
    public partial class Add_SP : Form
    {
        public Add_SP()
        {
            InitializeComponent();
        }

        crud connect = new crud();
         //Hàm đổ dữ liệu vào ComboBox
        private void dataDanhmuc()
        {
            DataTable dt = connect.readdata("select * from DANHMUC");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["TenDanhMuc"] = dr["TenDanhMuc"].ToString().Trim();
                }
                cb_danhmuc.DataSource = dt;
            }
        }

        private void Add_SP_Load(object sender, EventArgs e)
        {
            dataDanhmuc();
        }

        private void btn_huybo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int iddm = -1;

        private void btn_addsp_Click(object sender, EventArgs e)
        {
            if (txt_masp.Text != "" || txt_tensp.Text != "")
            {
                if (connect.exedata("insert into MATHANG (IDSanPham, TenSanPham, NgaySX, XuatXu, SoLuongTon, Gia, KhuyenMai, IDDanhMuc) values ('" + txt_masp.Text.ToString() + "', N'" + txt_tensp.Text.ToString() + "', '" + dtp_ngaysx.Value.Date.ToString("yyyy-MM-dd") + "', N'" + txt_xuatxu.Text.ToString() + "', " + Convert.ToInt32(nb_soluong.Value.ToString()) + ", " + Convert.ToInt32(txt_dongia.Text.ToString()) + ", " + Convert.ToInt32(txt_khuyenmai.Text.ToString()) + ", " + iddm + ")") == true)
                {
                    DialogResult dlr = MessageBox.Show("Đã thêm dữ liệu thành công");
                    if (dlr == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không thể thêm dữ liệu");
                }
            }
            else
            {
                MessageBox.Show("Không thể thêm dữ liệu");
            }
        }

        private void cb_danhmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            iddm = Convert.ToInt32(cb_danhmuc.SelectedValue.ToString());
        }
    }
}
