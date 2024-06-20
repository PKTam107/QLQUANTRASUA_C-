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
    internal class BL_CheBien
    {
        DBMain db = null;
        SqlDataReader sqldr;
        public BL_CheBien()
        {
            db = new DBMain();
        }
        public DataTable LayCheBien()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadCheBien", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LaySanPham()
        {
            SqlCommand command = new SqlCommand("select * from v_SanPham", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayNguyenLieu()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadNguyenLieu", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemCheBien(string MaSP, string MaNL, int LieuLuong, string DonVi)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemCheBien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
            command.Parameters.Add("@LieuLuong", SqlDbType.Int).Value = LieuLuong;
            command.Parameters.Add("@DonVi", SqlDbType.NChar).Value = DonVi;
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
        public bool XoaCheBien(string MaSP, string MaNL)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaCheBien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
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
        public bool CapNhatCheBien(string MaSP, string MaNL, int LieuLuong, string DonVi)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaCheBien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaSP", SqlDbType.NChar).Value = MaSP;
            command.Parameters.Add("@MaNL", SqlDbType.NChar).Value = MaNL;
            command.Parameters.Add("@LieuLuong", SqlDbType.Int).Value = LieuLuong;
            command.Parameters.Add("@DonVi", SqlDbType.NChar).Value = DonVi;
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
        public string LoadTenSanPham(string MaSP)
        {
            string tenSP = "";
            db.openConnection();
            SqlCommand command = new SqlCommand("SELECT TenSP From v_LoadSanPham where MaSP= N'" + MaSP + "'", db.getConnection);
            sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                tenSP = sqldr[0].ToString();
            }
            db.closeConnection();
            return tenSP;
        }

        public string LoadTenNguyenLieu(string MaNL)
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

        public DataTable TimKiemCheBienBangMaSP(string MaSP)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemCheBienBangMaSP(@MaSP)", db.getConnection);
            command.Parameters.AddWithValue("@MaSP", MaSP);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemCheBienBangMaNL(string MaNL)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemCheBienBangMaNL(@MaNL)", db.getConnection);
            command.Parameters.AddWithValue("@MaNL", MaNL);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
    }
}
