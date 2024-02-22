using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoApp
{
    internal class MainViewModel
    {
        public Task SelectedTask { get; set; }
        public RelayCommand PlaySound { get; set; }
        public RelayCommand AddTask { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public ObservableCollection<Task> CurrentTasks { get; set; }
        public MainViewModel() 
        {
            CurrentTasks = new ObservableCollection<Task>();
            PlaySound=new RelayCommand(ExecutePlaySound,CanExecuteMyCommand);
            AddTask = new RelayCommand(ExecuteAddTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask,CanExecuteMyCommand);
        }
        private bool ExecuteCommand(object parameter)
        {
            if (Application.Current.MainWindow != null)
            {
                return true;//  Application.Current.MainWindow.Activate();
            }
            else return false;
            //_canExecute = !_canExecute;
            // return _canExecute;
        }
        private void ExecuteAddTask(object parameter)
        {
            var dbCon = DbConnection.Instance();
            List<Task> new_task = new List<Task>();
            new_task = dbCon.GetAllTasks();
            foreach (var task in new_task) 
            {
                CurrentTasks.Add(task);
            }
            

        }
        private void ExecuteDeleteTask(object parameter)
        {
            CurrentTasks.Remove(SelectedTask);
        }
        private void ExecutePlaySound(object parameter)
        {
            SoundNotification.PlayNotificationSound();
        }
        private bool CanExecuteMyCommand(object parameter)
        {
            return true;
        }
    }
}
