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
    public partial class Add_DM : Form
    {
        public Add_DM()
        {
            InitializeComponent();
        }

        crud connect = new crud();


        private void btn_themdm_Click(object sender, EventArgs e)
        {
            //Thực hiện thêm dữ liệu
            if (txt_danhmuc.Text != "")
            {
                if (connect.exedata("insert into DANHMUC (TenDanhMuc) values (N'" + txt_danhmuc.Text + "')") == true)
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
    }
}
