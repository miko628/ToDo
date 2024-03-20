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
        public bool UpdateTask(ToDoTask task,string name,string description,DateTime toDoDate,bool done)
        {
            try
            {
                DbCrud.UpdateTask(task,name,description,toDoDate,done);
            }
            catch(Exception ex) 
            {
                Trace.WriteLine(ex,"Wystąpił błąd przy aktualizowaniu zadania! (TaskInfoModel)");
            }
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
            Trace.WriteLine(ex, "Wystąpił błąd przy usuwaniu zadania! (TaskInfoModel)");
                return false;
            }
            SoundNotification.PlayNotificationSound();

            return true;
        }
        
        public ToDoTask GetTask(string id)
        {
            ToDoTask task;
            try
            {
                task=DbCrud.GetTask(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "Wystąpił błąd przy usuwaniu zadania.(TaskInfoModel)");
                return null;
            }
            SoundNotification.PlayNotificationSound();

            return task;
        }
    }
}
