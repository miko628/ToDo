using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private readonly HistoryModel historyModel;
        public ObservableCollection<ToDoTask> HistoryTasks { get; set; }
        private ToDoTask _selectedTask;
        public ToDoTask? SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                //ChangeViewRequest?.Invoke(this, EventArgs.Empty);
                if (value is not null)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                    OnViewChangeViewRequested();
                }


            }
        }
        public RelayCommand Unchecked { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public event EventHandler ChangeViewRequest;

        public HistoryViewModel()
        {
            historyModel = new HistoryModel();
            HistoryTasks = new ObservableCollection<ToDoTask>();
            LoadTasks(this);
            Unchecked = new RelayCommand(ExecuteUndoneTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);

        }

        private void OnViewChangeViewRequested()
        {

            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
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
            LoadTasks(this);
        }

    }
}
