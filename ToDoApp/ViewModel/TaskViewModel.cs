using Mysqlx.Connection;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToDoApp.Model;
using ToDoApp.Utility;
using ToDoApp.View;


namespace ToDoApp.ViewModel
{
    class TaskViewModel: ViewModelBase
    {
        public ObservableCollection<ToDoTask> Tasks { get; set; } 
        private TaskCreatorView? taskCreator;
        private ToDoTask _selectedTask;
        private readonly TaskModel taskModel;
        public event EventHandler ChangeViewRequest;
        public event EventHandler<TaskEventArgs> AddTimerTaskRequest;
        public event EventHandler<StringEventArgs> RemoveTimerRequest;
        public event EventHandler RemoveAllTimersRequest;


        private void OnRemoveAllTimersRequest()
        {
            RemoveAllTimersRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OnRemoveTimerRequest(string id)
        {
            RemoveTimerRequest?.Invoke(this, new StringEventArgs(id));
        }

        private void OnAddTimerTaskRequest(ToDoTask task)
        {
            AddTimerTaskRequest?.Invoke(this, new TaskEventArgs(task));
        }
        private void OnViewChangeViewRequested()
        {

            ChangeViewRequest?.Invoke(this, EventArgs.Empty);
        }

        public ToDoTask? SelectedTask 
        { 
            get { return _selectedTask;  }
            set {
                //ChangeViewRequest?.Invoke(this, EventArgs.Empty);
                if(value is not null)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                    Trace.WriteLine(_selectedTask.ShouldBeDone);
                    OnViewChangeViewRequested();
                }
                

            }
        }
        
        public RelayCommand AddTask { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public ObservableCollection<ToDoTask> CurrentTasks { get; set; }
        public RelayCommand Checked { get; set; }
        //public RelayCommand MoreCommand { get; set; } (Deleted)
        public TaskViewModel()
        {
            taskCreator = new TaskCreatorView();
            taskModel = new TaskModel();   
            CurrentTasks = new ObservableCollection<ToDoTask>();
            Tasks = new ObservableCollection<ToDoTask>();
            AddTask = new RelayCommand(ExecuteAddTask, CanExecuteMyCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTask, CanExecuteMyCommand);
            Checked = new RelayCommand(ExecuteDoneTask, CanExecuteMyCommand);
            //MoreCommand = new RelayCommand((args) => { ChangeViewRequest?.Invoke(this, EventArgs.Empty); }, CanExecuteMyCommand); (Deleted)
            RemoveAllTimersRequest?.Invoke(this, EventArgs.Empty);

           // OnRemoveAllTimersRequest();
            LoadTasks();
           // ReloadEvents();


        }
        public void Initialize()
        {
            ReloadEvents();
        }
        private void ReloadEvents()
        {
            
            OnRemoveAllTimersRequest();
            foreach (ToDoTask task in CurrentTasks)
            {
                Trace.WriteLine("powinno sie dodac chollera");
                OnAddTimerTaskRequest(task);
               // var timerTask = new TimerTask(task);
            }
        }
        private async void LoadTasks()
        {
            //TaskEvents tasksEvents = TaskEvents.Instance();
            
            CurrentTasks.Clear();
            //tasksEvents.RemoveAllTasks();
            foreach (ToDoTask t in taskModel.GetAllTasks())
            {
                CurrentTasks.Add(t);
                //var timer = new TimerTask(t);
                //tasksEvents.AddTask(t);
            }
        
        }

        private void ExecuteDoneTask(object parameter) 
        {
            //Trace.WriteLine((ToDoTask)parameter);
            var task = parameter as ToDoTask;
            taskModel.DoneTask(task);
            OnRemoveTimerRequest(task.Id);
// add try catch (validation)
            LoadTasks();
        }
    
        private void ExecuteAddTask(object parameter)
        {
           // OnAddTimerTaskRequest(new ToDoTask("cosik","","","","",""));
            //otworz okienko dodawania Taska
            /*var task = new ToDoTask("nowe", "", "", "przykladowy opis", "",null);
            DbCrud.InsertTask(task);
            
            
            TaskEvents.StartTask(task);*/

            if (taskCreator is null || !taskCreator.IsVisible)
            {
                taskCreator = new TaskCreatorView();              
                taskCreator.Closed += TaskCreator_Closed;
                var DataContext = new TaskCreatorViewModel();
                
                DataContext.AddTimerTaskRequest += OnAddTimerTaskRequest;
                DataContext.OnRequestClose += (s, e) => taskCreator.Close();

                taskCreator.DataContext= DataContext;

                taskCreator.ShowDialog();

            }
            else taskCreator.Activate();
            //TaskCreatorView taskCreator = new();
            //LoadTasks(new object());

        }

        private void OnAddTimerTaskRequest(object sender, TaskEventArgs task)
        {
            AddTimerTaskRequest?.Invoke(this, task);

        }

        private void TaskCreator_Closed(object sender, EventArgs e)
        {
            // Usuń referencję do zamkniętego okna
            taskCreator = null;
            LoadTasks();

            //ReloadEvents();


        }
        private void ExecuteDeleteTask(object parameter)
        {
            var task = parameter as ToDoTask;
            if (taskModel.DeleteTask(task))
            {
                OnRemoveTimerRequest(task.Id);
                LoadTasks();

            }
            else MessageBox.Show("Nie udalo sie usunac zadania!");
        }

       
    }
}
