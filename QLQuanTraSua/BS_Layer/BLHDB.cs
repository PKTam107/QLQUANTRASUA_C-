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
    internal class BLHDB
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BLHDB()
        {
            db = new DBMain();
        }
        public DataTable LayHoaDonBan()
        {
            SqlCommand command = new SqlCommand("select * from v_HoaDonBan", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable LayKhachHang()
        {
            SqlCommand command = new SqlCommand("select * from v_KhachHang", db.getConnection);
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
        public bool ThemHDB(string MaHDB, string Ngay, string SDT, string MaNV, int ThanhTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemHoaDonBan", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
            command.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
            command.Parameters.Add("@ThanhTien", SqlDbType.Int).Value = ThanhTien;
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
        public bool XoaHDB(string MaHDB)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaHoaDonBan", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
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
        public bool CapNhatHDBan(string MaHDB, string Ngay, string SDT, string MaNV, int ThanhTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaHoaDonBan", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
            command.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
            command.Parameters.Add("@ThanhTien", SqlDbType.Int).Value = ThanhTien;
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
        public string LoadTenKH(string SDT)
        {
            string tenKH = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenKH From v_KhachHang where SDT= N'" + SDT + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenKH = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenKH;
        }
        //load đơn giá bán
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
        public DataTable TimKiemHDBTheoNgay(string Ngay)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemHDBBangNgay(@NgayBan)", db.getConnection);
            command.Parameters.Add("@NgayBan", SqlDbType.Date).Value = Ngay;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
        public DataTable TimKiemHDBTheoSDT(string SDT)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemHDBBangSDT(@SDT)", db.getConnection);
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
        public DataTable TimKiemHDBTheoMaNV(string maNV)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM TimKiemHDBBangMaNV(@MaNV)", db.getConnection);
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = maNV;
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }
    }
}
