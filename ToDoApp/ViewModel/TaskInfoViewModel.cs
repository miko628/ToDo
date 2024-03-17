using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        //public string DateField { get; set; }
        public string DoneField { get; set; }
        public bool Disabled { get; set; }
        private ToDoTask _task;
        public DateTime DatePick { get; set; }
        public DateTime TodayDate { get; set; }

        public RelayCommand BackCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        private void OnViewChangeViewRequested()
        {
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }
        public void DefaultValues(ToDoTask task)
        {
            NameField = task.Name;
            DescriptionField = task.Description;
            TodayDate = DateTime.Now;
            DatePick = task.TaskToDoDate;
            if (task.Done == true)
            {
                DoneField = "Tak";
            }
            else DoneField = "Nie";
            OnPropertyChanged(nameof(NameField));
            OnPropertyChanged(nameof(DescriptionField));

            OnPropertyChanged(nameof(DatePick));
            
            OnPropertyChanged(nameof(DoneField));

        }
        public TaskInfoViewModel(ToDoTask task)
        {
            taskInfoModel = new TaskInfoModel();
            Disabled = true;
            _task = task;
            DefaultValues(task);
            //DateField = task.TaskToDoDate.ToString();
            
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); },CanExecuteMyCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            EditCommand = new RelayCommand(ExecuteEditTask, CanExecuteMyCommand);
            CancelCommand = new RelayCommand(ExecuteCancel, CanExecuteMyCommand);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteMyCommand);

        }
        private void ExecuteEditTask(object parameter)
        {
            Disabled = false;
            OnPropertyChanged(nameof(Disabled));
        }
        private void ExecuteSave(object parameter)
        {
            // TO DO SAVE CHANGES TO DATABASE
        }
        private void ExecuteCancel(object parameter)
        {
            // TO DO BRING BACK PREVIOUS VALUES 
            Disabled = true;
            DefaultValues(_task);
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
