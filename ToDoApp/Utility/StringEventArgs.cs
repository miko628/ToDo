using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Utility
{
    public class StringEventArgs
    {
        private string text;

        public StringEventArgs(string text)
        {
            this.text = text;
        }
        public string Text { get { return text; } }

    }
}
