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
    internal class BLCaLamViec
    {
        DBMain db = null;
        public BLCaLamViec()
        {
            db = new DBMain();
        }
        public DataTable LayCaLamViec()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadCaLamViec", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemCaLamViec(string MaCa, string Ngay, string GioBatDau, string GioKetThuc)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemCaLamViec", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaCa", SqlDbType.NChar).Value = MaCa;
            command.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
            command.Parameters.Add("@GioBatDau", SqlDbType.NChar).Value = GioBatDau;
            command.Parameters.Add("@GioKetThuc", SqlDbType.NChar).Value = GioKetThuc;
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
        public bool XoaCaLamViec(string MaCa, string Ngay)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaCaLamViec", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaCa", SqlDbType.NChar).Value = MaCa;
            command.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
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
        public bool CapNhatCaLamViec(string MaCa, string Ngay, string GioBatDau, string GioKetThuc)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaCaLamViec", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaCa", SqlDbType.NChar).Value = MaCa;
            command.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
            command.Parameters.Add("@GioBatDau", SqlDbType.NChar).Value = GioBatDau;
            command.Parameters.Add("@GioKetThuc", SqlDbType.NChar).Value = GioKetThuc;
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
