using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoApp.Model
{
    class HistoryModel
    {
        public bool UndoTask(ToDoTask? task)
        {
            if (task is not null)
            {
                DbCrud.UndoneTask(task);
                //add exceptions
                return true;
            }
            else return false;
        }
        public List<ToDoTask> GetAllHistoryTasks()
        {
            return DbCrud.getHistoryTasks();
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
