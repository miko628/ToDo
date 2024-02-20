using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
            if(dbCon.IsConnect())
            {
                string query = "select * from Task";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    //reader.
                    //CurrentTasks.Add(new Task("reader.GetString(0)", "reader.GetString(1)"));
                    Trace.WriteLine(reader.GetString(0) + reader.GetString(1));
                    Trace.WriteLine(reader.GetString(2)+reader.GetString(3));
                }
                dbCon.Close();
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
