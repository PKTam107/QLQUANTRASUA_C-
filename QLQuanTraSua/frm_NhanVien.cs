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
    public partial class frm_NhanVien : Form
    {
        DataTable NhanVien = null;
        bool Them;
        BLNhanVien dbNV = new BLNhanVien();
        public frm_NhanVien()
        {
            InitializeComponent();
        }
        private void LoadLoaiNhanVien()
        {
            DataTable LoaiNhaVien = dbNV.LayLoaiNhanVien();
            cmbMaLoaiNV.DataSource = LoaiNhaVien;
            cmbMaLoaiNV.DisplayMember = "MaLoaiNV";
            cmbMaLoaiNV.ValueMember = "TenLoaiNV";
        }

        private void LoadViTri()
        {
            DataTable ViTri = dbNV.LayViTri();
            cmbMaViTri.DataSource = ViTri;
            cmbMaViTri.DisplayMember = "MaViTri";
            cmbMaViTri.ValueMember = "TenViTri";
        }

        void LoadData()
        {
            try
            {
                NhanVien = new DataTable();
                NhanVien.Clear();
                NhanVien = dbNV.LayNhanVien();
                dgvNhanVien.DataSource = NhanVien;
                dgvNhanVien.AutoResizeColumns();
                this.txtMaNV.ResetText();
                this.txtTenNV.ResetText();
                this.txtNgaySinh.ResetText();
                this.cmbGTinh.ResetText();
                this.txtDChi.ResetText();
                this.txtSdt.ResetText();
                this.cmbMaLoaiNV.ResetText();
                this.cmbMaViTri.ResetText();
                this.txtNgayTD.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.panel2.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                dgvNhanVien_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_NhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadLoaiNhanVien();
            LoadViTri();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaNV.ResetText();
            this.txtTenNV.ResetText();
            this.txtNgaySinh.ResetText();
            this.cmbGTinh.ResetText();
            this.txtDChi.ResetText();
            this.txtSdt.ResetText();
            this.cmbMaLoaiNV.ResetText();
            this.cmbMaViTri.ResetText();
            this.txtNgayTD.ResetText();
            this.lblTenLoaiNV.ResetText();
            this.lblTenViTri.ResetText();
            this.btnLuu.Enabled = true;
            this.panel2.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaNV.Enabled = true;
            this.txtMaNV.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvNhanVien.CurrentCell.RowIndex;
                string strNhanVien =
                dgvNhanVien.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbNV.XoaNhanVien(strNhanVien);
                    LoadData();
                    MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không xóa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            this.panel2.Enabled = true;
            dgvNhanVien_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.panel2.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaNV.Enabled = false;
            this.txtTenNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (Them)
            {
                try
                {
                    BLNhanVien blNV = new BLNhanVien();
                    blNV.ThemNhanVien(this.txtMaNV.Text, this.txtTenNV.Text, this.txtNgaySinh.Text, this.cmbGTinh.Text, this.txtDChi.Text, this.txtSdt.Text, this.cmbMaLoaiNV.Text, this.cmbMaViTri.Text, this.txtNgayTD.Text);
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
                    BLNhanVien blNV = new BLNhanVien();
                    blNV.CapNhatNhanVien(this.txtMaNV.Text, this.txtTenNV.Text, this.txtNgaySinh.Text, this.cmbGTinh.Text, this.txtDChi.Text, this.txtSdt.Text, this.cmbMaLoaiNV.Text, this.cmbMaViTri.Text, this.txtNgayTD.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int r = dgvNhanVien.CurrentCell.RowIndex;
            dgvNhanVien.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaNV.Text =
            dgvNhanVien.Rows[r].Cells[0].Value.ToString();
            dgvNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dgvNhanVien.Columns[1].Width = 100;
            this.txtTenNV.Text =
            dgvNhanVien.Rows[r].Cells[1].Value.ToString();
            dgvNhanVien.Columns[2].HeaderText = "Ngày Sinh";
            dgvNhanVien.Columns[2].Width = 100;
            this.txtNgaySinh.Text =
            dgvNhanVien.Rows[r].Cells[2].Value.ToString();
            dgvNhanVien.Columns[3].HeaderText = "Giới Tính";
            dgvNhanVien.Columns[3].Width = 100;
            this.cmbGTinh.Text =
            dgvNhanVien.Rows[r].Cells[3].Value.ToString();
            dgvNhanVien.Columns[4].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns[4].Width = 100;
            this.txtDChi.Text =
            dgvNhanVien.Rows[r].Cells[4].Value.ToString();
            dgvNhanVien.Columns[5].HeaderText = "Số Điện Thoại";
            dgvNhanVien.Columns[5].Width = 100;
            this.txtSdt.Text =
            dgvNhanVien.Rows[r].Cells[5].Value.ToString();
            dgvNhanVien.Columns[6].HeaderText = "Mã Loại NV";
            dgvNhanVien.Columns[6].Width = 100;
            this.cmbMaLoaiNV.Text =
            dgvNhanVien.Rows[r].Cells[6].Value.ToString();
            dgvNhanVien.Columns[7].HeaderText = "Mã Vị Trí";
            dgvNhanVien.Columns[7].Width = 100;
            this.cmbMaViTri.Text =
            dgvNhanVien.Rows[r].Cells[7].Value.ToString();
            dgvNhanVien.Columns[8].HeaderText = "Ngày Tuyển Dụng";
            dgvNhanVien.Columns[8].Width = 100;
            this.txtNgayTD.Text =
            dgvNhanVien.Rows[r].Cells[8].Value.ToString();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvNhanVien.AutoResizeColumns();
            this.txtMaNV.ResetText();
            this.txtTenNV.ResetText();
            this.txtNgaySinh.ResetText();
            this.cmbGTinh.ResetText();
            this.txtDChi.ResetText();
            this.txtSdt.ResetText();
            this.cmbMaLoaiNV.ResetText();
            this.cmbMaViTri.ResetText();
            this.txtNgaySinh.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel2.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvNhanVien_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbMaLoaiNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaLoaiNV.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cmbMaLoaiNV.SelectedItem;
                lblTenLoaiNV.Text = selectedRow["TenLoaiNV"].ToString();
            }
        }

        private void cmbMaViTri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaViTri.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cmbMaViTri.SelectedItem;
                lblTenViTri.Text = selectedRow["TenViTri"].ToString();
            }
        }

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            if (txtTK.Text != "")
            {
                try
                {
                    BLNhanVien tkNhanVien = new BLNhanVien();

                    string InforToSearch = txtTK.Text;
                    DataTable resultTable = null;

                    if (op1.Checked)
                    {
                        resultTable = tkNhanVien.TimKiemNVBangTenNV(InforToSearch);
                    }
                    else if (op2.Checked)
                    {
                        resultTable = tkNhanVien.TimKiemNVBangDiaChi(InforToSearch);
                    }
                    else if (op3.Checked)
                    {
                        resultTable = tkNhanVien.TimKiemNVBangSDT(InforToSearch);
                    }
                    else if (op4.Checked)
                    {
                        resultTable = tkNhanVien.TimKiemNVBangNgayTuyenDung(InforToSearch);
                    }

                    if (resultTable != null)
                    {
                        dgvNhanVien.DataSource = resultTable;
                        dgvNhanVien.AutoResizeColumns();
                        dgvNhanVien_CellClick(null, null);
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
