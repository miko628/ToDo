using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows;
using MySqlX.XDevAPI;
using System.Diagnostics;

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
                string connstring = string.Format("user='root',password='notSecureChangeMe',host='127.0.0.1', port='82',database='ToDoApp'");
                Connection = new MySqlConnection(builder.ConnectionString);
                //Connection = new MySqlConnection(connstring);
                Connection.Open();
                
            }


            Trace.WriteLine("koniec ? sie");

            return true;
        }

        public List<Task> GetAllTasks()
        {
            List<Task> new_tasks = new List<Task>();

            if (this.IsConnect())
            { 
                string query = "select * from Task";
                var cmd = new MySqlCommand(query, Connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    new_tasks.Add(new Task(reader.GetString("name"), Convert.ToDateTime(reader.GetString("do_date")), Convert.ToDateTime(reader.GetString("add_date")), reader.GetString("description"), Convert.ToBoolean(reader.GetString("done"))));
                    }

                    reader.Close();
                }
                Connection.Close();
            }
            return new_tasks;
        }
        /*        public void Connect()
                {
                    server = "localhost:81";
                    databasename = "localhost:81";
                    username = "localhost:81";
                    password= "localhost:81";
                    string connect = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, databasename, username, password);
                    MySqlConnection connection = new MySqlConnection(connect);
                    MySqlCommand command = new MySqlCommand("select * from `Tasks`",connection);
                    connection.Open();
                    connection.Close();
                }*/
        public void Close()
        {
            Connection.Close();
        }
    }
}
