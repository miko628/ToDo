using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class HistoryViewModel: ViewModelBase
    {
        public ObservableCollection<ToDoTask> HistoryTasks { get; set; }
        public ToDoTask SelectedTask { get; set; }
        public RelayCommand Unchecked { get; set; }
        public RelayCommand DeleteTask { get; set; }


        public HistoryViewModel()
        {
            HistoryTasks = new ObservableCollection<ToDoTask>();
            LoadTasks(new object());
            Unchecked = new RelayCommand(ExecuteUndoneTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);

        }

        private void ExecuteUndoneTask(object parameter)
        {
            //Trace.WriteLine((ToDoTask)parameter);
            DbCrud.UndoneTask((ToDoTask)parameter);
            LoadTasks(new object());
        }

        private void LoadTasks(object parameter)
        {
            HistoryTasks.Clear();
            List<ToDoTask> tasks = DbCrud.getHistoryTasks();

            foreach (ToDoTask t in tasks)
            {
                HistoryTasks.Add(t);
            }

        }
       /* private void ExecuteDeleteTask(object parameter)
        {
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (SelectedTask is not null)
                {
                    DbCrud.DeleteTask(SelectedTask);
                    LoadTasks(new object());
                }
            }
        }*/
        private void ExecuteDeleteTask(object parameter)
        {
            var result = MessageBox.Show("Czy jesteś pewien, że chcesz usunąć to zadanie?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (parameter is not null)
                {
                    DbCrud.DeleteTask((ToDoTask)parameter);
                    LoadTasks(new object());
                }
                else MessageBox.Show("Nie poprawny parametr");
            }
        }

    }
}
