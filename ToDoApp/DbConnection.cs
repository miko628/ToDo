using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows;
using System.Diagnostics;
using System.Configuration;

namespace ToDoApp
{
    class DbConnection
    {
        private string server;
        private string databasename;
        private string username;
        private string password;
        private string port;
        public MySqlConnection Connection { get; set; }

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
                Server = "127.0.0.1",
                Port = 82,
                UserID = "root",
                Password = "notSecureChangeMe",
                Database = "ToDoApp",
                
            };

            

            if (Connection == null)
            {
                string connstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                Connection = new MySqlConnection(builder.ConnectionString);
                //Connection = new MySqlConnection(connstring);
                try
                { Connection.Open(); }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex, "Database connection failed.");
                    return false;
                }
                
            }else if (Connection.State == System.Data.ConnectionState.Closed)
            {
                try
                { Connection.Open(); }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex, "Database connection failed.");
                    return false;
                }
            }



            return true;
        }

       
  
        public void Close()
        {
            Connection.Close();
        }
    }
}
