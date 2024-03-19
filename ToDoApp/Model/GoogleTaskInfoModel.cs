using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.Model
{
    class GoogleTaskInfoModel
    {
        public bool DeleteEvent(Event task)
        {
            GoogleApiConnection apiConnection = new GoogleApiConnection();
            return apiConnection.DeleteEvent(task);
        }
        public bool UpdateEvent(Event task,string name,string description,DateTime date)
        {
            //task.Summary
            GoogleApiConnection apiConnection = new GoogleApiConnection();
            return apiConnection.UpdateEvent(task,name,description,date);
        }
        public Event? GetEvent(string id)
        {
            GoogleApiConnection apiConnection = new GoogleApiConnection();
            return apiConnection.GetEvent(id);
        }
    }
}
