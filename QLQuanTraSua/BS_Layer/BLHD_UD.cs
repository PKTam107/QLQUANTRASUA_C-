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
    internal class BLHD_UD
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BLHD_UD()
        {
            db = new DBMain();
        }
        public DataTable LayHoaDonUngDung()
        {
            SqlCommand command = new SqlCommand("select * from v_HoaDonUngDung", db.getConnection);
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

        public DataTable LayUngDung()
        {
            SqlCommand command = new SqlCommand("select * from v_UngDung", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemHoaDonUngDung(string MaHDUD, string Ngay, string SDT, string MaUD, string MaNV, int ThanhTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemHoaDonUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHD_UD", SqlDbType.NChar).Value = MaHDUD;
            command.Parameters.Add("@Ngay", SqlDbType.NVarChar).Value = Ngay;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@MaUD", SqlDbType.NChar).Value = MaUD;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
            command.Parameters.Add("@GiaTriHD", SqlDbType.Int).Value = ThanhTien;
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
        public bool XoaHoaDonUngDung(string MaHDUD)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaHoaDonUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHD_UD", SqlDbType.NChar).Value = MaHDUD;
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
        public bool CapNhatHoaDonUngDung(string MaHDUD, string Ngay, string SDT, string MaUD, string MaNV, int ThanhTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_CapNhatHoaDonUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHD_UD", SqlDbType.NChar).Value = MaHDUD;
            command.Parameters.Add("@Ngay", SqlDbType.NVarChar).Value = Ngay;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@MaUD", SqlDbType.NChar).Value = MaUD;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
            command.Parameters.Add("@GiaTriHD", SqlDbType.Int).Value = ThanhTien;
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

        public string LoadTenUD(string MaUD)
        {
            string tenUD = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenUD From v_UngDung where MaUD= N'" + MaUD + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenUD = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenUD;
        }

        public DataTable TimKiemHoaDonBangMaHD(string MaHDUD)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemHDUDBangMaHDUD(@MaHD_UD)", db.getConnection);
            command.Parameters.AddWithValue("@MaHD_UD", MaHDUD);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemHoaDonBangNgayBan(string NgayBan)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemHDUDBangNgayBan(@Ngay)", db.getConnection);
            command.Parameters.AddWithValue("@Ngay", NgayBan);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemHoaDonBangSDT(string SDT)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemHDUDBangSDT(@SDT)", db.getConnection);
            command.Parameters.AddWithValue("@SDT", SDT);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemHoaDonBangMaNV(string MaNV)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemHDUDBangMaNV(@MaNV)", db.getConnection);
            command.Parameters.AddWithValue("@MaNV", MaNV);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
    }
}
