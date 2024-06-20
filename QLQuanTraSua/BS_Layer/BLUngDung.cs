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
    internal class BLUngDung
    {
        DBMain db = null;
        public BLUngDung()
        {
            db = new DBMain();
        }
        public DataTable LayUngDung()
        {
            SqlCommand command = new SqlCommand("select * from v_UngDung", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemUngDung(string MaUD, string TenUD, string ChietKhau)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaUD", SqlDbType.NChar).Value = MaUD;
            command.Parameters.Add("@TenUD", SqlDbType.NVarChar).Value = TenUD;
            command.Parameters.Add("@ChietKhau", SqlDbType.NChar).Value = ChietKhau;
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
        public bool XoaUngDung(string MaUD)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaUD", SqlDbType.NChar).Value = MaUD;
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
        public bool CapNhatUngDung(string MaUD, string TenUD, string ChietKhau)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaUD", SqlDbType.NChar).Value = MaUD;
            command.Parameters.Add("@TenUD", SqlDbType.NVarChar).Value = TenUD;
            command.Parameters.Add("@ChietKhau", SqlDbType.NChar).Value = ChietKhau;
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
