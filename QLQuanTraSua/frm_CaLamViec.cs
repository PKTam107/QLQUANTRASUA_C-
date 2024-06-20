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
    public partial class frm_CaLamViec : Form
    {
        DataTable CaLamViec = null;
        bool Them;
        BLCaLamViec dbCaLV = new BLCaLamViec();
        public frm_CaLamViec()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                CaLamViec = new DataTable();
                CaLamViec.Clear();
                CaLamViec = dbCaLV.LayCaLamViec();
                dgvCaLamViec.DataSource = CaLamViec;
                dgvCaLamViec.AutoResizeColumns();
                this.txtMaCa.ResetText();
                this.txtNgay.ResetText();
                this.txtGioBD.ResetText();
                this.txtGioKT.ResetText();
                this.btnLuu.Enabled = false;
                this.txtMaCa.Enabled = false;
                this.txtNgay.Enabled = false;
                this.txtGioBD.Enabled = false;
                this.txtGioKT.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                dgvCaLamViec_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table CaLamViec. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fr_CaLamViec_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtMaCa.ResetText();
            this.txtNgay.ResetText();
            this.txtGioBD.ResetText();
            this.txtGioKT.ResetText();
            this.btnLuu.Enabled = true;
            //this.grBox.Enabled = true;
            this.panel2.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnHuyBo.Enabled = true;
            this.txtMaCa.Enabled = true;
            this.txtNgay.Enabled = true;
            this.txtGioBD.Enabled = true;
            this.txtGioKT.Enabled = true;
            this.txtMaCa.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvCaLamViec.CurrentCell.RowIndex;
                string maCa = dgvCaLamViec.Rows[r].Cells["MaCa"].Value.ToString();
                string ngay = dgvCaLamViec.Rows[r].Cells["Ngay"].Value.ToString();

                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (traloi == DialogResult.Yes)
                {
                    string err = string.Empty;
                    bool result = dbCaLV.XoaCaLamViec(maCa, ngay);

                    if (result)
                    {
                        LoadData();
                        MessageBox.Show("Đã xóa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không xóa được. Lỗi: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            //this.grBox.Enabled = true;
            this.panel2.Enabled = true;
            dgvCaLamViec_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnHuyBo.Enabled = true;
            this.txtMaCa.Enabled = false;
            this.txtNgay.Enabled = false;
            this.txtGioBD.Enabled = true;
            this.txtGioKT.Enabled = true;
            this.txtGioBD.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLCaLamViec blCaLV = new BLCaLamViec();
                    blCaLV.ThemCaLamViec(this.txtMaCa.Text, this.txtNgay.Text, this.txtGioBD.Text, this.txtGioKT.Text);
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
                    BLCaLamViec blCaLV = new BLCaLamViec();
                    blCaLV.CapNhatCaLamViec(this.txtMaCa.Text, this.txtNgay.Text, this.txtGioBD.Text, this.txtGioKT.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCaLamViec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvCaLamViec.CurrentCell.RowIndex;
            dgvCaLamViec.Columns[0].HeaderText = "Mã Ca";
            dgvCaLamViec.Columns[0].Width = 165;
            this.txtMaCa.Text =
            dgvCaLamViec.Rows[r].Cells[0].Value.ToString();
            dgvCaLamViec.Columns[1].HeaderText = "Ngày";
            dgvCaLamViec.Columns[1].Width = 165;
            this.txtNgay.Text =
            dgvCaLamViec.Rows[r].Cells[1].Value.ToString();
            dgvCaLamViec.Columns[2].HeaderText = "Giờ Bắt Đầu";
            dgvCaLamViec.Columns[2].Width = 165;
            this.txtGioBD.Text =
            dgvCaLamViec.Rows[r].Cells[2].Value.ToString();
            dgvCaLamViec.Columns[3].HeaderText = "Giờ Kết Thúc";
            dgvCaLamViec.Columns[3].Width = 165;
            this.txtGioKT.Text =
            dgvCaLamViec.Rows[r].Cells[3].Value.ToString();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            dgvCaLamViec.AutoResizeColumns();
            this.txtMaCa.ResetText();
            this.txtNgay.ResetText();
            this.txtGioBD.ResetText();
            this.txtGioKT.ResetText();
            this.btnLuu.Enabled = false;
            this.txtMaCa.Enabled = false;
            this.txtNgay.Enabled = false;
            this.txtGioBD.Enabled = false;
            this.txtGioKT.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnHuyBo.Enabled = false;
            dgvCaLamViec_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
