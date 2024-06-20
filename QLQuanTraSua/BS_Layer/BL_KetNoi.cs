using QLQuanTraSua.DB_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanTraSua.BS_Layer
{
    internal class BL_KetNoi
    {
        DBMain db = new DBMain();
        SqlDataReader sqldr;

        public bool CheckLogin(string username, string password)
        {
            db.openConnection();
            SqlCommand cmd = new SqlCommand("SELECT dbo.checkLogin(@user, @pass)", db.getConnection);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);

            bool count = (bool)cmd.ExecuteScalar();
            db.closeConnection();

            return count;
        }

        public DataTable LayAccount()
        {
            SqlCommand command = new SqlCommand("select * from v_Account", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayNhanVien()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadNhanVien", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public bool DeleteEmployee(string MaNV)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_deleteEmployee", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;

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

        public bool UpdateEmployee(string username, string password, string MaNV, string roles)
        {
            db.openConnection();
            SqlCommand cmd = new SqlCommand("proc_updateAccount", db.getConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = password;
            cmd.Parameters.AddWithValue("@MaNV", SqlDbType.NChar).Value = MaNV;
            cmd.Parameters.AddWithValue("@roles", SqlDbType.VarChar).Value = roles;
            if (cmd.ExecuteNonQuery() > 0)
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

        public string LoadTenNV(string MaNV)
        {
            string tenNV = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenNV From v_NhanVien where MaNV= N'" + MaNV + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenNV = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenNV;
        }
    }
}
