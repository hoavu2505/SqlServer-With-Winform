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

        int vitriNV = -1;
        int vitriDM = -1;

        //LOAD DATA Nhân Viên
        private void dataNhanVien()
        {
            DataTable dt = connect.readdata("select * from ViewDSNhanVien");
            if (dt != null)
            {
                dtGridNhanVien.DataSource = dt;
            }
        }

        //LOAD DATA Tìm Nhân Viên
        private void dataTKNhanVien(string name)
        {
            DataTable dt = connect.readdata("select * from ft_TimKiemNV(N'" + name + "')");
            if (dt != null)
            {
                dtGridNhanVien.DataSource = dt;
            }
        }

        //LOAD DATA Danh Mục
        private void dataDanhMuc()
        {
            DataTable dt = connect.readdata("select * from ViewDanhMuc");
            if (dt != null)
            {
                dtGridDanhMuc.DataSource = dt;
            }
        }

        //LOAD DATA Tìm Danh Mục
        private void dataTKDanhMuc(string name)
        {
            DataTable dt = connect.readdata("select * from fu_bangdanhmuc(N'" + name + "')");
            if (dt != null)
            {
                dtGridDanhMuc.DataSource = dt;
            }
        }

        //LOAD DATA Sản Phẩm
        private void dataSanPham()
        {
            DataTable dt = connect.readdata("select * from ViewDSSanPham");
            if (dt != null)
            {
                dtGridSanPham.DataSource = dt;
            }
        }

        //LOAD DATA Tìm Sản Phẩm
        private void dataTKSanPham(string name)
        {
            DataTable dt = connect.readdata("select * from fu_bangsanpham(N'" + name + "')");
            if (dt != null)
            {
                dtGridSanPham.DataSource = dt;
            }
        }

        //LOAD DATA Khách Hàng
        private void dataKhachHang()
        {
            DataTable dt = connect.readdata("select * from ViewDSkhachhang");
            if (dt != null)
            {
                dtGridKhachHang.DataSource = dt;
            }
        }

        //LOAD DATA Tìm Khách Hàng
        private void dataTKKhachHang(string name)
        {
            DataTable dt = connect.readdata("select * from ViewDSkhachhang where HoTen = "+ name +"");
            if (dt != null)
            {
                dtGridKhachHang.DataSource = dt;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Xin chào User có Chức Vụ: " + Login.ID_USER);
            
            dataNhanVien();
            dataDanhMuc();
            dataSanPham();
            dataKhachHang();
        }

        //Thêm Nhân Viên
        private void button6_Click(object sender, EventArgs e)
        {
            Add_NV addnv = new Add_NV();
            addnv.ShowDialog();
            //LOAD lại dữ liệu sau khi thêm
            DataTable dt = connect.readdata("select * from ViewDSNhanVien");
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

        //Thêm Danh Mục
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

        //Thêm Sản Phẩm
        private void button15_Click(object sender, EventArgs e)
        {
            Add_SP addsp = new Add_SP();
            addsp.ShowDialog();
            DataTable dt = connect.readdata("select * from ViewDSSanPham");
            if (dt != null)
            {
                dtGridSanPham.DataSource = dt;
            }
        }

        private void btn_TKNhanVien_Click(object sender, EventArgs e)
        {
            dataTKNhanVien(txt_TKNhanVien.Text.ToString());
        }

        private void btn_TKDanhMuc_Click(object sender, EventArgs e)
        {
            dataTKDanhMuc(txt_TKDanhMuc.Text.ToString());
        }

        private void btn_TKSanPham_Click(object sender, EventArgs e)
        {
            dataTKSanPham(txt_TKSanPham.Text.ToString());
        }

        private void btn_TKKhachHang_Click(object sender, EventArgs e)
        {
            dataTKKhachHang(txt_TKKhachHang.Text.ToString());
        }

        //Lấy chỉ số hàng
        private void dtGridNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitriNV = dtGridNhanVien.CurrentCell.RowIndex;
        }

        private void dtGridDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitriDM = dtGridDanhMuc.CurrentCell.RowIndex;
        }

        
    }
}
