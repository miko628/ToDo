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
using Org.BouncyCastle.Tls;
using ControlzEx.Theming;

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
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public RelayCommand HistoryCommand { get; set; }
        public RelayCommand TasksCommand { get; set; }
        public RelayCommand CalendarCommand { get; set; }
        public RelayCommand LightThemeCommand { get; set; }
        public RelayCommand DarkThemeCommand { get; set; }

        public MainViewModel()
        {
            CurrentView = new TaskViewModel();
            GoToTasks(this);

            PlaySound = new RelayCommand(ExecutePlaySound, CanExecuteMyCommand);
            HistoryCommand = new RelayCommand(GoToHistory, CanExecuteMyCommand);
            TasksCommand = new RelayCommand(GoToTasks, CanExecuteMyCommand);
            CalendarCommand = new RelayCommand(GoToCalendar, CanExecuteMyCommand);
            LightThemeCommand = new RelayCommand((object parameters) => {
            ThemeManager.Current.ChangeTheme(App.Current, "Light.Blue", false);
                Trace.WriteLine("should be light");

                // should be changed
            }, CanExecuteMyCommand);
            DarkThemeCommand = new RelayCommand((object parameters) => {
                ThemeManager.Current.ChangeTheme(App.Current, "Dark.Blue", false);

                Trace.WriteLine("should be dark");
                // should be changed

            }, CanExecuteMyCommand);
            ToDoApp.Utility.Timer timer = new ToDoApp.Utility.Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
            //ThemeManager.Current.ChangeTheme(this, "Dark.Green");
           ThemeManager.Current.ChangeTheme(App.Current, "Light.Blue", false);
            //Application.Current.Resources.MergedDictionaries[0].Source =
            //new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.Blue.xaml");
            // should be changed

        }


        private void GoToHistory(object obj) => CurrentView = new HistoryViewModel();


        //private void GoToInfo(object obj) =>CurrentView=new TaskInfoViewModel();
        private void GoToTasks(object obj) 
        {
            var taskViewModel = new TaskViewModel();
            taskViewModel.ChangeViewRequest += ChangeViewEvent;
            CurrentView = taskViewModel;
            Trace.WriteLine(CurrentView.GetType());
                }
        private void GoToCalendar(object obj) => CurrentView = new CalendarViewModel();

        private void ChangeViewEvent(object sender,EventArgs e)
        {
            if (CurrentView.GetType() == typeof(TaskInfoViewModel))
            {
                var taskViewModel = new TaskViewModel();
                taskViewModel.ChangeViewRequest += ChangeViewEvent;
                CurrentView = taskViewModel;
            }
            else if (CurrentView.GetType()==typeof(TaskViewModel))
            {
                TaskViewModel taskViewModel = CurrentView as TaskViewModel;
                var taskInfoViewModel = new TaskInfoViewModel(taskViewModel.SelectedTask);
                taskInfoViewModel.ChangeViewRequest += ChangeViewEvent;
                CurrentView = taskInfoViewModel;
                Trace.WriteLine(CurrentView.GetType());
            }

        }


        

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString(); 
        }


    /*    private bool ExecuteCommand(object parameter)
        {
            if (Application.Current.MainWindow != null)
            {
                return true;//  Application.Current.MainWindow.Activate();
            }
            else return false;
            //_canExecute = !_canExecute;
            // return _canExecute;
        }*/
       
       
        private void ExecutePlaySound(object parameter)
        {
            SoundNotification.PlayNotificationSound();
        }
     
       /* public async Task updateTimer()
        {

        }*/
    }
}
