using QLQuanTraSua.BS_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanTraSua
{
    public partial class frm_NguyenLieu : Form
    {
        DataTable tb_NL = null;
        bool Them;
        BL_NguyenLieu dbNL = new BL_NguyenLieu();
        public frm_NguyenLieu()
        {
            InitializeComponent();
        }

        void LoadComboBoxNCC()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbNL.LayNhaCungCap();
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
                tb_NL = new DataTable();
                tb_NL.Clear();
                tb_NL = dbNL.LayNguyenLieu();
                dgvNGUYENLIEU.DataSource = tb_NL;
                dgvNGUYENLIEU.AutoResizeColumns();
                this.txtMaNL.ResetText();
                this.txtTenNL.ResetText();
                this.cmbNCC.ResetText();
                this.txtSoLuong.ResetText();
                this.txtDonVi.ResetText();
                this.txtMaNL.Enabled = false;
                this.txtTenNL.Enabled = false;
                this.cmbNCC.Enabled = false;
                this.txtSoLuong.Enabled = false;
                this.txtDonVi.Enabled = false;
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;

                LoadComboBoxNCC();
                cmbNCC.SelectedIndexChanged += cmbNCC_SelectedIndexChanged;
                dgvNGUYENLIEU_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NguyenLieu. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_NguyenLieu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaNL.ResetText();
            this.txtTenNL.ResetText();
            this.cmbNCC.ResetText();
            this.txtSoLuong.ResetText();
            this.txtDonVi.ResetText();
            this.lblTenNCC.ResetText();
            this.txtMaNL.Enabled = true;
            this.txtTenNL.Enabled = true;
            this.cmbNCC.Enabled = true;
            this.txtSoLuong.Enabled = true;
            this.txtDonVi.Enabled = true;
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaNL.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BL_NguyenLieu blNL = new BL_NguyenLieu();
                    blNL.ThemNguyenLieu(this.txtMaNL.Text, this.txtTenNL.Text, this.cmbNCC.Text, int.Parse(this.txtSoLuong.Text), this.txtDonVi.Text);
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

                    BL_NguyenLieu blNL = new BL_NguyenLieu();
                    blNL.CapNhatNguyenLieu(this.txtMaNL.Text, this.txtTenNL.Text, this.cmbNCC.Text, int.Parse(this.txtSoLuong.Text), this.txtDonVi.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dgvNGUYENLIEU_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.gbNL.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;

            this.txtMaNL.Enabled = false;
            this.txtTenNL.Enabled = true;
            this.cmbNCC.Enabled = true;
            this.txtSoLuong.Enabled = true;
            this.txtDonVi.Enabled = true;
            this.txtMaNL.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvNGUYENLIEU.CurrentCell.RowIndex;
                string strMaNL =
                dgvNGUYENLIEU.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbNL.XoaNguyenLieu(strMaNL);
                    LoadData();
                    MessageBox.Show("Đã xóa xong!");
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không xóa được. Lỗi: " + ex.Message);
            }
        }

        private void dgvNGUYENLIEU_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvNGUYENLIEU.CurrentCell.RowIndex;
            dgvNGUYENLIEU.Columns[0].HeaderText = "Mã Nguyên Liệu";
            dgvNGUYENLIEU.Columns[0].Width = 100;
            dgvNGUYENLIEU.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaNL.Text =
            dgvNGUYENLIEU.Rows[r].Cells[0].Value.ToString();
            dgvNGUYENLIEU.Columns[1].HeaderText = "Tên Nguyên Liệu";
            dgvNGUYENLIEU.Columns[1].Width = 100;
            this.txtTenNL.Text =
            dgvNGUYENLIEU.Rows[r].Cells[1].Value.ToString();
            dgvNGUYENLIEU.Columns[2].HeaderText = "Nhà Cung Cấp";
            dgvNGUYENLIEU.Columns[2].Width = 100;
            this.cmbNCC.Text =
            dgvNGUYENLIEU.Rows[r].Cells[2].Value.ToString();
            dgvNGUYENLIEU.Columns[3].HeaderText = "Số Lượng";
            dgvNGUYENLIEU.Columns[3].Width = 100;
            this.txtSoLuong.Text =
            dgvNGUYENLIEU.Rows[r].Cells[3].Value.ToString();
            dgvNGUYENLIEU.Columns[4].HeaderText = "Đơn Vị";
            dgvNGUYENLIEU.Columns[4].Width = 100;
            this.txtDonVi.Text =
            dgvNGUYENLIEU.Rows[r].Cells[4].Value.ToString();

            string MaNCC = this.cmbNCC.Text;
            DataRow[] r1 = dbNL.LayNhaCungCap().Select("MaNCC='" + MaNCC + "'");
            if (r1.Length > 0)
            {
                this.lblTenNCC.Text = r1[0]["TenNCC"].ToString();
            }
        }

        private void cmbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenNCC.Text = dbNL.LoadTenNhaCungCap(cmbNCC.Text);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvNGUYENLIEU_CellClick(null, null);
            dgvNGUYENLIEU.AutoResizeColumns();
            this.txtMaNL.ResetText();
            this.txtTenNL.ResetText();
            this.cmbNCC.ResetText();
            this.txtSoLuong.ResetText();
            this.txtDonVi.ResetText();

            this.txtMaNL.Enabled = false;
            this.txtTenNL.Enabled = false;
            this.cmbNCC.Enabled = false;
            this.txtSoLuong.Enabled = false;
            this.txtDonVi.Enabled = false;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                BL_NguyenLieu blNL = new BL_NguyenLieu();
                DataTable resultTable = blNL.TimKiemNL(this.txtTimKiem.Text);
                dgvNGUYENLIEU.DataSource = resultTable;
                if (resultTable != null)
                {
                    dgvNGUYENLIEU.AutoResizeColumns();
                    dgvNGUYENLIEU_CellClick(null, null);
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
    }
}