using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoApp.ViewModel;

namespace ToDoApp.View
{
    /// <summary>
    /// Interaction logic for TaskCreatorView.xaml
    /// </summary>
    public partial class TaskCreatorView : MetroWindow
    {
        public event EventHandler OnRequestClose;
        public TaskCreatorView()
        {
            InitializeComponent();
            DataContext = new TaskCreatorViewModel();
            (DataContext as TaskCreatorViewModel).OnRequestClose += (s, e) => Close();
        }

        
    }
}
