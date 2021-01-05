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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source='DESKTOP-TR8V8B8\MSSQLSERVER2505';Initial Catalog='QLCuaHang';Integrated Security='True'");

        private void TableNhanVien()
        {
            conn.Open();
            string sql = "select ID,HoTen,TaiKhoan,MatKhau,ChucVu,CaLamViec from USERS";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dtGridNhanVien.DataSource = dt;
        }

        private void TableDanhMuc()
        {
            conn.Open();
            string sql = "select IDDanhMuc, TenDanhMuc from DANHMUC";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dtGridDanhMuc.DataSource = dt;
        }

        private void TableSanPham()
        {
            conn.Open();
            string sql = "select IDSanPham, TenSanPham, Gia from MATHANG";
            SqlCommand com = new SqlCommand(sql, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dtGridSanPham.DataSource = dt;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Xin chào User có Chức Vụ: " + Login.ID_USER);
            TableNhanVien();
            TableDanhMuc();
            TableSanPham();
        }
    }
}
