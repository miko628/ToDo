using Google.Apis.Calendar.v3.Data;
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
    class GoogleTaskInfoViewModel:ViewModelBase
    {
        private GoogleTaskInfoModel taskInfoModel;
        public event EventHandler ChangeViewRequest;
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public string DateField { get; set; }
        public string DoneField { get; set; }
        public bool Disabled { get; set; }
        private Event _task;

        public RelayCommand BackCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        public GoogleTaskInfoViewModel(Event task)
        {
            taskInfoModel = new GoogleTaskInfoModel();
            Disabled = true;
            _task = task;
            NameField = task.Summary;
            DescriptionField = task.Description;
            DateField = task.Start.DateTime.ToString();
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); }, CanExecuteMyCommand);
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
                /*if (taskInfoModel.DeleteTask(_task))
                {
                    OnViewChangeViewRequested();
                }
                else MessageBox.Show("Wystapil blad przy usuwaniu zadania!");*/


            }
        }
        private void OnViewChangeViewRequested()
        {
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
