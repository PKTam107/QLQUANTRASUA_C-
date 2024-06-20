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
    public partial class frm_SanPham : Form
    {
        DataTable tb_SanPham = null;
        bool Them;
        BL_SanPham dbSP = new BL_SanPham();
        public frm_SanPham()
        {
            InitializeComponent();
        }

        void LoadComboBoxLoaiSP()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbSP.LayLoaiSanPham();
                cmbMaLoaiSP.DataSource = dt;
                cmbMaLoaiSP.DisplayMember = "MaLoaiSP";
                cmbMaLoaiSP.ValueMember = "MaLoaiSP";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table LoaiSanPham. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadData()
        {
            try
            {
                tb_SanPham = new DataTable();
                tb_SanPham.Clear();
                tb_SanPham = dbSP.LaySanPham();
                dgvSANPHAM.DataSource = tb_SanPham;
                dgvSANPHAM.AutoResizeColumns();
                this.txtMaSP.ResetText();
                this.txtTenSP.ResetText();
                this.txtDonGia.ResetText();
                this.txtTinhTrang.ResetText();
                this.cmbMaLoaiSP.ResetText();
                this.txtMaSP.Enabled = false;
                this.txtTenSP.Enabled = false;
                this.txtTinhTrang.Enabled = false;
                this.txtDonGia.Enabled = false;
                this.cmbMaLoaiSP.Enabled = false;
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                LoadComboBoxLoaiSP();
                cmbMaLoaiSP.SelectedIndexChanged += cmbMaLoaiSP_SelectedIndexChanged;
                dgvSANPHAM_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table SanPham. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_SanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaSP.ResetText();
            this.txtTenSP.ResetText();
            this.txtDonGia.ResetText();
            this.txtTinhTrang.ResetText();
            this.cmbMaLoaiSP.ResetText();
            this.lblTenLoaiSP.ResetText();
            this.txtMaSP.Enabled = true;
            this.txtTenSP.Enabled = true;
            this.txtDonGia.Enabled = true;
            this.txtTinhTrang.Enabled = true;
            this.cmbMaLoaiSP.Enabled = true;
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaSP.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BL_SanPham blSP = new BL_SanPham();
                    blSP.ThemSanPham(this.txtMaSP.Text, this.txtTenSP.Text, int.Parse(this.txtDonGia.Text), this.txtTinhTrang.Text, this.cmbMaLoaiSP.Text);
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
                    BL_SanPham blSP = new BL_SanPham();
                    blSP.CapNhatSanPham(this.txtMaSP.Text, this.txtTenSP.Text, int.Parse(this.txtDonGia.Text), this.txtTinhTrang.Text, this.cmbMaLoaiSP.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Không thêm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            dgvSANPHAM_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.gbHDB.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;

            this.txtMaSP.Enabled = false;
            this.txtTenSP.Enabled = true;
            this.txtDonGia.Enabled = true;
            this.txtTinhTrang.Enabled = true;
            this.cmbMaLoaiSP.Enabled = true;
            this.txtTenSP.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvSANPHAM.CurrentCell.RowIndex;
                string strSP =
                dgvSANPHAM.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbSP.XoaSanPham(strSP);
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

        private void dgvSANPHAM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvSANPHAM.CurrentCell.RowIndex;
            dgvSANPHAM.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvSANPHAM.Columns[0].Width = 100;
            dgvSANPHAM.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaSP.Text =
            dgvSANPHAM.Rows[r].Cells[0].Value.ToString();
            dgvSANPHAM.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvSANPHAM.Columns[1].Width = 100;
            this.txtTenSP.Text =
            dgvSANPHAM.Rows[r].Cells[1].Value.ToString();
            dgvSANPHAM.Columns[2].HeaderText = "Đơn Giá";
            dgvSANPHAM.Columns[2].Width = 100;
            this.txtDonGia.Text =
            dgvSANPHAM.Rows[r].Cells[2].Value.ToString();
            dgvSANPHAM.Columns[3].HeaderText = "Tình Trạng";
            dgvSANPHAM.Columns[3].Width = 100;
            this.txtTinhTrang.Text =
            dgvSANPHAM.Rows[r].Cells[3].Value.ToString();
            dgvSANPHAM.Columns[4].HeaderText = "Mã Loại Sản Phẩm";
            dgvSANPHAM.Columns[4].Width = 100;
            this.cmbMaLoaiSP.Text =
            dgvSANPHAM.Rows[r].Cells[4].Value.ToString();

            string maLoaiSP = this.cmbMaLoaiSP.Text;
            DataRow[] r1 = dbSP.LayLoaiSanPham().Select("MaLoaiSP='" + maLoaiSP + "'");
            if (r1.Length > 0)
            {
                this.lblTenLoaiSP.Text = r1[0]["TenLoaiSP"].ToString();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvSANPHAM_CellClick(null, null);
            dgvSANPHAM.AutoResizeColumns();
            this.txtMaSP.ResetText();
            this.txtTenSP.ResetText();
            this.txtDonGia.ResetText();
            this.txtTinhTrang.ResetText();
            this.cmbMaLoaiSP.ResetText();

            this.txtMaSP.Enabled = false;
            this.txtTenSP.Enabled = false;
            this.txtDonGia.Enabled = false;
            this.txtTinhTrang.Enabled = false;
            this.cmbMaLoaiSP.Enabled = false;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
        }

        private void cmbMaLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenLoaiSP.Text = dbSP.LoadTenLoaiSanPham(cmbMaLoaiSP.Text);
        }

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            if (txtTK.Text != "")
            {
                try
                {
                    BL_SanPham tkSanPham = new BL_SanPham();

                    string InforToSearch = txtTK.Text;
                    DataTable resultTable = null;

                    if (op1.Checked)
                    {
                        resultTable = tkSanPham.TimKiemSanPhamBangMaSP(InforToSearch);
                    }
                    else if (op2.Checked)
                    {
                        resultTable = tkSanPham.TimKiemSanPhamBangTenSP(InforToSearch);
                    }
                    else if (op3.Checked)
                    {
                        resultTable = tkSanPham.TimKiemSanPhamBangMaLoaiSP(InforToSearch);
                    }

                    if (resultTable != null)
                    {
                        dgvSANPHAM.DataSource = resultTable;
                        dgvSANPHAM.AutoResizeColumns();
                        dgvSANPHAM_CellClick(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}