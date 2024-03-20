using Google.Apis.Calendar.v3.Data;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Model;
using ToDoApp.Utility;
using ToDoApp.View;

namespace ToDoApp.ViewModel
{
    class CalendarViewModel : ViewModelBase
    {
        private CalendarModel calendarModel;
        private Event? _selectedTask;
        public ObservableCollection<Event> CurrentTasks { get; set; }

        public RelayCommand SynchronizeCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand Checked { get; set; }

        public event EventHandler? ChangeViewRequest;

        private void OnViewChangeViewRequested()
        {
            Trace.WriteLine("Wysylam calendarviewmodel evenr");
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }

        public Event? SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                //ChangeViewRequest?.Invoke(this, EventArgs.Empty);
                if (value is not null)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                    OnViewChangeViewRequested();
                }


            }
        }

        public CalendarViewModel() 
        {
            CurrentTasks = new ObservableCollection<Event>();
            GoogleApiConnection api = new GoogleApiConnection();
            calendarModel= new CalendarModel();
            LoadTasks();
            SynchronizeCommand = new RelayCommand(ExecuteSyncTasks, CanExecuteMyCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            Checked = new RelayCommand(ExecuteDoneTask, CanExecuteMyCommand);

            //api.PostTask(new ToDoTask("nazwa", DateTime.Now.ToString(), DateTime.Now.ToString(), "","",null));
        }
        private void ExecuteDoneTask(object parameter)
        {
            // TO DO EXECUTE DONE TASK GOOGLE APi
        }
        private void ExecuteDeleteTask(object parameter)
        {
            var task = parameter as Event;
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (calendarModel.DeleteEvent(task))
                {
                    MessageBox.Show("Pomyślnie usunięto zadanie z kalendarza Google.");
                }
                else MessageBox.Show("Wystąpił błąd przy usuwaniu zadania z kalendarza Google.");
                LoadTasks();
            }
                

        }

        private void LoadTasks()
        {
            CurrentTasks.Clear();
            var events = calendarModel.GetGoogleTaks();
            if (events is not null)
            {
                foreach (var e in events.Items)
                {
                    CurrentTasks.Add(e);
                }
            }
            else MessageBox.Show("Wystąpił błąd przy próbie wczytania zadań z kalendarza Google.");
            
        }
        private void ExecuteSyncTasks ( object param)
        {
            calendarModel.SynchronizeTasks();
            LoadTasks();
        }
    }
}
