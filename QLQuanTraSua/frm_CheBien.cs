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
    public partial class frm_CheBien : Form
    {
        DataTable CheBien = null;
        bool Them;
        BL_CheBien dbCheBien = new BL_CheBien();
        public frm_CheBien()
        {
            InitializeComponent();
        }

        private void gbHDB_Enter(object sender, EventArgs e)
        {

        }

        void LoadComboBoxSP()
        {
            DataTable SanPham = dbCheBien.LaySanPham();
            cmbSP.DataSource = SanPham;
            cmbSP.DisplayMember = "MaSP";
            cmbSP.ValueMember = "TenSP";
        }

        void LoadComboBoxNL()
        {
            DataTable NguyenLieu = dbCheBien.LayNguyenLieu();
            cmbNL.DataSource = NguyenLieu;
            cmbNL.DisplayMember = "MaNL";
            cmbNL.ValueMember = "TenNL";

        }

        void LoadData()
        {
            try
            {
                CheBien = new DataTable();
                CheBien.Clear();
                CheBien = dbCheBien.LayCheBien();
                dgvCheBien.DataSource = CheBien;
                dgvCheBien.AutoResizeColumns();
                this.cmbSP.ResetText();
                this.cmbNL.ResetText();
                this.txtDonVi.ResetText();
                this.txtLieuLuong.ResetText();
                this.txtLieuLuong.Enabled = false;
                this.cmbNL.Enabled = false;
                this.cmbSP.Enabled = false;
                this.txtDonVi.Enabled = false;
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                dgvCheBien_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table CheBien. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_CheBien_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxNL();
            LoadComboBoxSP();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.cmbSP.ResetText();
            this.cmbNL.ResetText();
            this.txtDonVi.ResetText();
            this.txtLieuLuong.ResetText();
            this.cmbSP.Enabled = true;
            this.cmbNL.Enabled = true;
            this.txtDonVi.Enabled = true;
            this.txtLieuLuong.Enabled = true;
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.cmbSP.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            BL_CheBien blCheBien = new BL_CheBien();
            if (Them)
            {
                try
                {
                    blCheBien.ThemCheBien(this.cmbSP.Text, this.cmbNL.Text, int.Parse(txtLieuLuong.Text), this.txtDonVi.Text);
                    LoadData();
                    MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    blCheBien.CapNhatCheBien(this.cmbSP.Text, this.cmbNL.Text, int.Parse(txtLieuLuong.Text), this.txtDonVi.Text);
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
            dgvCheBien_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.cmbSP.Enabled = false;
            this.cmbNL.Enabled = false;
            this.txtDonVi.Enabled = true;
            this.txtLieuLuong.Enabled = true;
            this.cmbSP.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvCheBien.CurrentCell.RowIndex;
                string strMaSP =
                dgvCheBien.Rows[r].Cells[0].Value.ToString();
                string strMaNL =
                dgvCheBien.Rows[r].Cells[1].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbCheBien.XoaCheBien(strMaSP, strMaNL);
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
                MessageBox.Show("Không thêm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCheBien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvCheBien.CurrentCell.RowIndex;
            dgvCheBien.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvCheBien.Columns[0].Width = 100;
            dgvCheBien.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.cmbSP.Text =
            dgvCheBien.Rows[r].Cells[0].Value.ToString();
            dgvCheBien.Columns[1].HeaderText = "Mã Nguyên Liệu";
            dgvCheBien.Columns[1].Width = 100;
            this.cmbNL.Text =
            dgvCheBien.Rows[r].Cells[1].Value.ToString();
            dgvCheBien.Columns[2].HeaderText = "Liều Lượng";
            dgvCheBien.Columns[2].Width = 100;
            this.txtLieuLuong.Text =
            dgvCheBien.Rows[r].Cells[2].Value.ToString();
            dgvCheBien.Columns[3].HeaderText = "Đơn Vị";
            dgvCheBien.Columns[3].Width = 100;
            this.txtDonVi.Text =
            dgvCheBien.Rows[r].Cells[3].Value.ToString();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvCheBien_CellClick(null, null);
            dgvCheBien.AutoResizeColumns();
            this.cmbSP.ResetText();
            this.cmbNL.ResetText();
            this.txtLieuLuong.ResetText();
            this.txtDonVi.ResetText();

            this.cmbSP.Enabled = false;
            this.cmbNL.Enabled = false;
            this.txtLieuLuong.Enabled = false;
            this.txtDonVi.Enabled = false;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
        }

        private void cmbSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSP.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cmbSP.SelectedItem;
                lblTenSP.Text = selectedRow["TenSP"].ToString();
            }
        }

        private void cmbNL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNL.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cmbNL.SelectedItem;
                lblTenNL.Text = selectedRow["TenNL"].ToString();
            }
        }

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            if (txtTK.Text != "")
            {
                try
                {
                    BL_CheBien tkCheBien = new BL_CheBien();

                    string InforToSearch = txtTK.Text;
                    DataTable resultTable = null;

                    if (op1.Checked)
                    {
                        resultTable = tkCheBien.TimKiemCheBienBangMaSP(InforToSearch);
                    }
                    else if (op2.Checked)
                    {
                        resultTable = tkCheBien.TimKiemCheBienBangMaNL(InforToSearch);
                    }

                    if (resultTable != null)
                    {
                        dgvCheBien.DataSource = resultTable;
                        dgvCheBien.AutoResizeColumns();
                        dgvCheBien_CellClick(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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