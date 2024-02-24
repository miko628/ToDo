using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    class TaskViewModel: ViewModelBase
    {
        public Task SelectedTask { get; set; }
        public RelayCommand AddTask { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public ObservableCollection<Task> CurrentTasks { get; set; }

        public TaskViewModel()
        {
            CurrentTasks = new ObservableCollection<Task>();

            AddTask = new RelayCommand(ExecuteAddTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
        }

        private void ExecuteAddTask(object parameter)
        {

            //otworz okienko dodawania Taska
            CurrentTasks.Add(new Task("nowe", "", "", "przykladowy opis", ""));

        }
        private void ExecuteDeleteTask(object parameter)
        {
            CurrentTasks.Remove(SelectedTask);
        }

        private bool CanExecuteMyCommand(object parameter)
        {
            return true;
        }
    }
}
