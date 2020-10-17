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

        string dependentColumn;
        string independentColumn;
        string dependentRow;
        string independentRow;

        public string DependentColumn
        {
            get
            {
                return dependentColumn;
            }
            set
            {
                dependentColumn = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
