using QLQuanTraSua.DB_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLQuanTraSua.BS_Layer
{
    internal class BLLoaiNV
    {
        DBMain db = null;
        public BLLoaiNV()
        {
            db = new DBMain();
        }
        public DataTable LayLoaiNV()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadLoaiNhanVien", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemLoaiNV(string MaLoaiNV, string TenLoaiNV, string LuongCB)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemLoaiNhanVien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaLoaiNV", SqlDbType.NChar).Value = MaLoaiNV;
            command.Parameters.Add("@TenLoaiNV", SqlDbType.NVarChar).Value = TenLoaiNV;
            command.Parameters.Add("@LuongCB", SqlDbType.Float).Value = LuongCB;
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
        public bool XoaLoaiNV(string MaLoaiNV)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaLoaiNhanVien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaLoaiNV", SqlDbType.NChar).Value = MaLoaiNV;
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
        public bool CapNhatLoaiNV(string MaLoaiNV, string TenLoaiNV, string LuongCB, ref string err)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaLoaiNhanVien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaLoaiNV", SqlDbType.NChar).Value = MaLoaiNV;
            command.Parameters.Add("@TenLoaiNV", SqlDbType.NVarChar).Value = TenLoaiNV;
            command.Parameters.Add("@LuongCB", SqlDbType.Float).Value = LuongCB;
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
    }
}
