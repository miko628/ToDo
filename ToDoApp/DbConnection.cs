using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows;

namespace ToDoApp
{
    class DbConnection
    {
        private string server;
        private string databasename;
        private string username;
        private string password;
        private string port;
        public MySqlConnection Connection { get; private set; }

        private static DbConnection _instance = null;

        public static DbConnection Instance()
        {
            if (_instance == null)
                _instance = new DbConnection();
            return _instance;
        }
        public bool IsConnect()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1:81",
                UserID = "root",
                Password = "notSecureChangeMe",
                Database = "ToDoApp",
                
            };

            

            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databasename))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; userid={2}; password={3}; port={4}; SslMode=None", server, databasename, username, password, port);
                Connection = new MySqlConnection(builder.ConnectionString);
                Connection.Open();
            }

            return true;
        }

        public void Connect()
        {
            server = "localhost:81";
            databasename = "localhost:81";
            username = "localhost:81";
            password= "localhost:81";
            string connect = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, databasename, username, password);
            MySqlConnection connection = new MySqlConnection(connect);
            MySqlCommand command = new MySqlCommand("select * from Tasks",connection);
            connection.Open();
            connection.Close();
        }
        public void Close()
        {
            Connection.Close();
        }
    }
}
