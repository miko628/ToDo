using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.ViewModel
{
    internal class HistoryViewModel: ViewModelBase
    {
        public ObservableCollection<ToDoTask> HistoryTasks { get; set; }
        public ToDoTask SelectedTask { get; set; }

        private void LoadTasks(object parameter)
        {
            HistoryTasks.Clear();
            List<ToDoTask> tasks = DbCrud.getHistoryTasks();

            foreach (ToDoTask t in tasks)
            {
                HistoryTasks.Add(t);
            }

        }

        public HistoryViewModel()
        {
            HistoryTasks = new ObservableCollection<ToDoTask>();
            LoadTasks(new object());
        }
    }
}
