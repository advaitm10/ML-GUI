using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MLGui
{
    public partial class PointSelector : Window
    {
        ObservableCollection<TextWrapperClass> Categoricals = new ObservableCollection<TextWrapperClass>();
        ObservableCollection<TextWrapperClass> Continuous = new ObservableCollection<TextWrapperClass>();
        string PythonPath;

        public PointSelector(int numOfCats, int numOfCont, string pythonPath) //what does this take in? It just needs the number of each I believe
        {
            InitializeComponent();

            PythonPath = pythonPath;

            for (int i = 0; i < numOfCats; i++)
            {
                Categoricals.Add(new TextWrapperClass(""));
            }

            for (int i = 0; i < numOfCont; i++)
            {
                Continuous.Add(new TextWrapperClass(""));
            }

            CategoricalSelection.ItemsSource = Categoricals;
            ContinuousSelection.ItemsSource = Continuous;
        }

        private void PredictPointButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //actually we can just run the python script here, as long as we make that function static. 

    }
}
