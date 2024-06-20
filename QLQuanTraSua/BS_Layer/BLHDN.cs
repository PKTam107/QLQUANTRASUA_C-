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
    internal class BLHDN
    {
        DBMain db = null;
        public BLHDN()
        {
            db = new DBMain();
        }
        public DataTable LayHoaDonNhap()
        {
            SqlCommand command = new SqlCommand("select * from v_HoaDonNhap", db.getConnection);
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
        public bool ThemHDN(string MaHDN, string NgayNhap, string TriGiaDN, string MaNCC)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemHoaDonNhap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDN", SqlDbType.NChar).Value = MaHDN;
            command.Parameters.Add("@NgayNhap", SqlDbType.Date).Value = NgayNhap;
            command.Parameters.Add("@TriGiaDonNhap", SqlDbType.Float).Value = TriGiaDN;
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

        public bool XoaHDN(string MaHDN)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaHoaDonNhap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDN", SqlDbType.NChar).Value = MaHDN;
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

        public bool CapNhatHDN(string MaHDN, string NgayNhap, string MaNCC, string TriGiaDN)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaHoaDonNhap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDN", SqlDbType.NChar).Value = MaHDN;
            command.Parameters.Add("@NgayNhap", SqlDbType.Date).Value = NgayNhap;
            command.Parameters.Add("@TriGiaDonNhap", SqlDbType.Float).Value = TriGiaDN;
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
        public DataTable TimKiemHDNTheoNgay(string Ngay)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemHDNBangNgay(@NgayNhap)", db.getConnection);
            command.Parameters.Add("@NgayNhap", SqlDbType.Date).Value = Ngay;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
        public DataTable TimKiemHDNTheoNCC(string MaNCC)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemHDNBangMaNCC(@MaNCC)", db.getConnection);
            command.Parameters.Add("@MaNCC", SqlDbType.NChar).Value = MaNCC;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
    }
}
