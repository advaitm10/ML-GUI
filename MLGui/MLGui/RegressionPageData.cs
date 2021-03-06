﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MLGui
{
    public class RegressionPageData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action PredictPointEvent = delegate { };
        public event Action PlotPredictionEvent = delegate { };

        string accuracy;
        string trainingLoss;
        string prediction;

        ICommand predictPointCommand;
        ICommand plotPredictionCommand;

        public RegressionPageData()
        {
            prediction = "n/a";
        }

        public string Prediction
        {
            get
            {
                return prediction;
            }
            set
            {
                prediction = value;
                OnPropertyChanged();
            }
        }

        public string Accuracy
        {
            get
            {
                return accuracy;
            }
            set
            {
                accuracy = value;
                OnPropertyChanged();
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

        public string Point
        {
            get;
            set;
        }

        public ICommand PlotPrediction
        {
            get
            {
                if (plotPredictionCommand == null)
                {
                    plotPredictionCommand = new RelayCommand(param => PlotPredictionEvent());
                }
                return plotPredictionCommand;
            }
        }

        public string TrainingLoss
        {
            get
            {
                return trainingLoss;
            }
            set
            {
                trainingLoss = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}