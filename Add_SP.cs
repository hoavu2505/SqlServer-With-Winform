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

        private void dataDanhmuc()
        {
            DataTable dt = connect.readdata("select TenDanhMuc from DANHMUC");
            if (dt != null)
            {
                cb_danhmuc.DataSource = dt;
            }
        }

        private void btn_addsp_Click(object sender, EventArgs e)
        {

            if (txt_masp.Text != "" || txt_tensp.Text != "" || cb_danhmuc.Text != "")
            {
                
            }
            else
            {
                MessageBox.Show("Không thể thêm dữ liệu");
            }
        }

        private void Add_SP_Load(object sender, EventArgs e)
        {
            dataDanhmuc();
        }
    }
}
