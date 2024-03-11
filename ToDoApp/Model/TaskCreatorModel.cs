using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.Model
{
    class TaskCreatorModel
    {
        public bool SaveTask(string name,DateTime toDoDate,string description)
        {
           
            var task = new ToDoTask(name, toDoDate.ToString(), DateTime.Now.ToString(), description, "", "");
            try
            {
                DbCrud.InsertTask(task);


            }
            catch (Exception ex) 
            {
                Trace.WriteLine(ex,"Blad przy probie zapisania zadania!(TaskCreatorModel)");
                return false;
            }

            SoundNotification.PlayNotificationSound();

            return true;
        }
    }
}
