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
    public partial class frm_LoaiSP : Form
    {
        DataTable tb_LoaiSP = null;
        bool Them;
        BL_LoaiSP dbLoaiSP = new BL_LoaiSP();
        public frm_LoaiSP()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                tb_LoaiSP = new DataTable();
                tb_LoaiSP.Clear();
                tb_LoaiSP = dbLoaiSP.LayLoaiSanPham();
                dgvLOAISP.DataSource = tb_LoaiSP;
                dgvLOAISP.AutoResizeColumns();
                this.txtMaLoaiSP.ResetText();
                this.txtTenLoaiSP.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.grBox.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                //
                dgvLOAISP_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table LoaiSanPham. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_LoaiSP_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaLoaiSP.ResetText();
            this.txtTenLoaiSP.ResetText();
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.grBox.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaLoaiSP.Enabled = true;
            this.txtMaLoaiSP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            dgvLOAISP_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.grBox.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaLoaiSP.Enabled = false;
            this.txtTenLoaiSP.Focus();
        }

        private void dgvLOAISP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvLOAISP.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            int r = dgvLOAISP.CurrentCell.RowIndex;
            dgvLOAISP.Columns[0].HeaderText = "Mã Loại Sản Phẩm";
            dgvLOAISP.Columns[0].Width = 150;
            dgvLOAISP.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtMaLoaiSP.Text =
            dgvLOAISP.Rows[r].Cells[0].Value.ToString();
            dgvLOAISP.Columns[1].HeaderText = "Tên Loại Sản Phẩm";
            dgvLOAISP.Columns[1].Width = 150;
            this.txtTenLoaiSP.Text =
            dgvLOAISP.Rows[r].Cells[1].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BL_LoaiSP blLoaiSP = new BL_LoaiSP();
                    blLoaiSP.ThemLoaiSanPham(this.txtMaLoaiSP.Text, this.txtTenLoaiSP.Text);
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
                    BL_LoaiSP blLoaiSP = new BL_LoaiSP();
                    blLoaiSP.CapNhatLoaiSanPham(this.txtMaLoaiSP.Text, this.txtTenLoaiSP.Text);
                    LoadData(); ;
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Không thêm được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvLOAISP.CurrentCell.RowIndex;
                string strLoaiSP =
                dgvLOAISP.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbLoaiSP.XoaLoaiSanPham(strLoaiSP);
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

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvLOAISP.AutoResizeColumns();
            this.txtTenLoaiSP.ResetText();
            this.txtMaLoaiSP.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.grBox.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvLOAISP_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
