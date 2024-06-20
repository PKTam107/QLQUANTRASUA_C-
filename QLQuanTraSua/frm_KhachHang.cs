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
    public partial class frm_KhachHang : Form
    {
        DataTable tb_KhachHang = null;
        bool Them;
        BLKhachHang dbKh = new BLKhachHang();
        public frm_KhachHang()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                tb_KhachHang = new DataTable();
                tb_KhachHang.Clear();
                tb_KhachHang = dbKh.LayKhachHang();
                dgvKHACHHANG.DataSource = tb_KhachHang;
                dgvKHACHHANG.AutoResizeColumns();
                this.txtSDT.ResetText();
                this.txtTenKH.ResetText();
                this.txtDiaChi.ResetText();
                this.btnLuu.Enabled = false;
                this.grBox.Enabled = false;
                this.btnHuyBo.Enabled = false;
                this.btnThem.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnXoa.Enabled = true;
                //
                dgvKHACHHANG_CellClick(null, null);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table KhachHang. Lỗi rồi!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them = true;
            this.txtSDT.ResetText();
            this.txtTenKH.ResetText();
            this.txtDiaChi.ResetText();
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.grBox.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtSDT.Enabled = true;
            this.txtSDT.Focus();
            this.txtTenKH.Focus();
            this.txtDiaChi.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Them = false;
            dgvKHACHHANG_CellClick(null, null);
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.grBox.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.txtSDT.Enabled = false;
            this.txtTenKH.Focus();
            this.txtDiaChi.Focus();
        }

        private void dgvKHACHHANG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvKHACHHANG.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            int r = dgvKHACHHANG.CurrentCell.RowIndex;
            dgvKHACHHANG.Columns[0].HeaderText = "Số Điện Thoại";
            dgvKHACHHANG.Columns[0].Width = 150;
            dgvKHACHHANG.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.txtSDT.Text =
            dgvKHACHHANG.Rows[r].Cells[0].Value.ToString();
            dgvKHACHHANG.Columns[1].HeaderText = "Tên Khách Hàng";
            dgvKHACHHANG.Columns[1].Width = 150;
            this.txtTenKH.Text =
            dgvKHACHHANG.Rows[r].Cells[1].Value.ToString();
            dgvKHACHHANG.Columns[2].HeaderText = "Địa Chỉ";
            dgvKHACHHANG.Columns[2].Width = 150;
            this.txtDiaChi.Text =
            dgvKHACHHANG.Rows[r].Cells[2].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    BLKhachHang blKH = new BLKhachHang();
                    blKH.ThemKhachHang(this.txtSDT.Text, this.txtTenKH.Text, this.txtDiaChi.Text);
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
                    BLKhachHang blKH = new BLKhachHang();
                    blKH.CapNhatKhachHang(this.txtSDT.Text, this.txtTenKH.Text, this.txtDiaChi.Text);
                    LoadData();
                    MessageBox.Show("Đã sửa xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Không sửa được. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = dgvKHACHHANG.CurrentCell.RowIndex;
                string strKH =
                dgvKHACHHANG.Rows[r].Cells[0].Value.ToString();
                DialogResult traloi;
                traloi = MessageBox.Show("Chắc xóa mẫu tin này không?", "Trả lời",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (traloi == DialogResult.Yes)
                {
                    dbKh.XoaKhachHang(strKH);
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
            dgvKHACHHANG.AutoResizeColumns();
            this.txtSDT.ResetText();
            this.txtTenKH.ResetText();
            this.txtDiaChi.ResetText();
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.grBox.Enabled = false;
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            dgvKHACHHANG_CellClick(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTKKH_Click(object sender, EventArgs e)
        {
            if (txtTK.Text != "")
            {
                try
                {
                    BLKhachHang tkKhachHang = new BLKhachHang();

                    string InforToSearch = txtTK.Text;
                    DataTable resultTable = null;

                    if (op1.Checked)
                    {
                        resultTable = tkKhachHang.TimKiemKhachHangBangSDT(InforToSearch);
                    }
                    else if (op2.Checked)
                    {
                        resultTable = tkKhachHang.TimKiemKhachHangBangDiaChi(InforToSearch);
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
