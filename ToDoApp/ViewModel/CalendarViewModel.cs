using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    class CalendarViewModel : ViewModelBase
    {
        public RelayCommand SynchronizeCommand { get; set; }
        public CalendarViewModel() 
        {
            SynchronizeCommand = new RelayCommand(ExecuteSyncTasks, CanExecuteMyCommand);
            //api.PostTask(new ToDoTask("nazwa", DateTime.Now.ToString(), DateTime.Now.ToString(), "","",null));
        }
        private void ExecuteSyncTasks ( object param)
        {
            List<ToDoTask> tasks = DbCrud.getCurrentTasks();
            GoogleApiConnection api = new GoogleApiConnection();
            api.SyncrhonizeTasks(tasks);
        }
    }
}
