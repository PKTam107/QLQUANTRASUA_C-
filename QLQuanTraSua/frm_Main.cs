using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanTraSua
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }
        private Form kiemtratontai(Type formtype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == formtype)
                    return f;
            }
            return null;
        }
        private void loạiNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_LoaiNhanVien));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_LoaiNhanVien fr = new frm_LoaiNhanVien();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void nhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_NhanVien));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_NhanVien fr = new frm_NhanVien();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_KhachHang));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_KhachHang fr = new frm_KhachHang();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_NCC));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_NCC fr = new frm_NCC();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void loạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_LoaiSP));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_LoaiSP fr = new frm_LoaiSP();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void hóaĐơnNhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_HDN));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_HDN fr = new frm_HDN();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void hóaĐơnXuấtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_HDB));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_HDB fr = new frm_HDB();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void fr_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            if (e.Cancel == false)
            {
                this.Dispose();
                Application.Exit();
            }
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_DangNhap fr = new frm_DangNhap();
            this.Hide();
            fr.Show();
        }

        private void fr_Main_Resize(object sender, EventArgs e)
        {
            //pictureBox1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void vịTríCôngViệcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_ViTriCongViec));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_ViTriCongViec fr = new frm_ViTriCongViec();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void bảngPhânCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_BangPhanCa));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_BangPhanCa fr = new frm_BangPhanCa();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void caLàmViệcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_CaLamViec));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_CaLamViec fr = new frm_CaLamViec();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void sảnPhẩmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_SanPham));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_SanPham fr = new frm_SanPham();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void chếBiếnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_CheBien));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_CheBien fr = new frm_CheBien();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void nguyênLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_NguyenLieu));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_NguyenLieu fr = new frm_NguyenLieu();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void ứngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_UngDung));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_UngDung fr = new frm_UngDung();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void hóaĐơnỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_HDUD));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_HDUD fr = new frm_HDUD();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void chiTiếtHóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_CTHDN));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_CTHDN fr = new frm_CTHDN();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void chiTiếtHóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_CTHDB));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_CTHDB fr = new frm_CTHDB();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void chiTiếtHóaĐơnỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_CTHDUD));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_CTHDUD fr = new frm_CTHDUD();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        private void quanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = kiemtratontai(typeof(frm_Account));
            if (frm != null)
                frm.Activate();
            else
            {
                frm_Account fr = new frm_Account();
                fr.MdiParent = this;
                fr.Show();
            }
        }
    }
}
