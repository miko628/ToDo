using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ToDoApp;

namespace ToDoApp.Utility
{
    public class TaskEvents
    {
       
 
        public static void StartTask(ToDoTask t)
        {
            ToDoTask task = t;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10); // Okres czasu, co ile ma być sprawdzana aktualna godzina (np. co minutę)
            timer.Tick += (sender, e) => Timer_Tick(sender, e, task); // Sprawdzenie o 12:00
           // Timer_Tick(timer, EventArgs.Empty,task); // Wywołaj metodę Timer_Tick natychmiast po inicjalizacji timera
            timer.Start();
        }
        private static void Timer_Tick(object sender, EventArgs e, ToDoTask task)
        {
            DateTime currentDate = DateTime.Now;

            if (currentDate >= task.TaskToDoDate)
            {
                // Wyświetl MessageBox
                SoundNotification.PlayNotificationSound();

                MessageBox.Show("Zadanie do wykonania! "+task.Name+" "+task.Description+task.TaskToDoDate);

                ((DispatcherTimer)sender).Stop();
            }
        }
        private void ExecutePlaySound(object parameter)
        {
            SoundNotification.PlayNotificationSound();
        }
    }
}
