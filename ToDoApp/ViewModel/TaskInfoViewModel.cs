using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class TaskInfoViewModel: ViewModelBase
    {
        public event EventHandler ChangeViewRequest;
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public string DateField { get; set; }
        public string DoneField { get; set; }
        public bool Disabled { get; set; }
        private ToDoTask _task;
        private void OnViewChangeViewRequested()
        {
            Trace.WriteLine("zmiana");
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        public TaskInfoViewModel(ToDoTask task)
        {
            Disabled = true;
            _task = task;
            NameField = task.Name;
            DescriptionField = task.Description;
            DateField = task.TaskToDoDate.ToString();
            if (task.Done == true)
            {
                DoneField = "Tak";
            }
            else DoneField = "Nie";
            Trace.WriteLine(task.Name+" "+task.Description);
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); },CanExecuteMyCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            EditCommand = new RelayCommand(ExecuteEditTask, CanExecuteMyCommand);
        }
        private void ExecuteEditTask(object parameter)
        {
            Disabled = false;
            OnPropertyChanged(nameof(Disabled));
        }

        private void ExecuteDeleteTask(object parameter)
        {
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (_task is not null)
                {
                    DbCrud.DeleteTask(_task);
                }
                OnViewChangeViewRequested();
            }
        }
    }
}
