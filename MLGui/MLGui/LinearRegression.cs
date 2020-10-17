using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MLGui
{
    class LinearRegression : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string Directory
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }

        public bool SelectModel
        {
            get;
            set;
        }

        //The downside of this is if it's a path it's not going to work
        //So, this is an optional string for the model if select model is checked. 
        public string ModelPath
        {
            get;
            set;
        }

        public RegressionModel Model
        {
            get;
            set;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}