using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MLGui
{
    class RegressionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string dependentColumns;
        string independentColumns;
        string dependentRows;
        string independentRows;

        public string SelectedIndColumn
        {
            get;
            set;
        }

        //Expects a string separated by commas
        public string DependentColumns
        {
            get
            {
                return dependentColumns;
            }
            set
            {
                dependentColumns = value;
                OnPropertyChanged();
            }
        }

        public void HandleCheckSelectionChanged(CheckableItem item)
        {
            if (item.Checked == false)
            {
                //so, it got changed to false, so we have to remove it from the list
                DependentColumns = RemoveStringFromCSVString(DependentColumns, item.Content);
            }
            if (item.Checked == true) {
                //add it to the list. Much easier.
                DependentColumns += item.Content + ",";
            }
            Console.WriteLine("DependentColumns: " + DependentColumns);
        }
        string RemoveStringFromCSVString(string csv, string remove)
        {
            csv = csv.Substring(0, csv.Length - 1);

            string[] values = csv.Split(',');
            string result = "";

            for (int i = 0; i < values.Length; i++)
            {
                if (!remove.Equals(values[i]))
                {
                    result += values[i] + ",";
                }
            }
            return result;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
