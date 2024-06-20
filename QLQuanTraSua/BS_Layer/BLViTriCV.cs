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
    internal class BLViTriCV
    {
        DBMain db = null;
        public BLViTriCV()
        {
            db = new DBMain();
        }
        public DataTable LayViTriCV()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadViTriCongViec", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemViTriCV(string MaViTri, string TenViTri, string PhuCapLuong)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemViTriCongViec", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaViTri", SqlDbType.NChar).Value = MaViTri;
            command.Parameters.Add("@TenViTri", SqlDbType.NVarChar).Value = TenViTri;
            command.Parameters.Add("@PhuCapLuong", SqlDbType.Float).Value = PhuCapLuong;
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
        public bool XoaViTriCV(string MaViTri)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaViTriCongViec", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaViTri", SqlDbType.NChar).Value = MaViTri;
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
        public bool CapNhatViTriCV(string MaViTri, string TenViTri, string PhuCapLuong)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaViTriCongViec", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaViTri", SqlDbType.NChar).Value = MaViTri;
            command.Parameters.Add("@TenViTri", SqlDbType.NVarChar).Value = TenViTri;
            command.Parameters.Add("@PhuCapLuong", SqlDbType.Float).Value = PhuCapLuong;
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
