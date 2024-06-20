using QLQuanTraSua.BS_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanTraSua
{
    public partial class frm_HDB : Form
    {
        DataTable tb_HDB = null;
        bool Them;
        BLHDB dbHDB = new BLHDB();
        public frm_HDB()
        {
            InitializeComponent();
        }
        void LoadComboBoxKH()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbHDB.LayKhachHang();
                cmbKH.DataSource = dt;
                cmbKH.DisplayMember = "SDT";
                cmbKH.ValueMember = "SDT";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table KhachHang. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LoadComboBoxNV()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbHDB.LayNhanVien();
                cmbNV.DataSource = dt;
                cmbNV.DisplayMember = "MaNV";
                cmbNV.ValueMember = "MaNV";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadData()
        {
            try
            {
                tb_HDB = new DataTable();
                tb_HDB.Clear();
                tb_HDB = dbHDB.LayHoaDonBan();
                dgvHDB.DataSource = tb_HDB;
                dgvHDB.AutoResizeColumns();
                this.txtMaHDB.ResetText();
                this.dtpNgayBan.ResetText();
                this.cmbNV.ResetText();
                this.cmbKH.ResetText();
                this.txtThanhTien.ResetText();
                this.txtMaHDB.Enabled = false;
                this.dtpNgayBan.Enabled = false;
                this.cmbKH.Enabled = false;
                this.cmbNV.Enabled = false;
                this.txtThanhTien.Enabled = false;
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;

                LoadComboBoxKH();
                cmbKH.SelectedIndexChanged += cmbKH_SelectedIndexChanged;
                LoadComboBoxNV();
                cmbNV.SelectedIndexChanged += cmbNV_SelectedIndexChanged;
                dgvHDB_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table HoaDonBan. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fr_HDB_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaHDB.ResetText();
            this.dtpNgayBan.ResetText();
            this.cmbNV.ResetText();
            this.lblTenKH.ResetText();
            this.lblTenNV.ResetText();
            this.txtMaHDB.Enabled = true;
            this.dtpNgayBan.Enabled = true;
            this.cmbKH.Enabled = true;
            this.cmbNV.Enabled = true;
            this.txtThanhTien.Enabled = false;
            this.cmbKH.ResetText();
            this.txtThanhTien.Text = "0";
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.dtpNgayBan.Focus();
            this.cmbNV.Focus();
            this.cmbKH.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLHDB blHDB = new BLHDB();
                    blHDB.ThemHDB(this.txtMaHDB.Text, this.dtpNgayBan.Text, this.cmbKH.Text, this.cmbNV.Text, int.Parse(this.txtThanhTien.Text));
                    LoadData();
                    MessageBox.Show("Đã thêm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frm_CTHDB fr = new frm_CTHDB();
                    this.Close();
                    fr.Show();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    BLHDB blHDB = new BLHDB();
                    blHDB.CapNhatHDBan(this.txtMaHDB.Text, this.dtpNgayBan.Text, this.cmbKH.Text, this.cmbNV.Text, int.Parse(this.txtThanhTien.Text));
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                    frm_CTHDB fr = new frm_CTHDB();
                    this.Close();
                    fr.Show();
                }

                catch
                {
                    MessageBox.Show("Không sửa được. Lỗi rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            dgvHDB_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.gbHDB.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;

            this.txtMaHDB.Enabled = false;
            this.dtpNgayBan.Enabled = true;
            this.cmbKH.Enabled = true;
            this.cmbNV.Enabled = true;
            this.txtThanhTien.Enabled = false;
            this.txtThanhTien.Enabled = false;
            this.dtpNgayBan.Focus();
            this.cmbNV.Focus();
            this.cmbKH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvHDB.CurrentCell.RowIndex;
                string strHDB =
                dgvHDB.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbHDB.XoaHDB(strHDB);
                    LoadData();
                    MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvHDB.CurrentCell.RowIndex;
            dgvHDB.Columns[0].HeaderText = "Mã Hóa Đơn Bán";
            dgvHDB.Columns[0].Width = 100;
            dgvHDB.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaHDB.Text =
            dgvHDB.Rows[r].Cells[0].Value.ToString();
            dgvHDB.Columns[1].HeaderText = "Ngày Bán";
            dgvHDB.Columns[1].Width = 100;
            this.dtpNgayBan.Text =
            dgvHDB.Rows[r].Cells[1].Value.ToString();
            dgvHDB.Columns[2].HeaderText = "SDT Khách Hàng";
            dgvHDB.Columns[2].Width = 100;
            this.cmbKH.Text =
            dgvHDB.Rows[r].Cells[2].Value.ToString();
            dgvHDB.Columns[3].HeaderText = "Mã Nhân Viên";
            dgvHDB.Columns[3].Width = 100;
            this.cmbNV.Text =
            dgvHDB.Rows[r].Cells[3].Value.ToString();
            dgvHDB.Columns[4].HeaderText = "Thành Tiền";
            dgvHDB.Columns[4].Width = 100;
            this.txtThanhTien.Text =
            dgvHDB.Rows[r].Cells[4].Value.ToString();

            string SDT = this.cmbKH.Text;
            DataRow[] r1 = dbHDB.LayKhachHang().Select("SDT='" + SDT + "'");
            if (r1.Length > 0)
            {
                this.lblTenKH.Text = r1[0]["TenKH"].ToString();
            }
            string maNV = this.cmbNV.Text;
            DataRow[] r2 = dbHDB.LayNhanVien().Select("MaNV='" + maNV + "'");
            if (r2.Length > 0)
            {
                this.lblTenNV.Text = r2[0]["TenNV"].ToString();
            }
        }

        private void cmbKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenKH.Text = dbHDB.LoadTenKH(cmbKH.Text);
        }

        private void cmbNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenNV.Text = dbHDB.LoadTenNV(cmbNV.Text);
        }

        private void dgvHDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvHDB.CurrentCell.RowIndex;
            frm_CTHDB fr = new frm_CTHDB();
            //fr.MaHDB = dgvHDB.Rows[r].Cells[0].Value.ToString();
            this.Close();
            fr.Show();
        }


        private void Reaload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dgvHDB_CellClick(null, null);
            dgvHDB.AutoResizeColumns();
            this.txtMaHDB.ResetText();
            this.dtpNgayBan.ResetText();
            this.cmbNV.ResetText();
            this.cmbKH.ResetText();
            this.txtThanhTien.ResetText();

            this.txtMaHDB.Enabled = false;
            this.dtpNgayBan.Enabled = false;
            this.cmbKH.Enabled = false;
            this.cmbNV.Enabled = false;
            this.txtThanhTien.Enabled = false;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rbtnNgay.Checked)
            {
                try
                {
                    BLHDB blHDB = new BLHDB();
                    DataTable resultTable = blHDB.TimKiemHDBTheoNgay(this.txtTimKiem.Text);
                    dgvHDB.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvHDB.AutoResizeColumns();
                        dgvHDB_CellClick(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Đã tìm được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không tìm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbtnSDT.Checked)
            {
                try
                {
                    BLHDB blHDB = new BLHDB();
                    DataTable resultTable = blHDB.TimKiemHDBTheoSDT(this.txtTimKiem.Text);
                    dgvHDB.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvHDB.AutoResizeColumns();
                        dgvHDB_CellClick(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Đã tìm được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không tìm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbtnMaNV.Checked)
            {
                try
                {
                    BLHDB blHDB = new BLHDB();
                    DataTable resultTable = blHDB.TimKiemHDBTheoMaNV(this.txtTimKiem.Text);
                    dgvHDB.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvHDB.AutoResizeColumns();
                        dgvHDB_CellClick(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Đã tìm được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không tìm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Vui lòng chọn thông tin muốn tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
