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

        crud connect = new crud();

        //LOAD DATA
        private void dataNhanVien()
        {
            DataTable dt = connect.readdata("select ID,HoTen,TaiKhoan,MatKhau,ChucVu,CaLamViec from USERS");
            if (dt != null)
            {
                dtGridNhanVien.DataSource = dt;
            }
        }

        private void dataDanhMuc()
        {
            DataTable dt = connect.readdata("select IDDanhMuc, TenDanhMuc from DANHMUC");
            if (dt != null)
            {
                dtGridDanhMuc.DataSource = dt;
            }
        }


        private void dataSanPham()
        {
            DataTable dt = connect.readdata("select IDSanPham, TenSanPham, Gia from MATHANG");
            if (dt != null)
            {
                dtGridSanPham.DataSource = dt;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Xin chào User có Chức Vụ: " + Login.ID_USER);
            dataNhanVien();
            dataDanhMuc();
            dataSanPham();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Add_NV addnv = new Add_NV();
            addnv.ShowDialog();
            //LOAD lại dữ liệu sau khi thêm
            DataTable dt = connect.readdata("select ID,HoTen,TaiKhoan,MatKhau,ChucVu,CaLamViec from USERS");
            if (dt != null)
            {
                dtGridNhanVien.DataSource = dt;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Edit_NV editnv = new Edit_NV();
            editnv.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Add_DM adddm = new Add_DM();
            adddm.ShowDialog();
            DataTable dt = connect.readdata("select IDDanhMuc, TenDanhMuc from DANHMUC");
            if (dt != null)
            {
                dtGridDanhMuc.DataSource = dt;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Edit_DM editdm = new Edit_DM();
            editdm.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Add_SP addsp = new Add_SP();
            addsp.ShowDialog();
            DataTable dt = connect.readdata("select IDSanPham, TenSanPham, Gia from MATHANG");
            if (dt != null)
            {
                dtGridSanPham.DataSource = dt;
            }
        }
    }
}
