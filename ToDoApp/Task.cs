using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    class Task
    {
        private string name;
        private string description;
        private DateTime taskAddDate;
        private DateTime taskToDoDate;
        private bool done;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public DateTime TaskAddDate { get => taskAddDate; set => taskAddDate = value; }
        public DateTime TaskToDoDate { get => taskToDoDate; set => taskToDoDate = value; }
        public bool Done { get => done; set => done = value; }
        public Task(string name, DateTime taskToDoDate, DateTime taskAddDate, string description = "",bool done = false)
        {
            this.Name = name;
            this.Description = description;
            this.TaskAddDate = taskAddDate;
            this.TaskToDoDate = taskToDoDate;
            this.Done = done;
        }
        public Task(string name,string description="",bool done = false)
        {
            this.Name = name;
            this.Description = description;
            this.TaskAddDate = DateTime.Now;
            this.TaskToDoDate = DateTime.Now;
            this.Done = done;
        }
    }
}
