using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.Model
{
    class TaskInfoModel
    {
        public bool UpdateTask(ToDoTask task,string name,string description,DateTime toDoDate)
        {
            return true;
        }
        public bool DeleteTask(ToDoTask task)
        {
            try
            {
                DbCrud.DeleteTask(task);
            }
            catch(Exception ex) 
            {
            Trace.WriteLine(ex,"blad przy usuwaniu zadania.(TaskInfoModel)");
                return false;
            }
            SoundNotification.PlayNotificationSound();

            return true;
        }
        
    }
}
