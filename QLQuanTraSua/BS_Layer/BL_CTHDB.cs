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
    internal class BL_CTHDB
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BL_CTHDB()
        {
            db = new DBMain();
        }
        public DataTable LayCTHDB()
        {
            SqlCommand command = new SqlCommand("select * from v_ChiTietDonBan", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable LayMaSP()
        {
            SqlCommand command = new SqlCommand("select * from v_SanPham", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemCTHDB(string MaHDB, string MaSP, int SoLuong, float DonGia, float TongTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemChiTietDonBan", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
            command.Parameters.Add("@DonGia", SqlDbType.Float).Value = DonGia;
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
        public bool XoaCTHDB(string MaHDB, string MaSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaChiTietDonBan", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
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
        public bool CapNhatCTHDB(string MaHDB, string MaSP, int SoLuong, float DonGia, float TongTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaChiTietDonBan", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
            command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = SoLuong;
            command.Parameters.Add("@DonGia", SqlDbType.Float).Value = DonGia;
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
        public string LoadTenSP(string MaSP)
        {
            string tenSP = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenSP From v_SanPham where MaSP= N'" + MaSP + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenSP = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenSP;
        }
        //load đơn giá bán
        public string LoadDonGia(string MaSP)
        {
            string donGia = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT DonGia From v_SanPham where MaSP= N'" + MaSP + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                donGia = sqldr[0].ToString();
            }
            db.closeConnection();
            return donGia;
        }
        public bool CapNhatThanhTien(string MaHDB)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_TinhTongTienHDB", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHDB", SqlDbType.NChar).Value = MaHDB;
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
