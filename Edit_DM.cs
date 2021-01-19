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
    public partial class Edit_DM : Form
    {
        int iddm = -1;

        crud connect = new crud();

        public Edit_DM(int id)
        {
            InitializeComponent();
            this.iddm = id;
            DataTable dt = connect.readdata("Select * from DANHMUC where IDDanhMuc = " + iddm);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_TenDanhMuc.Text = dr["TenDanhMuc"].ToString().Trim();
                }
            }
        }

        private void btn_SuaDanhMuc_Click(object sender, EventArgs e)
        {
            if (txt_TenDanhMuc.Text != "")
            {
                if (connect.exedata("Execute sp_updatedanhmuc " + this.iddm + ", N'" + txt_TenDanhMuc.Text + "'") == true)
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
