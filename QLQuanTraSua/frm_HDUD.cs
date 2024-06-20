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
    public partial class frm_HDUD : Form
    {
        DataTable tb_HDUD = null;
        bool Them;
        BLHD_UD dbHDUD = new BLHD_UD();
        public frm_HDUD()
        {
            InitializeComponent();
        }

        void LoadComboBoxKH()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbHDUD.LayKhachHang();
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
                dt = dbHDUD.LayNhanVien();
                cmbNV.DataSource = dt;
                cmbNV.DisplayMember = "MaNV";
                cmbNV.ValueMember = "MaNV";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadComboBoxUngDung()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbHDUD.LayUngDung();
                cmbUD.DataSource = dt;
                cmbUD.DisplayMember = "MaUD";
                cmbUD.ValueMember = "MaUD";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table UngDung. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadData()
        {
            try
            {
                tb_HDUD = new DataTable();
                tb_HDUD.Clear();
                tb_HDUD = dbHDUD.LayHoaDonUngDung();
                dgvHDUD.DataSource = tb_HDUD;
                dgvHDUD.AutoResizeColumns();
                this.txtMaHDUD.ResetText();
                this.dtpNgayBan.ResetText();
                this.cmbNV.ResetText();
                this.cmbKH.ResetText();
                this.cmbUD.ResetText();
                this.txtThanhTien.ResetText();
                this.txtMaHDUD.Enabled = false;
                this.dtpNgayBan.Enabled = false;
                this.cmbKH.Enabled = false;
                this.cmbNV.Enabled = false;
                this.cmbUD.Enabled = false;
                this.txtThanhTien.Enabled = false;
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;

                LoadComboBoxKH();
                cmbKH.SelectedIndexChanged += cmbKH_SelectedIndexChanged;
                LoadComboBoxUngDung();
                cmbUD.SelectedIndexChanged += cmbUD_SelectedIndexChanged;
                LoadComboBoxNV();
                cmbNV.SelectedIndexChanged += cmbNV_SelectedIndexChanged;
                dgvHDUD_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table HoaDonUngDung. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_HDUD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaHDUD.ResetText();
            this.dtpNgayBan.ResetText();
            this.cmbNV.ResetText();
            this.cmbKH.ResetText();
            this.cmbUD.ResetText();
            this.txtMaHDUD.Enabled = true;
            this.dtpNgayBan.Enabled = true;
            this.cmbKH.Enabled = true;
            this.cmbNV.Enabled = true;
            this.cmbUD.Enabled = true;
            this.txtThanhTien.Enabled = false;
            if (cmbNV.Items.Count > 0)
            {
                cmbNV.SelectedIndex = 0;
                lblTenNV.Text = cmbNV.Text;
                cmbNV_SelectedIndexChanged(null, null);
            }
            if (cmbKH.Items.Count > 0)
            {
                cmbKH.SelectedIndex = 0;
                lblTenKH.Text = cmbKH.Text;
                cmbKH_SelectedIndexChanged(null, null);
            }
            if (cmbUD.Items.Count > 0)
            {
                cmbUD.SelectedIndex = 0;
                lblTenUD.Text = cmbKH.Text;
                cmbUD_SelectedIndexChanged(null, null);
            }
            this.txtThanhTien.Text = "0";
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.dtpNgayBan.Focus();
            this.cmbNV.Focus();
            this.cmbKH.Focus();
            this.cmbUD.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLHD_UD blHDUD = new BLHD_UD();
                    blHDUD.ThemHoaDonUngDung(this.txtMaHDUD.Text, this.dtpNgayBan.Text, this.cmbKH.Text, this.cmbUD.Text, this.cmbNV.Text, int.Parse(this.txtThanhTien.Text));
                    LoadData();
                    MessageBox.Show("Đã thêm xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frm_CTHDUD fr = new frm_CTHDUD();
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
                    BLHD_UD blHDUD = new BLHD_UD();
                    blHDUD.CapNhatHoaDonUngDung(this.txtMaHDUD.Text, this.dtpNgayBan.Text, this.cmbKH.Text, this.cmbUD.Text, this.cmbNV.Text, int.Parse(this.txtThanhTien.Text));
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                    frm_CTHDUD fr = new frm_CTHDUD();
                    this.Close();
                    fr.Show();
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
            dgvHDUD_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;

            this.txtMaHDUD.Enabled = false;
            this.dtpNgayBan.Enabled = true;
            this.cmbKH.Enabled = true;
            this.cmbNV.Enabled = true;
            this.txtThanhTien.Enabled = false;
            string maNV = cmbNV.SelectedValue.ToString();
            DataRow[] r = dbHDUD.LayNhanVien().Select("MaNV='" + maNV + "'");
            if (r.Length > 0)
            {
                lblTenNV.Text = r[0]["TenNV"].ToString();
            }
            string maKH = cmbNV.SelectedValue.ToString();
            DataRow[] r1 = dbHDUD.LayKhachHang().Select("MaKH='" + maKH + "'");
            if (r1.Length > 0)
            {
                lblTenKH.Text = r1[0]["TenKH"].ToString();
            }
            string maUD = cmbNV.SelectedValue.ToString();
            DataRow[] r2 = dbHDUD.LayUngDung().Select("MaUD='" + maUD + "'");
            if (r2.Length > 0)
            {
                lblTenUD.Text = r2[0]["TenUD"].ToString();
            }
            this.txtThanhTien.Enabled = false;
            this.dtpNgayBan.Focus();
            this.cmbNV.Focus();
            this.cmbKH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvHDUD.CurrentCell.RowIndex;
                string strHDUD =
                dgvHDUD.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbHDUD.XoaHoaDonUngDung(strHDUD);
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

        private void dgvHDUD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvHDUD.CurrentCell.RowIndex;
            dgvHDUD.Columns[0].HeaderText = "Mã Hóa Đơn Ứng Dụng";
            dgvHDUD.Columns[0].Width = 100;
            dgvHDUD.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaHDUD.Text =
            dgvHDUD.Rows[r].Cells[0].Value.ToString();
            dgvHDUD.Columns[1].HeaderText = "Ngày Bán";
            dgvHDUD.Columns[1].Width = 100;
            this.dtpNgayBan.Text =
            dgvHDUD.Rows[r].Cells[1].Value.ToString();
            dgvHDUD.Columns[2].HeaderText = "SDT Khách Hàng";
            dgvHDUD.Columns[2].Width = 100;
            this.cmbKH.Text =
            dgvHDUD.Rows[r].Cells[2].Value.ToString();
            dgvHDUD.Columns[3].HeaderText = "Mã Ứng Dụng";
            dgvHDUD.Columns[3].Width = 100;
            this.cmbUD.Text =
            dgvHDUD.Rows[r].Cells[3].Value.ToString();
            dgvHDUD.Columns[4].HeaderText = "Mã Nhân Viên";
            dgvHDUD.Columns[4].Width = 100;
            this.cmbNV.Text =
            dgvHDUD.Rows[r].Cells[4].Value.ToString();
            dgvHDUD.Columns[5].HeaderText = "Thành Tiền";
            dgvHDUD.Columns[5].Width = 100;
            this.txtThanhTien.Text =
            dgvHDUD.Rows[r].Cells[5].Value.ToString();
        }

        private void cmbKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenKH.Text = dbHDUD.LoadTenKH(cmbKH.Text);
        }

        private void cmbNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenNV.Text = dbHDUD.LoadTenNV(cmbNV.Text);
        }

        private void cmbUD_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenUD.Text = dbHDUD.LoadTenUD(cmbUD.Text);
        }

        private void dgvHDUD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvHDUD.CurrentCell.RowIndex;
            frm_CTHDUD fr = new frm_CTHDUD();
            this.Close();
            fr.Show();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvHDUD_CellClick(null, null);
            dgvHDUD.AutoResizeColumns();
            this.txtMaHDUD.ResetText();
            this.dtpNgayBan.ResetText();
            this.cmbNV.ResetText();
            this.cmbKH.ResetText();
            this.txtThanhTien.ResetText();

            this.txtMaHDUD.Enabled = false;
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

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            if (txtTK.Text != "")
            {
                try
                {
                    BLHD_UD tkHDUD = new BLHD_UD();

                    string InforToSearch = txtTK.Text;
                    DataTable resultTable = null;

                    if (op1.Checked)
                    {
                        resultTable = tkHDUD.TimKiemHoaDonBangMaHD(InforToSearch);
                    }
                    else if (op2.Checked)
                    {
                        resultTable = tkHDUD.TimKiemHoaDonBangNgayBan(InforToSearch);
                    }
                    else if (op3.Checked)
                    {
                        resultTable = tkHDUD.TimKiemHoaDonBangSDT(InforToSearch);
                    }
                    else if (op4.Checked)
                    {
                        resultTable = tkHDUD.TimKiemHoaDonBangMaNV(InforToSearch);
                    }

                    if (resultTable != null)
                    {
                        dgvHDUD.DataSource = resultTable;
                        dgvHDUD.AutoResizeColumns();
                        dgvHDUD_CellClick(null, null);
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
