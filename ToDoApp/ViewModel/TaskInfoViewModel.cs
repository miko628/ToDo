using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class TaskInfoViewModel: ViewModelBase
    {
        public event EventHandler ChangeViewRequest;
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public string DateField { get; set; }
        private void OnViewChangeViewRequested()
        {
            Trace.WriteLine("zmiana");
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }
        public RelayCommand BackCommand { get; set; }
        public TaskInfoViewModel(ToDoTask task)
        {
            NameField = task.Name;
            DescriptionField = task.Description;
            DateField = task.TaskToDoDate.ToString();
            Trace.WriteLine(task.Name+" "+task.Description);
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); },CanExecuteMyCommand);
        }
    }
}
