using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoApp.Model
{
    class TaskModel
    {
        public bool DoneTask (ToDoTask? task)
        {
            if ( task is not null)
            {

                DbCrud.DoneTask (task);
                // exceptions check
                return true;
            }else
            return false;
        }
        public List<ToDoTask> GetAllTasks()
        {
            return DbCrud.getCurrentTasks();
        }
        public bool DeleteTask(ToDoTask? task)
        {
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (task is not null)
                {
                    DbCrud.DeleteTask(task);
                    //LoadTasks(new object());
                }
                else
                {
                    MessageBox.Show("Nie poprawny parametr");
                    return false;
                }
            }
            return true;
        }
    }
}
