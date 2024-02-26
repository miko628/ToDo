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
using ToDoApp.ViewModel;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class MainViewModel: ViewModelBase
    {
        private object _currentView;
        private string _currentTime;

        public RelayCommand PlaySound { get; set; }
      
        public string CurrentTime 
        {
            get { return _currentTime; }
            set {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));

                    }
        }

        public RelayCommand HistoryCommand { get; set; }
        public RelayCommand TasksCommand { get; set; }
        public RelayCommand CalendarCommand { get; set; }

        private void GoToHistory(object obj) => CurrentView = new HistoryViewModel();
        private void GoToTasks(object obj) => CurrentView = new TaskViewModel();
        private void GoToCalendar(object obj) => CurrentView = new CalendarViewModel();

        public object CurrentView 
        {
            get {  return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {
            CurrentView = new TaskViewModel();

            PlaySound = new RelayCommand(ExecutePlaySound, CanExecuteMyCommand);
            HistoryCommand = new RelayCommand(GoToHistory, CanExecuteMyCommand);
            TasksCommand = new RelayCommand(GoToTasks, CanExecuteMyCommand);
            CalendarCommand = new RelayCommand(GoToCalendar, CanExecuteMyCommand);
            ToDoApp.Utility.Timer timer = new ToDoApp.Utility.Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString(); // Aktualizujemy bieżący czas za każdym razem, gdy zdarzenie Tick zostanie wywołane
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
            //new_task = dbCon.GetAllTasks();
            foreach (var task in new_task)
            {
               // CurrentTasks.Add(task);
            }
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
