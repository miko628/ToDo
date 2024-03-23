using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ToDoApp.Utility
{
    internal class TimerManager
    {
        private List<TimerTask> _timers = new List<TimerTask>();

        public TimerManager()
        {
            
        }

        public void AddTimer(ToDoTask task)
        {
            _timers.Add(new TimerTask(task));
        }
        public void StopAllTimers()
        {
            foreach (var timer in _timers)
            {
                timer.Stop();
            }
        }
    }
}
