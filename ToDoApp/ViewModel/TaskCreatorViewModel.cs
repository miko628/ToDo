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
    internal class TaskCreatorViewModel : ViewModelBase
    {
        public string tekst { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public event EventHandler OnRequestClose;

        public TaskCreatorViewModel()
        {
            tekst = "pocz";
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteMyCommand);
            CancelCommand = new RelayCommand(ExecuteCancel,CanExecuteMyCommand);
        }

        private void ExecuteCancel(object parameter)
        {

            //SoundNotification.PlayNotificationSound();
            OnRequestClose?.Invoke(this, EventArgs.Empty);

        }
        private void ExecuteSave(object parameter)
        {
            SoundNotification.PlayNotificationSound();

            tekst = "dodaj";
            Trace.WriteLine("dodaj");
        }

    }
}
