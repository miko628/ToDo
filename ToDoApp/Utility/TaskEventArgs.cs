using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Utility
{
    public class TaskEventArgs: EventArgs
    {
        private readonly ToDoTask _task;
        public TaskEventArgs(ToDoTask task)
        {
            _task = task;
        }
        public ToDoTask Task { get { return _task; } }
    }
}
