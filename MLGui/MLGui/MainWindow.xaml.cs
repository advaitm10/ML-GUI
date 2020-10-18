using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MLGui
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string pythonPath;

        RegressionPageData regressionData;

        string pythonPathFileName = System.AppDomain.CurrentDomain.BaseDirectory + "pythonPath.txt";

        RegressionModel model;

        LinearRegression regression;
        public MainWindow()
        {
            InitializeComponent();
            regression = new LinearRegression();
            regression = new LinearRegression();
            RegressionTab.DataContext = regression;
            if (File.Exists(pythonPathFileName))
            {
                using (StreamReader reader = new StreamReader(pythonPathFileName))
                {
                    pythonPath = reader.ReadToEnd();
                } 
            } else
            {
                PromptPythonInstall();
            }
        }
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void DirectorySelectorButton_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {

                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    regression.Directory = folderBrowserDialog.SelectedPath;    
                }
            }
        }
        private void SelectDataButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV File (*.csv)|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                regression.Data = fileDialog.FileName;
            }
        }

        private void SelectModelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV File (*.csv)|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                regression.ModelPath = fileDialog.FileName;
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (!regression.SelectModel)
            {
                model = new RegressionModel();

                ModelCreationPopup.Visibility = Visibility.Visible;
                ModelCreationPopup.DataContext = model;

                string dropDownOptions = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "get-column-options.py", regression.Data);

                string[] columns = new string[1];

                //reads from the python script
                using (var reader = new StringReader(dropDownOptions))
                {
                    columns = reader.ReadLine().Split(',');
                }

                //could make this a function to reduce duplication
                ObservableCollection<CheckableItem> checkableItemsIndependentCat = new ObservableCollection<CheckableItem>();
                foreach (string s in columns)
                {
                    CheckableItem item = new CheckableItem(s, false);
                    checkableItemsIndependentCat.Add(item);
                    item.CheckedChanged += model.HandleCheckSelectionChangedIndependentColumnCat;
                }
                
                ObservableCollection<CheckableItem> checkableItemsIndependentCont = new ObservableCollection<CheckableItem>();
                foreach (string s in columns)
                {
                    CheckableItem item = new CheckableItem(s, false);
                    checkableItemsIndependentCont.Add(item);
                    item.CheckedChanged += model.HandleCheckSelectionChangedIndependentColumnCont;
                }

                model.CreateModelClicked += OnModelCreate;

                ModelCreationPopup.IndependentCategoricalColumnSelector.ItemsSource = checkableItemsIndependentCat;
                ModelCreationPopup.DependentColumnSelector.ItemsSource = columns;
                ModelCreationPopup.IndependentColumnContinuousSelector.ItemsSource = checkableItemsIndependentCont;

            }
        }
        
        void OnModelCreate()
        {
            Console.WriteLine("clicked model create button");
            
            string categorical = model.IndependentColumnsCategorical.Substring(0, model.IndependentColumnsCategorical.Length - 1);
            string continuous = model.IndependentColumnsContinuous.Substring(0, model.IndependentColumnsContinuous.Length - 1);

            Console.WriteLine(String.Format("{0} {1} {2} {3} {4}",
                regression.Data, model.DependentColumn, categorical, continuous, model.Cycles));

            string result = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "regression.py", String.Format("{0} {1} {2} {3} {4}", 
                regression.Data, model.DependentColumn, categorical, continuous, model.Cycles));

            Console.WriteLine("result before split: " + result);

            result = GetLastResultLine(result);

            ModelCreationPopup.Visibility = Visibility.Collapsed;

            SetUpRegressionPage(result);
            
        }

        void SetUpRegressionPage(string result)
        {
            RegressionPagePopup.Visibility = Visibility.Visible;

            regressionData = new RegressionPageData();
            RegressionPagePopup.DataContext = regressionData;

            List<string> values = ExtractValuesFromRegressionResult(result);

            regressionData.Accuracy = values[3];
            regressionData.TrainingLoss = values[1];

            Console.WriteLine("result: " + result);

            regressionData.PlotPredictionEvent += PlotPrediction;
            regressionData.PredictPointEvent += PredictPoint;
        }

        List<string> ExtractValuesFromRegressionResult(string regResult)
        {
            List<string> result = new List<string>();

            regResult.Trim();

            char lastChar = ' ';

            string word = "";
            for (int i = 0; i < regResult.Length; i++)
            {
                char nextChar = regResult[i];
                if (nextChar == ' ' && lastChar != ' ')
                {
                    result.Add(word);
                    word = "";
                } else if (nextChar != ' ' && lastChar == ' ' || lastChar != ' ' && nextChar != ' ')
                {
                    word += regResult[i];
                }
                lastChar = nextChar;
            }

            foreach (string s in result)
            {
                Console.WriteLine("Value: " + s);
            }

            return result;
        }

        string GetLastResultLine(string input)
        {

            string[] splitInput = input.Split('\n');

            Console.WriteLine("Length: " + splitInput.Length);
            
            for (int i = 0; i < splitInput.Length; i++)
            {
                Console.WriteLine(splitInput[i] + "; " + i);
            }
            return splitInput[splitInput.Length - 2];
        }

        void PredictPoint(string input)
        {
            Console.WriteLine(input);    
            
        }
        void PlotPrediction()
        {

        }

        string RunFileAndReturnOutput(string path, string args)
        {
            path.Trim();
            args.Trim();
            path += " " + args;
            
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonPath;
            start.Arguments = path;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    return output;
                }
            }
        }

        void PromptPythonInstall()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Executable (*.exe)|*.exe";
            fileDialog.Title = "Select Python.exe";
            if (fileDialog.ShowDialog() == true)
            {
                if (fileDialog.FileName.EndsWith("python.exe"))
                {
                    pythonPath = fileDialog.FileName;
                    using (StreamWriter writer = new StreamWriter(pythonPathFileName))
                    {
                        writer.WriteLine(pythonPath);
                    }
                }
            }
        }

    }
}
