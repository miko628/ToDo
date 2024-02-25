using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    class ToDoTask
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
        public ToDoTask(string name, string? taskToDoDate, string? taskAddDate, string? description,string? done)
        {
            this.Name = name;
            if(description=="" )
            {
                this.Description = "";
            }
            else this.Description = description;

            if (taskToDoDate =="")
            {
                this.TaskToDoDate = DateTime.Now;

            }
            else this.TaskToDoDate = Convert.ToDateTime(taskToDoDate); 

            if(taskAddDate == "")
            {
                this.taskAddDate = DateTime.Now;
            }
            else this.TaskAddDate = Convert.ToDateTime(taskAddDate);

            if (done == "")
            {
                this.Done = false;
            }
            else this.Done = Convert.ToBoolean(done);
        }
       /* public Task(string name,string description="",bool done = false)
        {
            this.Name = name;
            this.Description = description;
            this.TaskAddDate = DateTime.Now;
            this.TaskToDoDate = DateTime.Now;
            this.Done = done;
        }*/
    }
}
