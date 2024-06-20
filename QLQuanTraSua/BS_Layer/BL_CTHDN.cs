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
    internal class BL_CTHDN
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BL_CTHDN()
        {
            db = new DBMain();
        }
        public DataTable LayCTHDN()
        {
            SqlCommand command = new SqlCommand("select * from v_ChiTietDonNhap", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable LayMaNL()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadNguyenLieu", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayMaHDN()
        {
            SqlCommand command = new SqlCommand("select * from v_HoaDonNhap", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemCTHDN(string MaHDN, string MaNL, float DonGia, int SoLuong, string DonVi, float TongTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemChiTietDonNhap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDN", SqlDbType.NChar).Value = MaHDN;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
            command.Parameters.Add("@DonGia", SqlDbType.Float).Value = DonGia;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
            command.Parameters.Add("@DonVi", SqlDbType.NChar).Value = DonVi;
            command.Parameters.Add("@TongTien", SqlDbType.Float).Value = TongTien;
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
        public bool XoaCTHDN(string MaHDN, string MaNL)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaChiTietDonNhap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDN;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
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
        public bool CapNhatCTHDN(string MaHDN, string MaNL, float DonGia, int SoLuong, string DonVi, float TongTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaChiTietDonNhap", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDN", SqlDbType.NChar).Value = MaHDN;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
            command.Parameters.Add("@DonGia", SqlDbType.Float).Value = DonGia;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
            command.Parameters.Add("@DonVi", SqlDbType.NChar).Value = DonVi;
            command.Parameters.Add("@TongTien", SqlDbType.Float).Value = TongTien;
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
        public string LoadTenNL(string MaNL)
        {
            string tenNL = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenNL From v_LoadNguyenLieu where MaNL= N'" + MaNL + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenNL = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenNL;
        }
        public bool CapNhatThanhTien(string MaHDN)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_TinhTongTienHDN", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDN", SqlDbType.NChar).Value = MaHDN;
            db.openConnection();
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
