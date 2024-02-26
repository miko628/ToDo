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
        public ToDoTask SelectedTask { get; set; }
        public RelayCommand AddTask { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public ObservableCollection<ToDoTask> CurrentTasks { get; set; }
        public ObservableCollection<bool> Checked { get; set; }

        public TaskViewModel()
        {
            
            CurrentTasks = new ObservableCollection<ToDoTask>();
            Checked = new ObservableCollection<bool>();
            AddTask = new RelayCommand(ExecuteAddTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            LoadTasks(new object());
        }
        
        private void LoadTasks(object parameter)
        {
            CurrentTasks.Clear();
            List<ToDoTask> tasks = DbCrud.getCurrentTasks();
            
            foreach (ToDoTask t in tasks)
            {
                CurrentTasks.Add(t);
            }
        
        }

        private void ExecuteAddTask(object parameter)
        {

            //otworz okienko dodawania Taska
            var task = new ToDoTask("nowe", "", "", "przykladowy opis", "",null);
            DbCrud.InsertTask(task);
            LoadTasks(new object());

        }
        private void ExecuteDeleteTask(object parameter)
        {
            if (SelectedTask is not null)
            {
                DbCrud.DeleteTask(SelectedTask);
                LoadTasks(new object());
            }
        }

        private bool CanExecuteMyCommand(object parameter)
        {
            return true;
        }
    }
}
