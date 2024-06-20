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
    public partial class frm_NCC : Form
    {
        DataTable tb_NCC = null;
        bool Them;
        BLNCC dbNCC = new BLNCC();
        public frm_NCC()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                tb_NCC = new DataTable();
                tb_NCC.Clear();
                tb_NCC = dbNCC.LayNhaCungCap();
                dgvNCC.DataSource = tb_NCC;
                dgvNCC.AutoResizeColumns();
                this.txtMaNCC.ResetText();
                this.txtTenNCC.ResetText();
                this.txtDChi.ResetText();
                this.txtSdt.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.gb1.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                //
                dgvNCC_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhaCungCap. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_NCC_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaNCC.ResetText();
            this.txtTenNCC.ResetText();
            this.txtDChi.ResetText();
            this.txtSdt.ResetText();
            this.btnLuu.Enabled = true;
            this.gb1.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaNCC.Enabled = true;
            this.txtMaNCC.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            dgvNCC_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.gb1.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaNCC.Enabled = false;
            this.txtTenNCC.Focus();
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNCC.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            int r = dgvNCC.CurrentCell.RowIndex;
            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[0].Width = 150;
            dgvNCC.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaNCC.Text =
            dgvNCC.Rows[r].Cells[0].Value.ToString();
            dgvNCC.Columns[1].HeaderText = "Tên NCC";
            dgvNCC.Columns[1].Width = 150;
            this.txtTenNCC.Text =
            dgvNCC.Rows[r].Cells[1].Value.ToString();
            dgvNCC.Columns[2].HeaderText = "Địa Chỉ";
            dgvNCC.Columns[2].Width = 150;
            this.txtDChi.Text =
            dgvNCC.Rows[r].Cells[2].Value.ToString();
            dgvNCC.Columns[3].HeaderText = "Số điện thoại";
            dgvNCC.Columns[3].Width = 150;
            this.txtSdt.Text =
            dgvNCC.Rows[r].Cells[3].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLNCC blNCC = new BLNCC();
                    blNCC.ThemNCC(this.txtMaNCC.Text, this.txtTenNCC.Text, this.txtDChi.Text, this.txtSdt.Text);
                    LoadData();
                    MessageBox.Show("Đã thêm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    BLNCC blNCC = new BLNCC();
                    blNCC.CapNhatNCC(this.txtMaNCC.Text, this.txtTenNCC.Text, this.txtDChi.Text, this.txtSdt.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvNCC.CurrentCell.RowIndex;
                string strNCC =
                dgvNCC.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbNCC.XoaNCC(strNCC);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dgvNCC.AutoResizeColumns();
            this.txtMaNCC.ResetText();
            this.txtTenNCC.ResetText();
            this.txtDChi.ResetText();
            this.txtSdt.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.gb1.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvNCC_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rbtnTenNCC.Checked)
            {
                try
                {
                    BLNCC blNCC = new BLNCC();
                    DataTable resultTable = blNCC.TimKiemNCC(this.txtTimKiem.Text);
                    dgvNCC.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvNCC.AutoResizeColumns();
                        dgvNCC_CellClick(null, null);
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
                    BLNCC blNCC = new BLNCC();
                    DataTable resultTable = blNCC.TimKiemNCCTheoSDT(this.txtTimKiem.Text);
                    dgvNCC.DataSource = resultTable;
                    if (resultTable != null)
                    {
                        dgvNCC.AutoResizeColumns();
                        dgvNCC_CellClick(null, null);
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
