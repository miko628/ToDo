using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ToDoApp.Utility
{
    public class Timer
    {
        private DispatcherTimer timer;
        public event EventHandler Tick;

        public Timer()
        {
            timer= new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += Timer_Tick;


        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}
