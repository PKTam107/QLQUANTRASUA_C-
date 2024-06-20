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
    public partial class frm_CTHDN : Form
    {
        DataTable dtCTHDN = null;
        bool Them;
        BL_CTHDN dbCTHDN = new BL_CTHDN();
        public frm_CTHDN()
        {
            InitializeComponent();
        }

        void LoadComboBoxMaSP()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dbCTHDN.LayMaNL();
                cbMaNL.DataSource = dt;
                cbMaNL.DisplayMember = "MaNL";
                cbMaNL.ValueMember = "MaNL";
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NguyenLieu. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string mahdn;
        public string MaHDN
        {
            get
            {
                return mahdn;
            }
            set
            {
                mahdn = value;
            }
        }
        void LoadData()
        {
            try
            {
                dtCTHDN = new DataTable();
                dtCTHDN.Clear();
                dtCTHDN = dbCTHDN.LayCTHDN();
                dgvCTHDN.DataSource = dtCTHDN;
                dgvCTHDN.DefaultCellStyle.ForeColor = Color.Black;
                dgvCTHDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvCTHDN.Columns[0].HeaderText = "Mã HDN";
                dgvCTHDN.Columns[0].Frozen = true;
                dgvCTHDN.Columns[1].HeaderText = "Mã Nguyên Liệu";
                dgvCTHDN.Columns[1].Frozen = true;
                dgvCTHDN.Columns[2].HeaderText = "Đơn Giá";
                dgvCTHDN.Columns[3].HeaderText = "Số Lượng";
                dgvCTHDN.Columns[4].HeaderText = "Đơn Vị";
                dgvCTHDN.Columns[5].HeaderText = "Thành Tiền";
                this.txtSL.ResetText();
                this.txtDV.ResetText();
                this.txtDG.ResetText();
                this.txtTT.ResetText();
                this.btnLuu.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.panel4.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                //
                LoadComboBoxMaSP();
                cbMaNL.SelectedIndexChanged += cbMaSP_SelectedIndexChanged;
                dgvCTHDN_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Chi Tiết Hóa Đơn Nhập. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_CTHDN_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtSL.ResetText();
            this.txtDG.ResetText();
            this.txtDV.ResetText();
            this.txtTT.ResetText();
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
            dgvCTHDN_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel2.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtSL.Focus();
        }

        private void dgvCTHDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvCTHDN.CurrentCell.RowIndex;
            this.txtSoHD.Text =
            dgvCTHDN.Rows[r].Cells[0].Value.ToString();
            dgvCTHDN.Columns[0].HeaderText = "Mã HDN";
            dgvCTHDN.Columns[0].Width = 150;
            this.cbMaNL.Text =
            dgvCTHDN.Rows[r].Cells[1].Value.ToString();
            dgvCTHDN.Columns[1].HeaderText = "Mã Nguyên Liệu";
            dgvCTHDN.Columns[1].Width = 150;
            this.txtDG.Text =
            dgvCTHDN.Rows[r].Cells[2].Value.ToString();
            dgvCTHDN.Columns[2].HeaderText = "Đơn giá";
            dgvCTHDN.Columns[2].Width = 150;
            this.txtSL.Text =
            dgvCTHDN.Rows[r].Cells[3].Value.ToString();
            dgvCTHDN.Columns[3].HeaderText = "Số Lượng";
            dgvCTHDN.Columns[3].Width = 150;
            this.txtDV.Text =
            dgvCTHDN.Rows[r].Cells[4].Value.ToString();
            dgvCTHDN.Columns[4].HeaderText = "Đơn Vị";
            dgvCTHDN.Columns[4].Width = 150;
            this.txtTT.Text =
            dgvCTHDN.Rows[r].Cells[5].Value.ToString();
            dgvCTHDN.Columns[5].HeaderText = "Tổng Tiền";
            dgvCTHDN.Columns[5].Width = 150;
            string maNL = this.cbMaNL.Text;
            DataRow[] r1 = dbCTHDN.LayMaNL().Select("MaNL='" + maNL + "'");
            if (r1.Length > 0)
            {
                this.lbTenNL.Text = r1[0]["TenNL"].ToString();
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.txtSL.ResetText();
            this.txtDG.ResetText();
            this.txtDV.ResetText();
            this.txtTT.ResetText();
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel4.Enabled = false;
            dgvCTHDN_CellClick(null, null);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            BL_CTHDN blCTHDN = new BL_CTHDN();
            if (Them)
            {
                try
                {
                    float tt = float.Parse(txtSL.Text) * float.Parse(txtDG.Text);
                    blCTHDN.ThemCTHDN(this.txtSoHD.Text, this.cbMaNL.Text, float.Parse(txtDG.Text), int.Parse(txtSL.Text), this.txtDV.Text, tt);
                    blCTHDN.CapNhatThanhTien(this.txtSoHD.Text);
                    LoadData();
                    MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không lưu được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                float tt = float.Parse(txtSL.Text) * float.Parse(txtDG.Text);
                blCTHDN.CapNhatCTHDN(this.txtSoHD.Text, this.cbMaNL.Text, float.Parse(txtDG.Text), int.Parse(txtSL.Text), this.txtDV.Text, tt);
                LoadData();
                MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvCTHDN.CurrentCell.RowIndex;
                string strMaHD =
                dgvCTHDN.Rows[r].Cells[0].Value.ToString();
                string strMaSP =
                dgvCTHDN.Rows[r].Cells[1].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbCTHDN.XoaCTHDN(strMaHD, strMaSP);
                    LoadData();
                    MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thực hiện việc xóa mẫu tin!");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTenNL.Text = dbCTHDN.LoadTenNL(cbMaNL.Text);
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            frm_HDN fr = new frm_HDN();
            this.Close();
            fr.Show();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
