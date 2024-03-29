﻿using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using ToDoApp;

namespace ToDoApp.Utility
{
    public class TaskEvents
    {
        private static ObservableCollection<DispatcherTimer> _events;
        private static TaskEvents? _instance = null;


        public static TaskEvents Instance()
        {
            if (_instance == null)
            {
                _instance = new TaskEvents();
                _events=new ObservableCollection<DispatcherTimer>();
               // Thread thread = new Thread(new ThreadStart(_instance.Run));
                //thread.Start();
            }
            return _instance;
        }
        private static void Initialize()
        {
            TaskEvents instance = TaskEvents.Instance();
        }
        public static void TaskEventsMake()
        {
            Thread thread = new Thread(new ThreadStart(Initialize));
            thread.IsBackground = true; // Ustawienie wątku na wątek w tle
            thread.Start();
        }
        
        public void AddTask(ToDoTask task)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Okres czasu, co ile ma być sprawdzana aktualna godzina (np. co minutę)
            timer.Tick += (sender, e) => Timer_Tick(sender, e, task); // Sprawdzenie o 12:00
            timer.Start();
            _events.Add(timer);
        }

        public void RemoveAllTasks()
        {
            _events.Clear();
        }

        
        public void StartTask(ToDoTask t)
        {
            ToDoTask task = t;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10); // Okres czasu, co ile ma być sprawdzana aktualna godzina (np. co minutę)
            timer.Tick += (sender, e) => Timer_Tick(sender, e, task); // Sprawdzenie o 12:00
           // Timer_Tick(timer, EventArgs.Empty,task); // Wywołaj metodę Timer_Tick natychmiast po inicjalizacji timera
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e, ToDoTask task)
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
