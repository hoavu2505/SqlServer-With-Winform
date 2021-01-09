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
            DataTable dt = connect.readdata("select TenDanhMuc from DANHMUC");
            if (dt != null)
            {
                cb_danhmuc.DataSource = dt;
            }
        }

        //Lấy ID Danh Mục
        private string getID (string TenDanhMuc)
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("SELECT IDDanhMuc from DANHMUC where TenDanhMuc = N'"+TenDanhMuc+"'");
            SqlDataAdapter da = new SqlDataAdapter(cmd); //Chuyển dữ liệu về
            DataTable dt = new DataTable(); //Tạo kho ảo lưu trữ dữ liệu
            da.Fill(dt); //Đổ dữ liệu vào kho
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    id = dr["ChucVu"].ToString();
                }
            }
            return id;
        }

        private void Add_SP_Load(object sender, EventArgs e)
        {
            dataDanhmuc();
        }

        private void btn_huybo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string ID_DM = "";
        private void btn_addsp_Click(object sender, EventArgs e)
        {
            ID_DM = getID(cb_danhmuc.Text);
            MessageBox.Show(ID_DM);

            /*
            if (txt_masp.Text != "" || txt_tensp.Text != "")
            {
                if (connect.exedata("insert into MATHANG (IDSanPham, TenSanPham, NgaySX, XuatXu, SoLuongTon, Gia, KhuyenMai, IDDanhMuc) values ()") == true)
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
            */
        }
    }
}
