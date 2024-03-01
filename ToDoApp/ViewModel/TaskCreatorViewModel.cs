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
        public DateTime TimePick { get; set; }
        public DateTime TodayDate { get; set; }
        public string NameField { get; set; }
        public string DescriptionField { get; set; }


        public DateTime DatePick { get; set; }
        public TaskCreatorViewModel()
        {
            tekst = "pocz";
            TodayDate= DateTime.Now;
            DatePick = DateTime.Now;
            TimePick=DateTime.Now;
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
            //sprawdzenie czy data nie jest nizsza + sprawdzenie null dla daty i czasu
            //sprawdzenie null name

            if (!string.IsNullOrEmpty(NameField))
            {
                DateTime dateToAdd = new DateTime(DatePick.Year, DatePick.Month, DatePick.Day, TimePick.Hour, TimePick.Minute, TimePick.Second);

                if (DatePick > TodayDate )
                {
                    SoundNotification.PlayNotificationSound();
                    Trace.WriteLine(NameField);
                    Trace.WriteLine(DescriptionField);
                    Trace.WriteLine(dateToAdd);
                    OnRequestClose?.Invoke(this, EventArgs.Empty);
                }else MessageBox.Show("problemy");

            } else MessageBox.Show("problemy");

            

            

        }

    }
}
