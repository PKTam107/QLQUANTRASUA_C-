using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QLQuanTraSua.DB_Layer;

namespace QLQuanTraSua.BS_Layer
{
    internal class BLKhachHang
    {
        DBMain db = null;
        public BLKhachHang()
        {
            db = new DBMain();
        }
        public DataTable LayKhachHang()
        {
            SqlCommand command = new SqlCommand("select * from v_KhachHang", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemKhachHang(string SDT, string TenKH, string DiaChi)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemKhachHang", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = TenKH;
            command.Parameters.Add("@DiaChi", SqlDbType.NChar).Value = DiaChi;
            if (command.ExecuteNonQuery() > 0)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool XoaKhachHang(string SDT)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaKhachHang", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            if (command.ExecuteNonQuery() > 0)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        public bool CapNhatKhachHang(string SDT, string TenKH, string DiaChi)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaNhaKhachHang", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = TenKH;
            command.Parameters.Add("@DiaChi", SqlDbType.NChar).Value = DiaChi;
            if (command.ExecuteNonQuery() > 0)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }

        public DataTable TimKiemKhachHangBangSDT(string sdt)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemKHBangSDT(@SDT)", db.getConnection);
            command.Parameters.AddWithValue("@SDT", sdt);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            return dt;
        }

        public DataTable TimKiemKhachHangBangDiaChi(string DiaChi)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemKHBangDiaChi(@DiaChi)", db.getConnection);
            command.Parameters.AddWithValue("@DiaChi", DiaChi);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            return dt;
        }

    }
}
