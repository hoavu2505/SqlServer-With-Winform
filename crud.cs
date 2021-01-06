using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QLNhaHang
{
    class crud
    {
        SqlConnection conn = new SqlConnection(@"Data Source='DESKTOP-TR8V8B8\MSSQLSERVER2505';Initial Catalog='QLCuaHang';Integrated Security='True'");

        private void openconnect()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void closeconnect()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public Boolean exedata(string cmd)
        {
            openconnect();
            Boolean check = false;
            try
            {
                SqlCommand sc = new SqlCommand(cmd, conn); //Khai báo lệnh SQL
                sc.ExecuteNonQuery(); //Thực thi lệnh trên
                check = true; //Thực thi thành công thì gán check = true
            }
            catch (Exception)
            {
                check = false;
            }
            closeconnect();
            return check;
        }

        //READ
        public DataTable readdata(string cmd)
        {
            openconnect();
            DataTable da = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(da);
            }
            catch (Exception)
            {
                da = null;
            }
            closeconnect();
            return da;
        }
    }
}
