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

        string dependentColumnsOptions;
        string independentColumns;
        string dependentRows;
        string independentRows;


        //Expects a string separated by commas
        public string DependentColumnOptions
        {
            get
            {
                return dependentColumnsOptions;
            }
            set
            {
                string[] values = value.Split(',');
                dependentColumnsOptions = "";
                foreach (string s in values)
                {
                    dependentColumnsOptions += s + " ";
                }
                dependentColumnsOptions.Trim();
                OnPropertyChanged();
            }
        }



        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
