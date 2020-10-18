using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public event Action CreateModelClicked = delegate { };
        public event Action PredictPointEvent;

        public string dependentColumn;
        public string IndependentColumnsContinuous;
        public string IndependentColumnsCategorical;

        ICommand createModelCommand;

        ICommand predictPointCommand;

        int cycles;

        public ObservableCollection<TextWrapperClass> ColumnAndValue
        {
            get;
            set;
        }

        public int Cycles
        {
            get
            {
                return cycles;
            }
            set
            {
                cycles = value;
                Console.WriteLine("Cycles assigned to " + cycles);
            }
        }

        public string DependentColumn
        {
            get
            {
                return dependentColumn;
            }
            set
            {
                dependentColumn = value;
                Console.WriteLine("Dependent column: " + dependentColumn);
                OnPropertyChanged();
            }
        }

        public ICommand CreateModel
        {
            get
            {
                if (createModelCommand == null)
                {
                    createModelCommand = new RelayCommand( param => createModel());
                }
                return createModelCommand;
            }
        }

        public ICommand PredictPoint
        {
            get
            {
                if (predictPointCommand == null)
                {
                    predictPointCommand = new RelayCommand(param => PredictPointEvent());
                }
                return predictPointCommand;
            }
        }

        public bool Plot
        {
            get;
            set;
        }

        void createModel()
        {
            //this function only exists so mainwindow knows when the button's been clicked
            CreateModelClicked();
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
