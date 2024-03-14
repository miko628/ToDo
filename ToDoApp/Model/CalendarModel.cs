using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.Model
{
    class CalendarModel
    {
        public CalendarModel()
        {
            
        }
        public List<ToDoTask> GetGoogleTaks()
        {
            List<ToDoTask> tasks = new List<ToDoTask>();
            try
            {
                GoogleApiConnection apiConnection = new GoogleApiConnection();
                Events events = apiConnection.GetAllTasks();
                
            }
            catch(Exception ex) 
            {
            Trace.WriteLine(ex,"Problem z wczytaniem zadan z kalendarza google (CalendarModel).");
                return null;
            }
            return tasks;
        }

    }
}
