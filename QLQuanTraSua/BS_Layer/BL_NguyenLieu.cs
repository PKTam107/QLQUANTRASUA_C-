using QLQuanTraSua.DB_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanTraSua.BS_Layer
{
    internal class BL_NguyenLieu
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BL_NguyenLieu()
        {
            db = new DBMain();
        }
        public DataTable LayNguyenLieu()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadNguyenLieu", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayNhaCungCap()
        {
            SqlCommand command = new SqlCommand("select * from v_NhaCungCap", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemNguyenLieu(string MaNL, string TenNL, string MaNCC, int SoLuong, string DonVi)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemNguyenLieu", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
            command.Parameters.Add("@TenNl", SqlDbType.NVarChar).Value = TenNL;
            command.Parameters.Add("@MaNCC", SqlDbType.NChar).Value = MaNCC;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
            command.Parameters.Add("@DonVi", SqlDbType.NChar).Value = DonVi;
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
        public bool XoaNguyenLieu(string MaNL)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaNguyenLieu", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
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
        public bool CapNhatNguyenLieu(string MaNL, string TenNL, string MaNCC, int SoLuong, string DonVi)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaNguyenLieu", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
            command.Parameters.Add("@TenNl", SqlDbType.NVarChar).Value = TenNL;
            command.Parameters.Add("@MaNCC", SqlDbType.NChar).Value = MaNCC;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
            command.Parameters.Add("@DonVi", SqlDbType.NChar).Value = DonVi;
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
        public string LoadTenNhaCungCap(string MaNCC)
        {
            string tenNCC = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenNCC From v_NhaCungCap where MaNCC= N'" + MaNCC + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenNCC = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenNCC;
        }
        public DataTable TimKiemNL(string TenNL)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemNL(@TenNL)", db.getConnection);
            command.Parameters.Add("@TenNL", SqlDbType.NVarChar).Value = TenNL;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
    }
}
