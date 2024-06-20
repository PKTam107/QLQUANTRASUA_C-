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
    class BLNhanVien
    {
        DBMain db = null;
        public BLNhanVien()
        {
            db = new DBMain();
        }
        public DataTable LayNhanVien()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadNhanVien", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable LayViTri()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadViTriCongViec", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable LayLoaiNhanVien()
        {
            SqlCommand command = new SqlCommand("select * from v_LoadLoaiNhanVien", db.getConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public bool ThemNhanVien(string MaNV, string TenNV, string NgaySinh, string GioiTinh, string DiaChi, string SDT, string MaLoaiNV, string MaViTri, string NgayTuyenDung)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_ThemNhanVien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
            command.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = TenNV;
            command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = NgaySinh;
            command.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = GioiTinh;
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = DiaChi;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@MaLoaiNV", SqlDbType.NChar).Value = MaLoaiNV;
            command.Parameters.Add("@MaViTri", SqlDbType.NChar).Value = MaViTri;
            command.Parameters.Add("@NgayTuyenDung", SqlDbType.Date).Value = NgayTuyenDung;
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

        public bool XoaNhanVien(string MaNV)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_XoaNhanVien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
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

        public bool CapNhatNhanVien(string MaNV, string TenNV, string NgaySinh, string GioiTinh, string DiaChi, string SDT, string MaLoaiNV, string MaViTri, string NgayTuyenDung)
        {
            db.openConnection();
            SqlCommand command = new SqlCommand("proc_SuaNhanVien", db.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MaNV", SqlDbType.NChar).Value = MaNV;
            command.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = TenNV;
            command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = NgaySinh;
            command.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = GioiTinh;
            command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = DiaChi;
            command.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;
            command.Parameters.Add("@MaLoaiNV", SqlDbType.NChar).Value = MaLoaiNV;
            command.Parameters.Add("@MaViTri", SqlDbType.NChar).Value = MaViTri;
            command.Parameters.Add("@NgayTuyenDung", SqlDbType.Date).Value = NgayTuyenDung;
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

        public DataTable TimKiemNVBangTenNV(string TenNV)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemNVBangTenNV(@TenNV)", db.getConnection);
            command.Parameters.AddWithValue("@TenNV", TenNV);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
        public DataTable TimKiemNVBangSDT(string SDT)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemNVBangSDT(@SDT)", db.getConnection);
            command.Parameters.AddWithValue("@SDT", SDT);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemNVBangDiaChi(string DiaChi)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemNVBangDiaChi(@DiaChi)", db.getConnection);
            command.Parameters.AddWithValue("@DiaChi", DiaChi);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }

        public DataTable TimKiemNVBangNgayTuyenDung(string NgayTuyenDung)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.TimKiemNVBangNgayTuyenDung(@NgayTuyenDung)", db.getConnection);
            command.Parameters.AddWithValue("@NgayTuyenDung", NgayTuyenDung);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            return dt;
        }
    }
}
