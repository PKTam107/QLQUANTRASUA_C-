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

    public partial class frm_ViTriCongViec : Form
    {
        DataTable ViTriCongViec = null;
        bool Them;
        string err;
        BLViTriCV dbViTriCV = new BLViTriCV();
        public frm_ViTriCongViec()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                ViTriCongViec = new DataTable();
                ViTriCongViec.Clear();
                ViTriCongViec = dbViTriCV.LayViTriCV();
                dgvViTriCV.DataSource = ViTriCongViec;
                dgvViTriCV.AutoResizeColumns();
                this.txtMaViTri.ResetText();
                this.txtTenViTri.ResetText();
                this.txtPCLuong.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.txtMaViTri.Enabled = false;
                this.txtTenViTri.Enabled = false;
                this.txtPCLuong.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                dgvViTriCV_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table ViTriCongViec. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fr_ViTriCongViec_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaViTri.ResetText();
            this.txtTenViTri.ResetText();
            this.txtPCLuong.ResetText();
            this.btnLuu.Enabled = true;
            this.panel3.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaViTri.Enabled = true;
            this.txtTenViTri.Enabled = true;
            this.txtPCLuong.Enabled = true;
            this.txtMaViTri.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvViTriCV.CurrentCell.RowIndex;
                string strViTri =
                dgvViTriCV.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbViTriCV.XoaViTriCV(strViTri);
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
            this.panel3.Enabled = true;
            dgvViTriCV_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtMaViTri.Enabled = false;
            this.txtTenViTri.Enabled = true;
            this.txtPCLuong.Enabled = true;
            this.txtTenViTri.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLViTriCV blLoaiNV = new BLViTriCV();
                    blLoaiNV.ThemViTriCV(this.txtMaViTri.Text, this.txtTenViTri.Text, this.txtPCLuong.Text);
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
                    BLViTriCV blViTri = new BLViTriCV();
                    blViTri.CapNhatViTriCV(this.txtMaViTri.Text, this.txtTenViTri.Text, this.txtPCLuong.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvViTriCV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvViTriCV.CurrentCell.RowIndex;
            dgvViTriCV.Columns[0].HeaderText = "Mã Vị Trí";
            dgvViTriCV.Columns[0].Width = 165;
            this.txtMaViTri.Text =
            dgvViTriCV.Rows[r].Cells[0].Value.ToString();
            dgvViTriCV.Columns[1].HeaderText = "Tên Vị Trí";
            dgvViTriCV.Columns[1].Width = 165;
            this.txtTenViTri.Text =
            dgvViTriCV.Rows[r].Cells[1].Value.ToString();
            dgvViTriCV.Columns[2].HeaderText = "Phụ Cấp Lương";
            dgvViTriCV.Columns[2].Width = 165;
            this.txtPCLuong.Text =
            dgvViTriCV.Rows[r].Cells[2].Value.ToString();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {

            dgvViTriCV.AutoResizeColumns();
            this.txtMaViTri.ResetText();
            this.txtTenViTri.ResetText();
            this.txtPCLuong.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.txtMaViTri.Enabled = false;
            this.txtTenViTri.Enabled = false;
            this.txtPCLuong.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvViTriCV_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
