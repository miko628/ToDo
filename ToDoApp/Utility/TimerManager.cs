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
        private List<TimerTask> _timers;

        public TimerManager()
        {
            _timers=new List<TimerTask>();
        }

        public void AddTimer(ToDoTask task)
        {
            _timers.Add(new TimerTask(task));
        }
        public void DeleteTimer(string id)
        {
            foreach (var timer in _timers)
            {
                if (timer.GetId().Equals(id)) 
                {
                    timer.Stop();
                    _timers.Remove(timer);
                    break;
                }
            }
        }
        public void StopAllTimers()
        {
            foreach (var timer in _timers)
            {
                timer.Stop();
            }
            _timers.Clear();
        }
    }
}
