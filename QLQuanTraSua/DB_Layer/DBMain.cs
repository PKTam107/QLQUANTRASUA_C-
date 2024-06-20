using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLQuanTraSua.BS_Layer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLQuanTraSua.DB_Layer
{
    class DBMain
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=QLQuanTraSua;User Id=" + User.username + ";Password=" + User.password + ";");
        public SqlConnection getConnection
        {
            get
            {
                return con;
            }
        }
        SqlConnection conAdmin = new SqlConnection("Data Source=.;Initial Catalog=QLQuanTraSua;Integrated Security=True");
        public SqlConnection getConnectionAdmin
        {
            get
            {
                return conAdmin;
            }
        }
        // open the connection
        public void openConnection()
        {
            con = new SqlConnection(@"Data Source=.;Initial Catalog=QLQuanTraSua;User Id=" + User.username + ";Password=" + User.password + ";");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void openConnectionAdmin()
        {
            if (conAdmin.State == ConnectionState.Closed)
            {
                conAdmin.Open();
            }
        }
        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public void closeConnectionAdmin()
        {
            if (conAdmin.State == ConnectionState.Open)
            {
                conAdmin.Close();
            }
        }
    }
}
