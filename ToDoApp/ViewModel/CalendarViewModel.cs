using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Utility;

namespace ToDoApp.ViewModel
{
    class CalendarViewModel: ViewModelBase
    {
        public CalendarViewModel() 
        {
            GoogleApiConnection api = new GoogleApiConnection();
        }
    }
}
