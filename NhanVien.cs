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
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
        }

        crud connect = new crud();

        //LOAD DATA
        private void dataLichSuHoaDonTheoNgay(string time1, string time2)
        {
            DataTable dt = connect.readdata("select * from ft_LichSuHoaDon('" + time1 + "', '" + time2 + "')");
            if (dt != null)
            {
                dtGridLichSuHoaDon.DataSource = dt;
            }
        }

        private void dataChonSanPham()
        {
            DataTable dt = connect.readdata("select * from ChonSanPham");
            DataTable tempDT = new DataTable();
            tempDT = dt.DefaultView.ToTable(true, "TenSanPham", "Gia");
            if (dt != null)
            {
                dtGridChonSanPham.DataSource = dt;
            }
        }

        private void dataHoaDonBanHang()
        {
            DataTable dt = connect.readdata("Select * from HoaDonBanHang");
            DataTable dt2 = connect.readdata("Select TongTien from HOADON where IDHoaDon in (select max(IDHoaDon) from HOADON)");
            if (dt != null)
            {
                dtGridHoaDonBanHang.DataSource = dt;
                foreach (DataRow dr in dt2.Rows)
                {
                    txt_TongTien.Text = dr["TongTien"].ToString();
                }
            }
        }

        private void btn_TKHoaDon_Click(object sender, EventArgs e)
        {
            string time1 = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            string time2 = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
            dataLichSuHoaDonTheoNgay(time1, time2);
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            dataChonSanPham();
        }

        int vitri1 = -1;
        int vitri2 = -1;

        private void dtGridChonSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitri1 = dtGridChonSanPham.CurrentCell.RowIndex;
        }

        int tongsosanpham = 0;
        private void btn_ThemSP_Click(object sender, EventArgs e)
        {
            DataTable dt = connect.readdata("Select * from MATHANG where IDSanPham = '" + dtGridChonSanPham.Rows[vitri1].Cells[0].Value + "'");
            if (dt != null)
            {
                if (tongsosanpham == 0)
                {
                    if (connect.exedata("insert into HOADON(NgayTao, IDNhanVien, IDKhach) values (getdate(), " + Login.ID + ", " + ID_KH + ")"))
                    {
                        tongsosanpham = tongsosanpham + Convert.ToInt32(nb_SoLuong.Value);
                        DataTable dt2 = connect.readdata("select max(IDHoaDon) as sohoadon from HOADON");
                        int idhoadon = -1;
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            idhoadon = Convert.ToInt32(dr2["sohoadon"].ToString());
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            connect.exedata("Execute sp_ThemSanPhamCTHoaDon " + idhoadon + ", " + dr["IDSanPham"].ToString() + ", " + Convert.ToInt32(nb_SoLuong.Value.ToString()) + ", " + Convert.ToInt32(dr["Gia"].ToString()) + ", " + Convert.ToDouble(dr["KhuyenMai"].ToString()));
                            connect.exedata("Execute sp_CapNhatHoaDon " + idhoadon);
                        }
                    }
                }
                else
                {
                    tongsosanpham = tongsosanpham + Convert.ToInt32(nb_SoLuong.Value);
                    DataTable dt2 = connect.readdata("select max(IDHoaDon) as sohoadon from HOADON");
                    int idhoadon = -1;
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        idhoadon = Convert.ToInt32(dr2["sohoadon"].ToString());
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        connect.exedata("Execute sp_ThemSanPhamCTHoaDon " + idhoadon + ", " + dr["IDSanPham"].ToString() + ", " + Convert.ToInt32(nb_SoLuong.Value.ToString()) + ", " + Convert.ToInt32(dr["Gia"].ToString()) + ", " + Convert.ToDouble(dr["KhuyenMai"].ToString()));
                        connect.exedata("Execute sp_CapNhatHoaDon " + idhoadon);
                    }
                }
            }
            dataHoaDonBanHang();
        }

        private void btn_XoaSP_Click(object sender, EventArgs e)
        {
            DataTable dt = connect.readdata("select max(IDHoaDon) as sohoadon from HOADON");
            int idhoadon = -1;
            foreach (DataRow dr in dt.Rows)
            {
                idhoadon = Convert.ToInt32(dr["sohoadon"].ToString());
            }
            if (connect.exedata("Delete from CT_HOADON where IDHoaDon = " + idhoadon + " and " + "IDSanPham = '" + dtGridHoaDonBanHang.Rows[vitri2].Cells[0].Value + "'"))
            {
                tongsosanpham = tongsosanpham - Convert.ToInt32(dtGridHoaDonBanHang.Rows[vitri2].Cells[2].Value.ToString());
                if (tongsosanpham > 0)
                {
                    connect.exedata("Execute sp_CapNhatHoaDon " + idhoadon);
                    dataHoaDonBanHang();
                }
                else
                {
                    connect.exedata("Delete from HOADON where IDHoaDon = " + idhoadon);
                    dtGridHoaDonBanHang.Rows.RemoveAt(vitri2);
                    txt_TongTien.Text = "0";
                }
            }
        }

        private void dtGridHoaDonBanHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitri2 = dtGridHoaDonBanHang.CurrentCell.RowIndex;
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            tongsosanpham = 0;
            if (Convert.ToInt32(txt_TienKhachTra.Text.ToString()) > Convert.ToInt32(txt_TongTien.Text.ToString()))
            {
                vitri1 = -1;
                vitri2 = -1;

                DataTable dt = connect.readdata("Select * from HoaDonBanHang");
                dt.Clear();
                dtGridHoaDonBanHang.DataSource = dt;
                txt_TienKhachTra.Text = "0";
                txt_TienGiaLai.Text = "0";
                txt_TongTien.Text = "0";
                txt_SDT.Text = "";
                txt_HoTen.Text = "";
            }
            else
            {
                MessageBox.Show("Tiền nhận từ khách đang nhỏ hơn tiền cần phải thanh toán!");
            }
            
        }

        private void txt_TienKhachTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_TienGiaLai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_TienKhachTra_TextChanged(object sender, EventArgs e)
        {
            if (txt_TienKhachTra.Text == "")
            {
                txt_TienKhachTra.Text = "0";
            }
            txt_TienGiaLai.Text = (Convert.ToInt32(txt_TienKhachTra.Text.ToString()) - Convert.ToInt32(txt_TongTien.Text.ToString())).ToString();
        }


        //GET ID KHACH HANG
        private string getIDKhach(string sdt)
        {
            string id = "";
            DataTable dt = connect.readdata("select IDKhach from KHACHHANG where SDT = " + txt_SDT.Text + " ");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    id = dr["IDKhach"].ToString();
                }
            }
            return id;
        }

        //GET Ten KHACH HANG
        private string getTenKhach(string sdt)
        {
            string hoten = "";
            DataTable dt = connect.readdata("select HoTen from KHACHHANG where SDT = " + txt_SDT.Text + " ");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    hoten = dr["HoTen"].ToString();
                }
            }
            return hoten;
        }

        public static string ID_KH = "0";
        public static string Ten_KH = "";

        private void btn_KiemTra_Click(object sender, EventArgs e)
        {
            ID_KH = getIDKhach(txt_SDT.Text);
            if (txt_SDT.Text != "")
            {
                if (ID_KH != "")
                {
                    Ten_KH = getTenKhach(txt_SDT.Text);
                    txt_HoTen.Text = Ten_KH;
                }
                else
                {
                    DialogResult dlr = MessageBox.Show("Số điện thoại chưa có trên hệ thống. Bạn có muốn thêm ?", "Thông Báo", MessageBoxButtons.YesNo);
                    if (dlr == DialogResult.Yes)
                    {
                        Add_KH themKH = new Add_KH();
                        themKH.ShowDialog();
                    }    
                }
            }
            else
            {
                MessageBox.Show("Hãy nhập Số điện thoại");
            }
        }
    }
}
