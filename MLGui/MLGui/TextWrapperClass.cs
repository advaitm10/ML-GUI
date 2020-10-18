using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGui
{
    public class TextWrapperClass
    {
        public string Text
        {
            get;
            set;
        }
        public string Column
        {
            get;
            set;
        }
        public TextWrapperClass(string text, string column)
        {
            Text = text;
            Column = column;
        }
    }
}
