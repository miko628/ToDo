using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using ToDoApp.Model;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class TaskCreatorViewModel : ViewModelBase
    {
        private TaskCreatorModel creatorModel;
        public string tekst { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public event EventHandler OnRequestClose;
        public DateTime TimePick { get; set; }
        public DateTime TodayDate { get; set; }
        public string NameField { get; set; }
        public string DescriptionField { get; set; }
        public event EventHandler<TaskEventArgs> AddTimerTaskRequest;
       // public event EventHandler<StringEventArgs> RemoveTimerRequest;

        public DateTime DatePick { get; set; }

         private void OnAddTimerTaskRequest(ToDoTask task)
        {
            AddTimerTaskRequest?.Invoke(this, new TaskEventArgs(task));
        }
        public TaskCreatorViewModel()
        {

            
            //tekst = "pocz";
            creatorModel =new TaskCreatorModel();
            TodayDate= DateTime.Now;
            DatePick = DateTime.Now;
            TimePick= DateTime.Now;
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
            DateTime toDoDate = new DateTime(DatePick.Year, DatePick.Month, DatePick.Day, TimePick.Hour, TimePick.Minute, TimePick.Second);
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(NameField) )
            {
               // Trace.WriteLine(toDoDate.ToString()+ "\n" + now.ToString());
                if(toDoDate >= now)
                {
                    var task = new ToDoTask(NameField, toDoDate.ToString(), DateTime.Now.ToString(), DescriptionField, "", "");
                    if (creatorModel.SaveTask(task))
                    {
                        OnRequestClose?.Invoke(this, EventArgs.Empty);
                        OnAddTimerTaskRequest(task);
                    }
                    else MessageBox.Show("Błąd przy próbie stworzenia zadania!");
                }
                else MessageBox.Show("Data powinna być w przyszłości!");


            }
            else MessageBox.Show("Błąd przy próbie stworzenia zadania!");
           
            

            

        }

    }
}
