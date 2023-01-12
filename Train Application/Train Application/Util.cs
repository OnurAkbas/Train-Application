using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Train_Application
    {
        public static class Util
        {
        public static Login loginForm;
        public static AdminMain AdminPage;

        private static MySqlConnection _Connection;
        public static int Level = 0;
        public static string ServerHost = "Localhost";
        public static string DatabaseName = "Train";
        public static string DBUserName = "root";
        public static string DBPWD = "root";

        public static string getConnectString()
        {
            return "SERVER = " + ServerHost + "; DATABASE = " + DatabaseName + "; UID = " + DBUserName + "; PASSWORD = " + DBPWD + "; Convert Zero Datetime = True";
        }

        public static MySqlConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlConnection(getConnectString());
                }
                return _Connection;
            }
        }
    }
    }


