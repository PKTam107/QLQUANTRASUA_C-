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
    public partial class frm_HDN : Form
    {
        DataTable tb_HDN = null;
        bool Them;
        BLHDN dbHDN = new BLHDN();
        public frm_HDN()
        {
            InitializeComponent();
        }
        void LoadComboBoxNCC()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbHDN.LayNhaCungCap();
                cmbNCC.DataSource = dt;
                cmbNCC.DisplayMember = "MaNCC";
                cmbNCC.ValueMember = "MaNCC";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhaCungCap. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LoadData()
        {
            try
            {
                tb_HDN = new DataTable();
                tb_HDN.Clear();
                tb_HDN = dbHDN.LayHoaDonNhap();
                dgvHDN.DataSource = tb_HDN;
                dgvHDN.AutoResizeColumns();
                this.txtMaHDN.ResetText();
                this.dtpNgayNhap.ResetText();
                this.cmbNCC.ResetText();
                this.txtThanhTien.ResetText();
                this.btnLuu.Enabled = false;
                this.gbHDN.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                this.txtMaHDN.Focus();
                //
                LoadComboBoxNCC();
                cmbNCC.SelectedIndexChanged += cmbNCC_SelectedIndexChanged;
                dgvHDN_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table HoaDonNhap. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_HDN_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaHDN.ResetText();
            this.dtpNgayNhap.ResetText();
            this.cmbNCC.ResetText();
            this.lblTenNCC.ResetText();
            this.txtThanhTien.Text = "0";
            this.txtThanhTien.Enabled = false;
            this.btnLuu.Enabled = true;
            this.gbHDN.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaHDN.Enabled = true;
            this.dtpNgayNhap.Focus();
            this.cmbNCC.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLHDN blHDN = new BLHDN();
                    frm_CTHDN fr = new frm_CTHDN();
                    blHDN.ThemHDN(this.txtMaHDN.Text, this.dtpNgayNhap.Text, this.txtThanhTien.Text, this.cmbNCC.Text);
                    LoadData();
                    MessageBox.Show("Đã thêm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    fr.Show();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không thêm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    BLHDN blHDN = new BLHDN();
                    frm_CTHDN fr = new frm_CTHDN();
                    blHDN.CapNhatHDN(this.txtMaHDN.Text, this.dtpNgayNhap.Text, this.txtThanhTien.Text, this.cmbNCC.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    fr.Show();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            this.gbHDN.Enabled = true;
            dgvHDN_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.gbHDN.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            string maNCC = cmbNCC.SelectedValue.ToString();
            DataRow[] r1 = dbHDN.LayNhaCungCap().Select("MaNCC='" + maNCC + "'");
            if (r1.Length > 0)
            {
                lblTenNCC.Text = r1[0]["TenNCC"].ToString();
            }
            this.txtMaHDN.Enabled = false;
            this.dtpNgayNhap.Focus();
            this.cmbNCC.Focus();
            this.txtThanhTien.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvHDN.CurrentCell.RowIndex;
                string strHDN =
                dgvHDN.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbHDN.XoaHDN(strHDN);
                    LoadData();
                    MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không xóa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvHDN.CurrentCell.RowIndex;
            dgvHDN.Columns[0].HeaderText = "Mã HDN";
            dgvHDN.Columns[0].Width = 100;
            dgvHDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaHDN.Text =
            dgvHDN.Rows[r].Cells[0].Value.ToString();
            dgvHDN.Columns[1].HeaderText = "Ngày Nhập";
            dgvHDN.Columns[1].Width = 100;
            this.dtpNgayNhap.Text =
            dgvHDN.Rows[r].Cells[1].Value.ToString();
            dgvHDN.Columns[2].HeaderText = "Trị giá đơn nhập";
            dgvHDN.Columns[2].Width = 100;
            this.txtThanhTien.Text =
            dgvHDN.Rows[r].Cells[2].Value.ToString();
            dgvHDN.Columns[3].HeaderText = "Mã NCC";
            dgvHDN.Columns[3].Width = 100;
            this.cmbNCC.Text =
            dgvHDN.Rows[r].Cells[3].Value.ToString();
        }

        private void cmbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maNCC = cmbNCC.SelectedValue.ToString();
            DataRow[] r1 = dbHDN.LayNhaCungCap().Select("MaNCC='" + maNCC + "'");
            if (r1.Length > 0)
            {
                lblTenNCC.Text = r1[0]["TenNCC"].ToString();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dgvHDN.AutoResizeColumns();
            this.txtMaHDN.ResetText();
            this.dtpNgayNhap.ResetText();
            this.cmbNCC.ResetText();
            this.txtThanhTien.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.gbHDN.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvHDN_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvHDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvHDN.CurrentCell.RowIndex;
            frm_CTHDN fr = new frm_CTHDN();
            this.Close();
            fr.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rbtnNgay.Checked)
            {
                try
                {
                    BLHDN blHDN = new BLHDN();
                    DataTable resultTable = blHDN.TimKiemHDNTheoNgay(this.txtTimKiem.Text);
                    dgvHDN.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvHDN.AutoResizeColumns();
                        dgvHDN_CellClick(null, null);
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
            else if (rbtnNCC.Checked)
            {
                try
                {
                    BLHDN blHDN = new BLHDN();
                    DataTable resultTable = blHDN.TimKiemHDNTheoNCC(this.txtTimKiem.Text);
                    dgvHDN.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvHDN.AutoResizeColumns();
                        dgvHDN_CellClick(null, null);
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
