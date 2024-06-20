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
    class BLBangPhanCa
    {
        DBMain db = null;
        public BLBangPhanCa()
        {
            db = new DBMain();
        }
        public DataTable LayBangPhanCa()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadBangPhanCa", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayCaLamViec()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadCaLamViec", db.getConnection);
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

        public bool ThemBangPhanCa(string MaCa, string MaNV, string Ngay)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemBangPhanCa", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaCa", SqlDbType.NChar).Value = MaCa;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
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

        public bool XoaBangPhanCa(string MaCa, string MaNV, string Ngay)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaBangPhanCa", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaCa", SqlDbType.NChar).Value = MaCa;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
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

        public bool CapNhatBangPhanCa(string MaCa, string MaNV, string Ngay)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaBangPhanCa", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaCa", SqlDbType.NChar).Value = MaCa;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
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

        public DataTable TimKiemBangPhanCaBangMaNV(string MaNV)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemBangPhanCaBangMaNV(@MaNV)", db.getConnection);
            command.Parameters.AddWithValue("@MaNV", MaNV);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemBangPhanCaBangNgay(string Ngay)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemBangPhanCaBangNgay(@Ngay)", db.getConnection);
            command.Parameters.AddWithValue("@Ngay", Ngay);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemBangPhanCaBangMaCa(string MaCa)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemBangPhanCaBangMaCa(@MaCa)", db.getConnection);
            command.Parameters.AddWithValue("@MaCa", MaCa);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
    }
}
