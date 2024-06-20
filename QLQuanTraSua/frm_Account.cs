using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLQuanTraSua.BS_Layer;

namespace QLQuanTraSua
{
    public partial class frm_Account : Form
    {
        DataTable Account = null;
        BL_KetNoi businessLayer = new BL_KetNoi();
        public frm_Account()
        {
            InitializeComponent();
        }
        private void LoadAccount()
        {
            DataTable Account = businessLayer.LayAccount();
            cmbMaNV.DataSource = Account;
            cmbMaNV.DisplayMember = "MaNV";
        }

        void LoadComboBoxNV()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = businessLayer.LayNhanVien();
                cmbMaNV.DataSource = dt;
                cmbMaNV.DisplayMember = "MaNV";
                cmbMaNV.ValueMember = "MaNV";
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
                Account = new DataTable();
                Account.Clear();
                Account = businessLayer.LayAccount();
                dgvAccount.DataSource = Account;
                dgvAccount.AutoResizeColumns();
                this.txtUser.ResetText();
                this.txtPassword.ResetText();
                this.txtRole.ResetText();
                this.cmbMaNV.ResetText();
                this.btnXoa.Enabled = true;
                this.btnCapNhat.Enabled = true;
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.txtUser.Enabled = false;
                this.txtPassword.Enabled = false;
                this.txtRole.Enabled = false;
                this.cmbMaNV.Enabled = false;
                LoadComboBoxNV();
                cmbMaNV.SelectedIndexChanged += cmbMaNV_SelectedIndexChanged;
                dgvAccount_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Account. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvAccount.CurrentCell.RowIndex;
                string maNV = dgvAccount.Rows[r].Cells["MaNV"].Value.ToString();

                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (traloi == DialogResult.Yes)
                {
                    string err = string.Empty;
                    bool result = businessLayer.DeleteEmployee(maNV);

                    if (result)
                    {
                        MessageBox.Show("Không xóa được. Lỗi: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        
                        LoadData();
                        MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không xóa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvAccount.CurrentCell.RowIndex;
            dgvAccount.Columns[0].HeaderText = "Tên Tài Khoản";
            dgvAccount.Columns[0].Width = 100;
            dgvAccount.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtUser.Text =
            dgvAccount.Rows[r].Cells[0].Value.ToString();
            dgvAccount.Columns[1].HeaderText = "Mật Khẩu";
            dgvAccount.Columns[1].Width = 100;
            this.txtPassword.Text =
            dgvAccount.Rows[r].Cells[1].Value.ToString();
            dgvAccount.Columns[2].HeaderText = "Mã Nhân Viên";
            dgvAccount.Columns[2].Width = 100;
            this.cmbMaNV.Text =
            dgvAccount.Rows[r].Cells[2].Value.ToString();
            dgvAccount.Columns[3].HeaderText = "Roles";
            dgvAccount.Columns[3].Width = 100;
            this.txtRole.Text =
            dgvAccount.Rows[r].Cells[3].Value.ToString();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvAccount_CellClick(null, null);
            dgvAccount.AutoResizeColumns();
            this.btnXoa.Enabled = true;
            this.btnCapNhat.Enabled = true;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.txtUser.Enabled = false;
            this.txtPassword.Enabled = false;
            this.txtRole.Enabled = false;
            this.cmbMaNV.Enabled = false;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frm_DeleteEmployee_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadAccount();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            dgvAccount_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnCapNhat.Enabled = false;
            this.btnXoa.Enabled = false;
            this.cmbMaNV.Enabled = false;
            this.txtUser.Enabled = false;
            this.txtPassword.Enabled = true;
            this.txtRole.Enabled = false;
            this.txtPassword.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                BL_KetNoi businessLayer = new BL_KetNoi();
                businessLayer.UpdateEmployee(this.txtUser.Text, this.txtPassword.Text, this.cmbMaNV.Text, this.txtRole.Text);
                LoadData();
                MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTenNV.Text = businessLayer.LoadTenNV(cmbMaNV.Text);
        }
    }
}
