using Google.Apis.Calendar.v3.Data;
using Google.Protobuf.WellKnownTypes;
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
        public Events? GetGoogleTaks()
        {
            //List<Event> tasks = new List<ToDoTask>();
            Events events;
            try
            {
                GoogleApiConnection apiConnection = new GoogleApiConnection();
                events = apiConnection.GetAllTasks();
                foreach(var item in events.Items)
                {
                    Trace.WriteLine("name (summary) :"+item.Summary);
                    Trace.WriteLine("description :" + item.Description);
                    Trace.WriteLine("start :" + item.Start.DateTime);
                    Trace.WriteLine("end :" + item.End.DateTime);
                    Trace.WriteLine("done :" + item.Status);


                    //tasks.Add(new ToDoTask(item.etag));
                }
            }
            catch(Exception ex) 
            {
            Trace.WriteLine(ex,"Problem z wczytaniem zadan z kalendarza google (CalendarModel).");
                return null;
            }
            return events;
        }
        public bool DeleteEvent(Event task)
        {
                GoogleApiConnection apiConnection = new GoogleApiConnection();
                return apiConnection.DeleteEvent(task);

            
        }
        public bool SynchronizeTasks()
        {
            try
            {
                GoogleApiConnection api = new GoogleApiConnection();
                List<ToDoTask> tasks = DbCrud.getCurrentTasks();
                api.SyncrhonizeTasks(tasks);
            }
            catch (Exception ex) 
            {
                Trace.WriteLine(ex, "Bląd przy próbie synchronizacji zadań. (CalendarModel)");
            }
            return true;
        }

    }
}
