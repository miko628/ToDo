using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Model;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    class HistoryInfoViewModel: ViewModelBase
    {
        private HistoryInfoModel historyInfoModel;

        public event EventHandler ChangeViewRequest;
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public bool DoneField { get; set; }
        public bool Disabled { get; set; }
        private ToDoTask _task;
        public DateTime DatePick { get; set; }

        public RelayCommand BackCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private void OnViewChangeViewRequested()
        {
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }

        public HistoryInfoViewModel(ToDoTask task)
        {
            historyInfoModel = new HistoryInfoModel();
            _task = task;
            NameField = task.Name;
            DescriptionField = task.Description;
            DatePick = task.TaskToDoDate;
            DoneField = task.Done;
            Disabled = true;
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); }, CanExecuteMyCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            //EditCommand = new RelayCommand(ExecuteEditTask, CanExecuteMyCommand);
           // CancelCommand = new RelayCommand(ExecuteCancel, CanExecuteMyCommand);
           // SaveCommand = new RelayCommand(ExecuteSave, CanExecuteMyCommand);
        }

        private void ExecuteDeleteTask(object parameter)
        {
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (historyInfoModel.DeleteTask(_task))
                {
                   
                    OnViewChangeViewRequested();
                }
                else MessageBox.Show("Wystapil blad przy usuwaniu zadania!");


            }
        }
    }
}
