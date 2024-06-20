using QLQuanTraSua.DB_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanTraSua.BS_Layer
{
    internal class BLNCC
    {
        DBMain db = null;
        public BLNCC()
        {
            db = new DBMain();
        }
        public DataTable LayNhaCungCap()
        {
            SqlCommand command = new SqlCommand("select * from v_NhaCungCap", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemNCC(string MaNCC, string TenNCC, string DiaChi, string SDT)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemNhaCungCap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNCC", SqlDbType.NChar).Value = MaNCC;
            command.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = TenNCC;
            command.Parameters.Add("@DiaChi", SqlDbType.NChar).Value = DiaChi;
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
        public bool XoaNCC(string MaNCC)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaNhaCungCap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNCC", SqlDbType.NChar).Value = MaNCC;
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
        public bool CapNhatNCC(string MaNCC, string TenNCC, string DiaChi, string SDT)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaNhaCungCap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNCC", SqlDbType.NChar).Value = MaNCC;
            command.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = TenNCC;
            command.Parameters.Add("@DiaChi", SqlDbType.NChar).Value = DiaChi;
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
        public DataTable TimKiemNCC(string TenNCC)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemNCCBangTenNCC(@TenNCC)", db.getConnection);
            command.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = TenNCC;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
        public DataTable TimKiemNCCTheoSDT(string SDT)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemNCCBangSDT(@SDT)", db.getConnection);
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
    }
}
