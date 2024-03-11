using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Utility;

namespace ToDoApp.Model
{
    class HistoryModel
    {
        public bool UndoTask(ToDoTask? task)
        {
            if (task is not null)
            {
                try
                {
                    DbCrud.UndoneTask(task);

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex,"Problem przy zmianie zadania.(HistoryModel)");
                    return false;
                }
                //add exceptions

            }
            else return false;

            SoundNotification.PlayNotificationSound();
            return true;

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
            else return false;

            return true;
        }
    }
}
