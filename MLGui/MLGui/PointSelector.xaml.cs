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
        public event Action<ObservableCollection<TextWrapperClass>> SetColumnAndValue = delegate { };
        
        ObservableCollection<TextWrapperClass> Categoricals = new ObservableCollection<TextWrapperClass>();
        ObservableCollection<TextWrapperClass> Continuous = new ObservableCollection<TextWrapperClass>();
        string PythonPath;

        string pointScriptFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "CalculatePoint.py";

        public PointSelector(string categoricalCategories, string continuousCategories, string pythonPath) //what does this take in? It just needs the number of each I believe
        {
            InitializeComponent();

            PythonPath = pythonPath;

            //so, we're going to need columns in order.

            string[] Conts = continuousCategories.Split(',');
            string[] Cats = categoricalCategories.Split(',');

            for (int i = 0; i < Cats.Length; i++)
            {
                Categoricals.Add(new TextWrapperClass("", Cats[i]));
            }

            for (int i = 0; i < Conts.Length; i++)
            {
                Continuous.Add(new TextWrapperClass("", Conts[i]));
            }

            CategoricalSelection.ItemsSource = Categoricals;
            ContinuousSelection.ItemsSource = Continuous;
        }

        private void PredictPointButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextWrapperClass wrapper in Continuous)
            {
                Categoricals.Add(wrapper);
            }

            SetColumnAndValue(Categoricals);
            this.Close();
        }
    }
}