using QLQuanTraSua.DB_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanTraSua.BS_Layer
{
    internal class BLCTHD_UD
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BLCTHD_UD()
        {
            db = new DBMain();
        }
        public DataTable LayChiTietHoaDonUngDung()
        {
            SqlCommand command = new SqlCommand("select * from v_ChiTietHoadonUngDung", db.getConnection);
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
        public bool ThemChiTietHoaDonUngDung(string MaCTHDUD, string MaSP, int SoLuong, float DonGia, float TongTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemChiTietHoaDonUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHD_UD", SqlDbType.NChar).Value = MaCTHDUD;
            command.Parameters.Add("@MaSP", SqlDbType.NVarChar).Value = MaSP;
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
        public bool XoaChiTietHoaDonUngDung(string MaCTHDUD, string MaSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaChiTietHoaDonUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHD_UD", SqlDbType.NChar).Value = MaCTHDUD;
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
        public bool CapNhatChiTietHoaDonUngDung(string MaCTHDUD, string MaSP, int SoLuong, float DonGia, float TongTien)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaChiTietHoaDonUngDung", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaHD_UD", SqlDbType.NChar).Value = MaCTHDUD;
            command.Parameters.Add("@MaSP", SqlDbType.NVarChar).Value = MaSP;
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
            db.openConnection();
            string tenSP = "";
            SqlCommand command = new SqlCommand("SELECT TenSP From v_SanPham where MaSP= N'" + MaSP + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenSP = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenSP;
        }

        public string LoadDonGia(string MaSP)
        {
            db.openConnection();
            string donGia = "";
            SqlCommand command = new SqlCommand("SELECT DonGia From v_SanPham where MaSP= N'" + MaSP + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                donGia = sqldr[0].ToString();
            }
            db.closeConnection();
            return donGia;
        }

        public bool CapNhatThanhTien(string MaHDUD)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_TinhTongTienHoaDonUngDung", db.getConnection);
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
    }
}
