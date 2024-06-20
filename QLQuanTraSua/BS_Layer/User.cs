using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLQuanTraSua.DB_Layer;
using System.Data.SqlClient;

namespace QLQuanTraSua.BS_Layer
{
    class User
    {
        public static string username;
        public static string password;

        public string getUsername
        {
            get { return username; }
            set { username = value; }
        }
        public string getPassword
        {
            get { return password; }
            set { password = value; }
        }
    }
}
