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

        string pointScriptFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "CalculatePoint.py";

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

        string GetCSV(ObservableCollection<TextWrapperClass> input)
        {
            //probably do commas between conditionals and categoricals and spaces between the start of conditionals and categoricals
            string result = "";
            for (int i = 0; i < input.Count; i++)
            {
                if (i != input.Count - 1)
                {
                    result += input[i].Text + ",";
                } else
                {
                    result += input[i].Text;
                }
            }
            return result;
        }

        private void PredictPointButton_Click(object sender, RoutedEventArgs e)
        {
            string csvCont = GetCSV(Continuous);
            string csvCat = GetCSV(Categoricals);
            csvCat.Trim();
            csvCont.Trim();
            MainWindow.RunFileAndReturnOutput(pointScriptFilePath, csvCat + " " + csvCont, PythonPath);
            //shouldn't need to capture output
        }

    }
}
