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
        string IndependentColumnsContinuous;
        string IndependentColumnsCategorical;

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

        public void HandleCheckSelectionChangedDependentColumn(CheckableItem item)
        {
            DependentColumns = GetNewStringFromItemChecked(item, DependentColumns);
        }

        public void HandleCheckSelectionChangedIndependentColumnCont(CheckableItem item)
        {
            IndependentColumnsContinuous = GetNewStringFromItemChecked(item, IndependentColumnsContinuous);

        }

        public void HandleCheckSelectionChangedIndependentColumnCat(CheckableItem item)
        {
            IndependentColumnsCategorical = GetNewStringFromItemChecked(item, IndependentColumnsCategorical);
        }

        string GetNewStringFromItemChecked(CheckableItem item, string initial)
        {
            if (item.Checked == false)
            {
                //so, it got changed to false, so we have to remove it from the list
                initial = RemoveStringFromCSVString(initial, item.Content);
            }
            if (item.Checked == true)
            {
                //add it to the list
                initial += item.Content + ",";
            }
            return initial;
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
