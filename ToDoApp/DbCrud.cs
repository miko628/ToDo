using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace ToDoApp
{
    class DbCrud
    {
       public static List<ToDoTask> getAllTasks()
        {
            List<ToDoTask> new_tasks = new List<ToDoTask>();
            string query = "select * from Task";

            DbConnection conn = DbConnection.Instance();
            
            if (conn.IsConnect())
            {
                var Command=new MySqlCommand(query, conn.Connection);
                using (MySqlDataReader reader=Command.ExecuteReader()) 
                {

                    try
                    {

                        while (reader.Read())
                        {

                            var task = new ToDoTask(reader[1].ToString(), reader[4].ToString(), reader[3].ToString(), reader[2].ToString(), reader[5].ToString(), reader[0].ToString());
                            new_tasks.Add(task);

                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.ToString());
                    }
                    reader.Close();

                }
            }
            conn.Close();
            return new_tasks;
        }
        public static List<ToDoTask> getCurrentTasks()
        {
            List<ToDoTask> new_tasks = new List<ToDoTask>();
            string query = "select * from Task where done = 0 order by do_date";

            DbConnection conn = DbConnection.Instance();

            if (conn.IsConnect())
            {
                var Command = new MySqlCommand(query, conn.Connection);
                using (MySqlDataReader reader = Command.ExecuteReader())
                {

                    try
                    {

                        while (reader.Read())
                        {

                            var task = new ToDoTask(reader[1].ToString(), reader[4].ToString(), reader[3].ToString(), reader[2].ToString(), reader[5].ToString(), reader[0].ToString());
                            new_tasks.Add(task);

                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.ToString());
                    }
                    reader.Close();

                }
            }
            conn.Close();
            return new_tasks;

        }

        public static List<ToDoTask> getHistoryTasks()
        {
            List<ToDoTask> new_tasks = new List<ToDoTask>();
            string query = "select * from Task where done = 1 order by do_date";

            DbConnection conn = DbConnection.Instance();

            if (conn.IsConnect())
            {
                var Command = new MySqlCommand(query, conn.Connection);
                using (MySqlDataReader reader = Command.ExecuteReader())
                {

                    try
                    {

                        while (reader.Read())
                        {

                            var task = new ToDoTask(reader[1].ToString(), reader[4].ToString(), reader[3].ToString(), reader[2].ToString(), reader[5].ToString(), reader[0].ToString());
                            new_tasks.Add(task);

                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.ToString());
                    }
                    reader.Close();

                }
            }
            conn.Close();
            return new_tasks;

        }
        
        public static void InsertTask(ToDoTask task)
        {

            DbConnection conn = DbConnection.Instance();

            if (conn.IsConnect())
            {
                var Command = conn.Connection.CreateCommand();
                Command.CommandText = "INSERT INTO Task(name,description,add_date,do_date,done) VALUES(@name, @description, @add_date, @do_date,@done)";
                Command.Parameters.AddWithValue("@name", task.Name);
                Command.Parameters.AddWithValue("@description", task.Description);
                Command.Parameters.AddWithValue("@add_date", task.TaskAddDate);
                Command.Parameters.AddWithValue("@do_date", task.TaskToDoDate);

                if(task.Done is true)
                {
                    Command.Parameters.AddWithValue("@done", 1);

                }else Command.Parameters.AddWithValue("@done", 0);

                Command.ExecuteNonQuery();
            }
                
            conn.Close();
        }
        public static void DoneTask(ToDoTask task) 
        {
            DbConnection conn = DbConnection.Instance();

            if (conn.IsConnect())
            {
                var Command = conn.Connection.CreateCommand();
                Command.CommandText = "update Task set done = 1 where id=@id";
                Command.Parameters.AddWithValue("@id", task.Id);

                Command.ExecuteNonQuery();
            }

            conn.Close();

        }

        public static void UndoneTask(ToDoTask task)
        {
            DbConnection conn = DbConnection.Instance();

            if (conn.IsConnect())
            {
                var Command = conn.Connection.CreateCommand();
                Command.CommandText = "update Task set done = 0 where id=@id";
                Command.Parameters.AddWithValue("@id", task.Id);

                Command.ExecuteNonQuery();
            }

            conn.Close();
        }

        public static void DeleteTask(ToDoTask task) 
        {
            DbConnection conn = DbConnection.Instance();

            if (conn.IsConnect())
            {
                var Command = conn.Connection.CreateCommand();
                Command.CommandText = "delete from Task where id=@id";
                Command.Parameters.AddWithValue("@id", task.Id);

                Command.ExecuteNonQuery();
            }

            conn.Close();
        }


    }
}
