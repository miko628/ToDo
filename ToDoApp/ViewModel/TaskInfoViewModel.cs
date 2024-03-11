using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Model;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class TaskInfoViewModel: ViewModelBase
    {
        private TaskInfoModel taskInfoModel;
        public event EventHandler ChangeViewRequest;
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public string DateField { get; set; }
        public string DoneField { get; set; }
        public bool Disabled { get; set; }
        private ToDoTask _task;
       
        public RelayCommand BackCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        private void OnViewChangeViewRequested()
        {
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }

        public TaskInfoViewModel(ToDoTask task)
        {
            taskInfoModel = new TaskInfoModel();
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
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); },CanExecuteMyCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            EditCommand = new RelayCommand(ExecuteEditTask, CanExecuteMyCommand);
        }
        private void ExecuteEditTask(object parameter)
        {
            if (Disabled == true)
            {
                Disabled = false;
            }
            else Disabled = true;
            
            // TO DO EDITING
            OnPropertyChanged(nameof(Disabled));
        }

        private void ExecuteDeleteTask(object parameter)
        {
            //var task = (ToDoTask) parameter;
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (taskInfoModel.DeleteTask(_task))
                {
                    OnViewChangeViewRequested();
                }
                else MessageBox.Show("Wystapil blad przy usuwaniu zadania!");
                
               
            }
        }
    }
}
