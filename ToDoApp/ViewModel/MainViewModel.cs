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

namespace ToDoApp.ViewModel
{
    internal class MainViewModel: ViewModelBase
    {
        private object _currentView;

        public Task SelectedTask { get; set; }
        public RelayCommand PlaySound { get; set; }
        public RelayCommand AddTask { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public ObservableCollection<Task> CurrentTasks { get; set; }
        public string CurrentTime { get; set; }

        public RelayCommand HistoryCommand { get; set; }
        public RelayCommand TasksCommand { get; set; }
        public RelayCommand CalendarCommand { get; set; }


        public object CurrentView 
        {
            get {  return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {
            CurrentTasks = new ObservableCollection<Task>();
            PlaySound = new RelayCommand(ExecutePlaySound, CanExecuteMyCommand);
            AddTask = new RelayCommand(ExecuteAddTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);

            CurrentView = new TasksViewModel();
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
        private void showDBTasks()
        {
            var dbCon = DbConnection.Instance();
            List<Task> new_task = new List<Task>();
            new_task = dbCon.GetAllTasks();
            foreach (var task in new_task)
            {
                CurrentTasks.Add(task);
            }
        }
        private void ExecuteAddTask(object parameter)
        {

            //otworz okienko dodawania Taska
            CurrentTasks.Add(new Task("nowe", "", "", "przykladowy opis", ""));

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
       /* public async Task updateTimer()
        {

        }*/
    }
}
