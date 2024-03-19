using Google.Apis.Calendar.v3.Data;
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
    class GoogleTaskInfoViewModel:ViewModelBase
    {
        private GoogleTaskInfoModel googleTaskInfoModel;
        private Event _task;

        public event EventHandler ChangeViewRequest;
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public DateTime DateField { get; set; }
       // public string DoneField { get; set; }
        public bool Disabled { get; set; }
        public DateTime TodayDate { get; set; }

        public RelayCommand BackCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public GoogleTaskInfoViewModel(Event task)
        {
            googleTaskInfoModel = new GoogleTaskInfoModel();
            Disabled = true;
            _task = task;
            NameField = task.Summary;
            DescriptionField = task.Description;
            DateField = (DateTime) task.Start.DateTime;
         //   DoneField = task;
            TodayDate = DateTime.Now;
            BackCommand = new RelayCommand((e) => { OnViewChangeViewRequested(); }, CanExecuteMyCommand);
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
           // Trace.WriteLine(DoneField.ToString());
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(NameField) && DateField >= now)
            {
                googleTaskInfoModel.UpdateEvent(_task, NameField, DescriptionField, DateField);
            }
            else MessageBox.Show("Wystąpił błąd przy zapisywaniu!");
            ReloadTask();
        }
        private void DefaultValues(Event task)
        {
            NameField = task.Summary;
            DescriptionField = task.Description;
            DateField = (DateTime)task.Start.DateTime;
            OnPropertyChanged(nameof(NameField));
            OnPropertyChanged(nameof(DescriptionField));

            OnPropertyChanged(nameof(DateField));

           // OnPropertyChanged(nameof(DoneField));

        }

        private void ReloadTask()
        {
            var gettask = googleTaskInfoModel.GetEvent(_task.Id);
            if (gettask is not null)
            {
                _task = gettask;
                NameField = gettask.Summary;
                DescriptionField = gettask.Description;
                DateField = (DateTime)gettask.Start.DateTime;
               // DoneField = _task.Done;
            }

            CancelCommand.Execute(this);
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
                /*if (taskInfoModel.DeleteTask(_task))
                {
                    OnViewChangeViewRequested();
                }
               
                else MessageBox.Show("Wystapil blad przy usuwaniu zadania!");*/
                if (googleTaskInfoModel.DeleteEvent(_task))
                {
                    OnViewChangeViewRequested();
                    MessageBox.Show("Pomyślnie usunięto zadanie z kalendarza Google.");
                }
                else MessageBox.Show("Wystąpił błąd przy usuwaniu zadania z kalendarza Google.");

            }
        }
        private void OnViewChangeViewRequested()
        {
            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
