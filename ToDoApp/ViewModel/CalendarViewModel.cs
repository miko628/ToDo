using Google.Apis.Calendar.v3.Data;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    class CalendarViewModel : ViewModelBase
    {
        private CalendarModel calendarModel;
        public ObservableCollection<Event> CurrentTasks { get; set; }

        public RelayCommand SynchronizeCommand { get; set; }
        public CalendarViewModel() 
        {
            CurrentTasks = new ObservableCollection<Event>();
            GoogleApiConnection api = new GoogleApiConnection();
            calendarModel= new CalendarModel();
            LoadTasks();
            SynchronizeCommand = new RelayCommand(ExecuteSyncTasks, CanExecuteMyCommand);
            //api.PostTask(new ToDoTask("nazwa", DateTime.Now.ToString(), DateTime.Now.ToString(), "","",null));
        }
        private void LoadTasks()
        {
            CurrentTasks.Clear();
            var events = calendarModel.GetGoogleTaks();
            foreach (var e in events.Items)
            {
                CurrentTasks.Add(e);
            }
        }
        private void ExecuteSyncTasks ( object param)
        {
            calendarModel.SynchronizeTasks();
            LoadTasks();
        }
    }
}
