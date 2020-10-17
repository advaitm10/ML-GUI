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

        ICommand addColumn;

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

        public ICommand AddColumn
        {
            get
            {
                if (addColumn == null)
                {
                    addColumn = new RelayCommand( param => this.addColumnFunction());
                }
                return addColumn;
            } 
        }
        void addColumnFunction()
        {
            DependentColumns += SelectedIndColumn + " ";
            Console.WriteLine("Dependent Columns: " + DependentColumns);
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
