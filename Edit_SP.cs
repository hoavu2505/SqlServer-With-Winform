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
    public partial class Edit_SP : Form
    {
        string id = "";
        crud connect = new crud();

        private void dataDanhmuc()
        {
            DataTable dt = connect.readdata("select * from DANHMUC");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["TenDanhMuc"] = dr["TenDanhMuc"].ToString().Trim();
                }
                cb_DanhMuc.DataSource = dt;
            }
        }

        public Edit_SP(string id)
        {
            InitializeComponent();
            this.id = id;
            DataTable dt = connect.readdata("select * from MATHANG where IDSanPham = N'"+id+"'");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_MaSP.Text = dr["IDSanPham"].ToString();
                    txt_TenSP.Text = dr["TenSanPham"].ToString();
                    dtp_NgaySX.Value = DateTime.Parse(dr["NgaySX"].ToString());
                    txt_XuatXu.Text = dr["XuatXu"].ToString();
                    nb_SoLuong.Value = Int32.Parse(dr["SoLuongTon"].ToString());
                    txt_DonGia.Text = dr["Gia"].ToString();
                    txt_KhuyenMai.Text = dr["KhuyenMai"].ToString();

                    int iddm1 = Convert.ToInt32(dr["IDDanhMuc"]);
                    DataTable dt2 = connect.readdata("select * from DANHMUC where IDDanhMuc = N'"+iddm1+"' ");
                    if (dt2 != null)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            cb_DanhMuc.Text = dr2["TenDanhMuc"].ToString();
                        }
                    }
                }
            }
        }

        int iddm = -1;

        private void cb_DanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            iddm = Convert.ToInt32(cb_DanhMuc.SelectedValue.ToString());
        }

        private void Edit_SP_Load(object sender, EventArgs e)
        {
            dataDanhmuc();
        }

        private void btn_EditSP_Click(object sender, EventArgs e)
        {
            if (txt_MaSP.Text != "" || txt_TenSP.Text != "")
            {
                if (connect.exedata("Update MATHANG Set IDSanPham = '" + txt_MaSP.Text.ToString() + "' , TenSanPham = N'" + txt_TenSP.Text.ToString() + "' , NgaySX = '" + dtp_NgaySX.Value.Date.ToString("yyyy-MM-dd") + "' , XuatXu = N'" + txt_XuatXu.Text.ToString() + "' , SoLuongTon = " + Convert.ToInt32(nb_SoLuong.Value.ToString()) + " , Gia = " + Convert.ToInt32(txt_DonGia.Text.ToString()) + " , KhuyenMai = '" + Convert.ToInt32(txt_KhuyenMai.Text.ToString()) + "' , IDDanhMuc = " + iddm + " where IDSanPham = '"+id+"' ") == true)
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
                MessageBox.Show("Không thể sửa dữ liệu");
            }
        }

        private void btn_HuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
