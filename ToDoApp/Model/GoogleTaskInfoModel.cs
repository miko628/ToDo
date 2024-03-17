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
        public bool UpdateEvent(Event task)
        {
            GoogleApiConnection apiConnection = new GoogleApiConnection();
            return apiConnection.UpdateEvent(task);
        }
    }
}
