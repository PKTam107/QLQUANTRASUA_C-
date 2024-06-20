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
    internal class BL_LoaiSP
    {
        DBMain db = null;
        public BL_LoaiSP()
        {
            db = new DBMain();
        }
        public DataTable LayLoaiSanPham()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadLoaiSanPham", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemLoaiSanPham (string MaLoaiSP, string TenLoaiSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemLoaiSanPham", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaLoaiSP", SqlDbType.NChar).Value = MaLoaiSP;
            command.Parameters.Add("@TenLoaiSP", SqlDbType.NVarChar).Value = TenLoaiSP;
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
        public bool XoaLoaiSanPham(string MaLoaiSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaLoaiSanPham", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaLoaiSP", SqlDbType.NChar).Value = MaLoaiSP;
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
        public bool CapNhatLoaiSanPham(string MaLoaiSP, string TenLoaiSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaLoaiSanPham", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaLoaiSP", SqlDbType.NChar).Value = MaLoaiSP;
            command.Parameters.Add("@TenLoaiSP", SqlDbType.NVarChar).Value = TenLoaiSP;
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
