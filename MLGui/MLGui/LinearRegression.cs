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

        string directory, data, modelPath;
        RegressionModel model;
        bool selectModel;

        public string Directory
        {
            get
            {
                return directory;
            }
            set
            {
                directory = value;
                OnPropertyChanged();
            }
        }

        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }

        public bool SelectModel
        {
            get
            {
                return selectModel;
            }
            set 
            {
                selectModel = value;
                OnPropertyChanged();
            }
        }

        //The downside of this is if it's a path it's not going to work
        //So, this is an optional string for the model if select model is checked. 
        public string ModelPath
        {
            get
            {
                return modelPath;
            }
            set
            {
                modelPath = value;
                OnPropertyChanged();
            }
        }

        public RegressionModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}