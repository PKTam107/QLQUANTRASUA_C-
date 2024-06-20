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
    public partial class frm_BangPhanCa : Form
    {
        DataTable BangPhanCa = null;
        bool Them;
        BLBangPhanCa dbBangPC = new BLBangPhanCa();
        public frm_BangPhanCa()
        {
            InitializeComponent();
        }

        private void LoadNhanVien()
        {
            DataTable BangNhanVien = dbBangPC.LayNhanVien();
            cmbMaNV.DataSource = BangNhanVien;
            cmbMaNV.DisplayMember = "MaNV";
            cmbMaNV.ValueMember = "TenNV";
        }

        private void LoadCaLamViec()
        {
            DataTable BangCaLamViec = dbBangPC.LayCaLamViec();
            cmbMaCa.DataSource = BangCaLamViec;
            cmbMaCa.DisplayMember = "MaCa";
            cmbMaCa.ValueMember = "MaCa";
        }

        void LoadData()
        {
            try
            {
                BangPhanCa = new DataTable();
                BangPhanCa.Clear();
                BangPhanCa = dbBangPC.LayBangPhanCa();
                dgvBangPhanCa.DataSource = BangPhanCa;
                dgvBangPhanCa.AutoResizeColumns();
                this.cmbMaCa.ResetText();
                this.cmbMaNV.ResetText();
                this.txtNgayLam.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.cmbMaCa.Enabled = false;
                this.cmbMaNV.Enabled = false;
                this.txtNgayLam.Enabled = false;
                this.txtNgayLam.Text = "";
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                dgvBangPhanCa_CellClick(null, null);
                cmbMaCa_SelectedIndexChanged(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table BangPhanCa. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_BangPhanCa_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadNhanVien();
            LoadCaLamViec();
            cmbMaCa.SelectedIndexChanged += new EventHandler(cmbMaCa_SelectedIndexChanged);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.cmbMaCa.ResetText();
            this.cmbMaNV.ResetText();
            this.txtNgayLam.ResetText();
            this.btnLuu.Enabled = true;
            this.grBox.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.cmbMaCa.Enabled = true;
            this.cmbMaNV.Enabled = true;
            this.txtNgayLam.Enabled = false;
            this.cmbMaNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLBangPhanCa blBangPC = new BLBangPhanCa();
                    blBangPC.ThemBangPhanCa(this.cmbMaCa.Text, this.cmbMaNV.Text, this.txtNgayLam.Text);
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
                    BLBangPhanCa blBangPC = new BLBangPhanCa();
                    blBangPC.CapNhatBangPhanCa(this.cmbMaCa.Text, this.cmbMaNV.Text, this.txtNgayLam.Text);
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
            this.grBox.Enabled = true;
            dgvBangPhanCa_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.cmbMaCa.Enabled = false;
            this.cmbMaNV.Enabled = true;
            this.cmbMaNV.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvBangPhanCa.CurrentCell.RowIndex;
                string MaCa = dgvBangPhanCa.Rows[r].Cells["MaCa"].Value.ToString();
                string MaNV = dgvBangPhanCa.Rows[r].Cells["MaNV"].Value.ToString();
                string Ngay = dgvBangPhanCa.Rows[r].Cells["Ngay"].Value.ToString();

                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (traloi == DialogResult.Yes)
                {
                    dbBangPC.XoaBangPhanCa(MaCa, MaNV, Ngay);
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

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvBangPhanCa.AutoResizeColumns();
            this.cmbMaCa.ResetText();
            this.cmbMaNV.ResetText();
            this.txtNgayLam.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.cmbMaCa.Enabled = false;
            this.cmbMaNV.Enabled = false;
            this.txtNgayLam.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvBangPhanCa_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvBangPhanCa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvBangPhanCa.CurrentCell.RowIndex;
            dgvBangPhanCa.Columns[0].HeaderText = "Mã Ca";
            dgvBangPhanCa.Columns[0].Width = 165;
            this.cmbMaCa.Text =
            dgvBangPhanCa.Rows[r].Cells[0].Value.ToString();
            dgvBangPhanCa.Columns[1].HeaderText = "Mã Nhân Viên";
            dgvBangPhanCa.Columns[1].Width = 165;
            this.cmbMaNV.Text =
            dgvBangPhanCa.Rows[r].Cells[1].Value.ToString();
            dgvBangPhanCa.Columns[2].HeaderText = "Ngày Làm";
            dgvBangPhanCa.Columns[2].Width = 165;
            this.txtNgayLam.Text =
            dgvBangPhanCa.Rows[r].Cells[2].Value.ToString();
        }

        private void cmbMaCa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaCa.SelectedValue != null)
            {
                if (cmbMaCa.SelectedValue != null)
                {
                    DataRowView selectedRow = (DataRowView)cmbMaCa.SelectedItem;
                    txtNgayLam.Text = selectedRow["Ngay"].ToString();
                }
            }
        }

        private void cmbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaNV.SelectedValue != null)
            {
                DataRowView selectedRow = (DataRowView)cmbMaNV.SelectedItem;
                lblTenNV.Text = selectedRow["TenNV"].ToString();
            }
        }

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            if (txtTK.Text != "")
            {
                try
                {
                    BLBangPhanCa tkBangPhanCa = new BLBangPhanCa();

                    string InforToSearch = txtTK.Text;
                    DataTable resultTable = null;

                    if (op1.Checked)
                    {
                        resultTable = tkBangPhanCa.TimKiemBangPhanCaBangMaNV(InforToSearch);
                    }
                    else if (op2.Checked)
                    {
                        resultTable = tkBangPhanCa.TimKiemBangPhanCaBangNgay(InforToSearch);
                    }
                    else if (op3.Checked)
                    {
                        resultTable = tkBangPhanCa.TimKiemBangPhanCaBangMaCa(InforToSearch);
                    }

                    if (resultTable != null)
                    {
                        dgvBangPhanCa.DataSource = resultTable;
                        dgvBangPhanCa.AutoResizeColumns();
                        dgvBangPhanCa_CellClick(null, null);
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
