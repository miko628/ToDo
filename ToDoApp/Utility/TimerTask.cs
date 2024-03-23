using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ToDoApp.Utility
{
    public class TimerTask
    {
        private DispatcherTimer timer;
        private ToDoTask task;
        public event EventHandler Tick;

        public TimerTask(ToDoTask task)
        {
            this.task = task;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) => Timer_Tick(sender, e);
            timer.Start();


        }
        public string GetId()
        {
            return task.Id;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
     /*   public void StopbyId(string id)
        {
            if(id==task.Id)
            {
                timer.Stop();
            }
        }*/

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;

            if (currentDate >= task.TaskToDoDate)
            {
                // Wyświetl MessageBox
                SoundNotification.PlayNotificationSound();

                MessageBox.Show("Zadanie do wykonania! " + task.Name + " " + task.Description +" "+ task.TaskToDoDate);

                ((DispatcherTimer)sender).Stop();
            }
        }
    }
}
