using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MLGui
{
    class RegressionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string dependentColumns;
        string independentColumns;
        string dependentRows;
        string independentRows;

        public string DependentColumn
        {
            get
            {
                return dependentColumns;
            }
            set
            {
                string[] values = value.Split(',');
                dependentColumns = "";
                foreach (string s in values)
                {
                    dependentColumns += s + " ";
                }
                dependentColumns.Trim();
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
