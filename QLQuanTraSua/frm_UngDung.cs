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
    public partial class frm_UngDung : Form
    {

        DataTable tb_UngDung = null;
        bool Them;
        BLUngDung dbUD = new BLUngDung();
        public frm_UngDung()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                tb_UngDung = new DataTable();
                tb_UngDung.Clear();
                tb_UngDung = dbUD.LayUngDung();
                dgvUngDung.DataSource = tb_UngDung;
                dgvUngDung.AutoResizeColumns();
                this.txtMaUngDung.ResetText();
                this.txtTenUngDung.ResetText();
                this.txtChietKhau.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.txtMaUngDung.Enabled = false;
                this.txtTenUngDung.Enabled = false;
                this.txtChietKhau.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                //
                dgvUngDung_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table UngDung. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvUngDung.CurrentCell.RowIndex;
                string strUD =
                dgvUngDung.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbUD.XoaUngDung(strUD);
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaUngDung.ResetText();
            this.txtTenUngDung.ResetText();
            this.txtChietKhau.ResetText();
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaUngDung.Enabled = true;
            this.txtTenUngDung.Enabled = true;
            this.txtChietKhau.Enabled = true;
            this.txtMaUngDung.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            dgvUngDung_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtTenUngDung.Enabled = true;
            this.txtChietKhau.Enabled = true;
            this.txtTenUngDung.Focus();
        }

        private void dgvUngDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvUngDung.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            int r = dgvUngDung.CurrentCell.RowIndex;
            dgvUngDung.Columns[0].HeaderText = "Mã Ứng Dụng";
            dgvUngDung.Columns[0].Width = 150;
            dgvUngDung.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaUngDung.Text =
            dgvUngDung.Rows[r].Cells[0].Value.ToString();
            dgvUngDung.Columns[1].HeaderText = "Tên Ứng Dụng";
            dgvUngDung.Columns[1].Width = 150;
            this.txtTenUngDung.Text =
            dgvUngDung.Rows[r].Cells[1].Value.ToString();
            dgvUngDung.Columns[2].HeaderText = "Chiết Khấu";
            dgvUngDung.Columns[2].Width = 150;
            this.txtChietKhau.Text =
            dgvUngDung.Rows[r].Cells[2].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLUngDung blUD = new BLUngDung();
                    blUD.ThemUngDung(txtMaUngDung.Text, txtTenUngDung.Text, txtChietKhau.Text);
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
                BLUngDung blUD = new BLUngDung();
                blUD.CapNhatUngDung(txtMaUngDung.Text, txtTenUngDung.Text, txtChietKhau.Text);
                LoadData();
                MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvUngDung.AutoResizeColumns();
            txtMaUngDung.ResetText();
            txtTenUngDung.ResetText();
            this.txtChietKhau.ResetText();
            this.panel3.Enabled = false;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvUngDung_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frm_UngDung_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
