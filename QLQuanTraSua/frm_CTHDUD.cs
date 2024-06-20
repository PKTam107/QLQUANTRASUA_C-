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
    public partial class frm_CTHDUD : Form
    {
        DataTable dtCTHDUD = null;
        bool Them;
        BLCTHD_UD dbCTHDUD = new BLCTHD_UD();
        private string mahdb;
        public frm_CTHDUD()
        {
            InitializeComponent();
        }
        public string MaHDB
        {
            get
            {
                return mahdb;
            }
            set
            {
                mahdb = value;
            }
        }

        void LoadComboBoxMaSP()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbCTHDUD.LayMaSP();
                cbMaSP.DataSource = dt;
                cbMaSP.DisplayMember = "MaSP";
                cbMaSP.ValueMember = "MaSP";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table SanPham. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void LoadData()
        {
            this.txtSoHDUD.Text = MaHDB;
            LoadComboBoxMaSP();
            try
            {
                dtCTHDUD = new DataTable();
                dtCTHDUD.Clear();
                dtCTHDUD = dbCTHDUD.LayChiTietHoaDonUngDung();
                dgvCTHDUD.DataSource = dtCTHDUD;
                dgvCTHDUD.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCTHDUD.Columns[0].HeaderText = "Mã HDUD";
                dgvCTHDUD.Columns[0].Frozen = true;
                dgvCTHDUD.Columns[1].HeaderText = "Mã Sản Phẩm";
                dgvCTHDUD.Columns[1].Frozen = true;
                dgvCTHDUD.Columns[2].HeaderText = "Số lượng";
                dgvCTHDUD.Columns[3].HeaderText = "Đơn giá";
                dgvCTHDUD.Columns[4].HeaderText = "Tổng Tiền";
                this.txtSL.ResetText();
                this.txtDG.ResetText();
                this.txtTT.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.panel4.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;

                LoadComboBoxMaSP();
                cbMaSP.SelectedIndexChanged += cbMaSP_SelectedIndexChanged;
                dgvCTHDUD_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Chi Tiết Đơn Bán. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_CTHDUD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtSL.ResetText();
            this.txtDG.ResetText();
            this.txtTT.ResetText();
            this.cbMaSP.ResetText();
            this.lbTenSP.ResetText();
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel4.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtSL.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            this.panel4.Enabled = true;
            dgvCTHDUD_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel4.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtSL.Focus();
        }

        private void dgvCTHDUD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvCTHDUD.CurrentCell.RowIndex;
            this.txtSoHDUD.Text =
            dgvCTHDUD.Rows[r].Cells[0].Value.ToString();
            this.cbMaSP.Text =
            dgvCTHDUD.Rows[r].Cells[1].Value.ToString();
            this.txtSL.Text =
            dgvCTHDUD.Rows[r].Cells[2].Value.ToString();
            this.txtDG.Text =
            dgvCTHDUD.Rows[r].Cells[3].Value.ToString();
            this.txtTT.Text =
            dgvCTHDUD.Rows[r].Cells[4].Value.ToString();
            string maSP = this.cbMaSP.Text;
            DataRow[] r1 = dbCTHDUD.LayMaSP().Select("MaSP='" + maSP + "'");
            if (r1.Length > 0)
            {
                this.lbTenSP.Text = r1[0]["TenSP"].ToString();
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.txtSL.ResetText();
            this.txtDG.ResetText();
            this.txtTT.ResetText();
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel4.Enabled = false;
            dgvCTHDUD_CellClick(null, null);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            BLCTHD_UD blCTHDUD = new BLCTHD_UD();
            if (Them)
            {
                try
                {
                    float tt = (float.Parse(txtSL.Text) * float.Parse(txtDG.Text));
                    blCTHDUD.ThemChiTietHoaDonUngDung(this.txtSoHDUD.Text, this.cbMaSP.Text, int.Parse(txtSL.Text), float.Parse(txtSL.Text), tt);
                    blCTHDUD.CapNhatThanhTien(this.txtSoHDUD.Text);
                    LoadData();
                    MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không lưu được. Lỗi rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                float tt = (float.Parse(txtSL.Text) * float.Parse(txtDG.Text));
                blCTHDUD.CapNhatChiTietHoaDonUngDung(this.txtSoHDUD.Text, this.cbMaSP.Text, int.Parse(txtSL.Text), float.Parse(txtSL.Text), tt);
                blCTHDUD.CapNhatThanhTien(this.txtSoHDUD.Text);
                LoadData();
                MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvCTHDUD.CurrentCell.RowIndex;
                string strMaHD =
                dgvCTHDUD.Rows[r].Cells[0].Value.ToString();
                string strMaSP =
                dgvCTHDUD.Rows[r].Cells[1].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbCTHDUD.XoaChiTietHoaDonUngDung(strMaHD, strMaSP);
                    LoadData();
                    MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTenSP.Text = dbCTHDUD.LoadTenSP(cbMaSP.Text);
            txtDG.Text = dbCTHDUD.LoadDonGia(cbMaSP.Text);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
