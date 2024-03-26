using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.Model
{
    class HistoryInfoModel
    {
        public bool DeleteTask(ToDoTask task)
        {
            try
            {
                DbCrud.DeleteTask(task);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "Wystąpił błąd przy usuwaniu zadania! (HistoryInfoModel)");
                return false;
            }
            SoundNotification.PlayNotificationSound();

            return true;
        }
    }
}
