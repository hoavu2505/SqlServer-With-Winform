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
        public Edit_SP()
        {
            InitializeComponent();
            this.id = id;
            DataTable dt = connect.readdata("select * from MATHANG where IDSanPham = "+id+"");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_MaSP.Text = dr["IDSanPham"].ToString();
                    txt_TenSP.Text = dr["TenSanPham"].ToString();
                    dtp_NgaySX.Value = DateTime.Parse(dr["NgaySX"].ToString());
                    txt_XuatXu.Text = dr["XuatXu"].ToString();
                    nb_SoLuong.Value = Int32.Parse(dr["SoLuongTon"].ToString());
                    txt_DonGia.Text = dr["DonGia"].ToString();
                    txt_KhuyenMai.Text = dr["KhuyenMai"].ToString();
                }
            }
        }
    }
}
