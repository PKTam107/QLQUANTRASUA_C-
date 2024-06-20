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
    internal class BL_SanPham
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BL_SanPham()
        {
            db = new DBMain();
        }
        public DataTable LaySanPham()
        {
            SqlCommand command = new SqlCommand("select * from v_SanPham", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayLoaiSanPham()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadLoaiSanPham", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemSanPham(string MaSP, string TenSP, float DonGia, string TinhTrang, string MaLoaiSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemSanPham", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
            command.Parameters.Add("@TenSP", SqlDbType.NVarChar).Value = TenSP;
            command.Parameters.Add("@DonGia", SqlDbType.Float).Value = DonGia;
            command.Parameters.Add("@TinhTrang", SqlDbType.NChar).Value = TinhTrang;
            command.Parameters.Add("@MaLoaiSP", SqlDbType.NChar).Value = MaLoaiSP;
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
        public bool XoaSanPham(string MaSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaSanPham", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
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
        public bool CapNhatSanPham(string MaSP, string TenSP, float DonGia, string TinhTrang, string MaLoaiSP)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaSanPham", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
            command.Parameters.Add("@TenSP", SqlDbType.NVarChar).Value = TenSP;
            command.Parameters.Add("@DonGia", SqlDbType.Float).Value = DonGia;
            command.Parameters.Add("@MaLoaiSP", SqlDbType.NChar).Value = MaLoaiSP;
            command.Parameters.Add("@TinhTrang", SqlDbType.NChar).Value = TinhTrang;
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
        public string LoadTenLoaiSanPham(string MaLoaiSP)
        {
            string tenLoaiSP = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenLoaiSP From v_LoadLoaiSanPham where MaLoaiSP= N'" + MaLoaiSP + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenLoaiSP = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenLoaiSP;
        }

        public DataTable TimKiemSanPhamBangMaSP(string MaSP)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemSanPhamBangMaSP(@MaSP)", db.getConnection);
            command.Parameters.AddWithValue("@MaSP", MaSP);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemSanPhamBangMaLoaiSP(string MaLoaiSP)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemSanPhamBangMaLoaiSP(@MaLoaiSP)", db.getConnection);
            command.Parameters.AddWithValue("@MaLoaiSP", MaLoaiSP);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemSanPhamBangTenSP(string TenSP)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemSanPhamBangTenSP(@TenSP)", db.getConnection);
            command.Parameters.AddWithValue("@TenSP", TenSP);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

    }
}
