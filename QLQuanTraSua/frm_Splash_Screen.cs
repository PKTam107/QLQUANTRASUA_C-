using QLQuanTraSua.DB_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanTraSua
{
    public partial class frm_Splash_Screen : Form
    {
        public frm_Splash_Screen()
        {
            InitializeComponent();
        }
        public string Server { get; set; }
        DBMain db = new DBMain();
        private void timer1_Tick(object sender, EventArgs e)
        {
            frm_DangNhap fr = new frm_DangNhap();
            fr.Show();
            this.Hide();
            timer1.Enabled = false;
        }

        private void fr_Splash_Screen_Load(object sender, EventArgs e)
        {
            StreamReader read = new StreamReader("Sinfo");
            this.Server = (read.ReadLine().Split(':')[1]);
            try
            {
                db.openConnectionAdmin();
                db.closeConnectionAdmin();
                timer1.Enabled = true;
                read.Close();
            }
            catch
            {
                frm_Ketnoi fr = new frm_Ketnoi();
                read.Close();
                fr.ShowDialog();

            }
        }
    }
}
