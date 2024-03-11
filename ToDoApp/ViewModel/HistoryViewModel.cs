using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApp.Model;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    internal class HistoryViewModel: ViewModelBase
    {
        private HistoryModel historyModel;
        public ObservableCollection<ToDoTask> HistoryTasks { get; set; }
        public ToDoTask SelectedTask { get; set; }
        public RelayCommand Unchecked { get; set; }
        public RelayCommand DeleteTask { get; set; }


        public HistoryViewModel()
        {
            historyModel = new HistoryModel();
            HistoryTasks = new ObservableCollection<ToDoTask>();
            LoadTasks(this);
            Unchecked = new RelayCommand(ExecuteUndoneTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);

        }

        private void ExecuteUndoneTask(object parameter)
        {
            var task = parameter as ToDoTask;
            if (historyModel.UndoTask(task))
            {
                MessageBox.Show("Pomyslnie zmieniono zadanie.");
                // instead of messagebox do popup
            }
            else MessageBox.Show("Wystapil blad przy zmianie zadania!");
            LoadTasks(this);
        }

        private void LoadTasks(object parameter)
        {
            HistoryTasks.Clear();
            foreach (ToDoTask t in historyModel.GetAllHistoryTasks())
            {
                HistoryTasks.Add(t);
            }

        }

        private void ExecuteDeleteTask(object parameter)
        {
            var task= parameter as ToDoTask;
            if(historyModel.DeleteTask(task))
            {
                MessageBox.Show("Pomyslnie usunieto zadanie.");
                // instead of messagebox use popup
            }else MessageBox.Show("Wystapil problem przy probie usuniecia zadania.");
        }

    }
}
