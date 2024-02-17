using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoApp
{
    internal class MainViewModel
    {
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
            CurrentTasks.Add(new Task("zadanie","opis fajny"));
        }
        private void ExecuteDeleteTask(object parameter)
        {
            SoundNotification.PlayNotificationSound();
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
