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
    public partial class frm_LoaiNhanVien : Form
    {
        DataTable LoaiNhanVien = null;
        bool Them;
        string err;
        BLLoaiNV dbLoaiNV = new BLLoaiNV();
        public frm_LoaiNhanVien()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                LoaiNhanVien = new DataTable();
                LoaiNhanVien.Clear();
                LoaiNhanVien = dbLoaiNV.LayLoaiNV();
                dgvLoaiNhanVien.DataSource = LoaiNhanVien;
                dgvLoaiNhanVien.AutoResizeColumns();
                this.txtMaLoaiNV.ResetText();
                this.txtTenLoaiNV.ResetText();
                this.txtLuongCB.ResetText();
                this.btnLuu.Enabled = false;
                this.txtMaLoaiNV.Enabled = false;
                this.txtTenLoaiNV.Enabled = false;
                this.txtLuongCB.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                dgvLoaiNhanVien_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table LoaiNhanVien. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_LoaiNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaLoaiNV.ResetText();
            this.txtTenLoaiNV.ResetText();
            this.txtLuongCB.ResetText();
            this.btnLuu.Enabled = true;
            this.panel3.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaLoaiNV.Enabled = true;
            this.txtTenLoaiNV.Enabled = true;
            this.txtLuongCB.Enabled = true;
            this.txtMaLoaiNV.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvLoaiNhanVien.CurrentCell.RowIndex;
                string strLoaiNV =
                dgvLoaiNhanVien.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbLoaiNV.XoaLoaiNV(strLoaiNV);
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            //this.grBox.Enabled = true;
            this.panel3.Enabled = true;
            dgvLoaiNhanVien_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaLoaiNV.Enabled = false;
            this.txtTenLoaiNV.Enabled = true;
            this.txtLuongCB.Enabled = true;
            this.txtTenLoaiNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLLoaiNV blLoaiNV = new BLLoaiNV();
                    blLoaiNV.ThemLoaiNV(this.txtMaLoaiNV.Text, this.txtTenLoaiNV.Text, this.txtLuongCB.Text);
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
                    BLLoaiNV blLoai = new BLLoaiNV();
                    blLoai.CapNhatLoaiNV(this.txtMaLoaiNV.Text, this.txtTenLoaiNV.Text, this.txtLuongCB.Text, ref err);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvLoaiNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvLoaiNhanVien.CurrentCell.RowIndex;
            dgvLoaiNhanVien.Columns[0].HeaderText = "Mã loại NV";
            dgvLoaiNhanVien.Columns[0].Width = 165;
            this.txtMaLoaiNV.Text =
            dgvLoaiNhanVien.Rows[r].Cells[0].Value.ToString();
            dgvLoaiNhanVien.Columns[1].HeaderText = "Tên loại NV";
            dgvLoaiNhanVien.Columns[1].Width = 165;
            this.txtTenLoaiNV.Text =
            dgvLoaiNhanVien.Rows[r].Cells[1].Value.ToString();
            dgvLoaiNhanVien.Columns[2].HeaderText = "Lương Cơ Bản";
            dgvLoaiNhanVien.Columns[2].Width = 165;
            this.txtLuongCB.Text =
            dgvLoaiNhanVien.Rows[r].Cells[2].Value.ToString();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {

            dgvLoaiNhanVien.AutoResizeColumns();
            this.txtMaLoaiNV.ResetText();
            this.txtTenLoaiNV.ResetText();
            this.txtLuongCB.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.txtMaLoaiNV.Enabled = false;
            this.txtTenLoaiNV.Enabled = false;
            this.txtLuongCB.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvLoaiNhanVien_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
