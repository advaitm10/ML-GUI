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

        ClassificationModel cmodel = new ClassificationModel();
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
            }
            else
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
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
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
                cmodel.Data = fileDialog.FileName;
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
                if (RegressionSelection.SelectedIndex == 0)
                {

                    model = new RegressionModel();

                    ModelCreationPopup.Visibility = Visibility.Visible;
                    ModelCreationPopup.DataContext = model;

                    model.PredictPointEvent += PredictPoint;

                    string dropDownOptions = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "get-column-options.py", regression.Data, pythonPath);

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

                } else if (RegressionSelection.SelectedIndex == 1)
                {
                    CreateClassificationModel();
                }
            }
        }

        void CreateClassificationModel()
        {
            //This isn't really that complicated
            //We just need to store the settings somewhere
            //That's the point of cmodel
            //All the actual work is done by RunFileAndReturnOutput

            ClassificationPagePopup.Visibility = Visibility.Visible;

            string dropDownOptions = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "get-column-options.py", cmodel.Data, pythonPath);

            string[] columns = new string[1];

            //reads from the python script
            using (var reader = new StringReader(dropDownOptions))
            {
                columns = reader.ReadLine().Split(',');
            }

            ClassificationPagePopup.DataContext = cmodel;

            ClassificationPagePopup.LabelColumnNameSelector.ItemsSource = columns;
            ClassificationPagePopup.TextColumnNameSelector.ItemsSource = columns;


            cmodel.ContinueClickedEvent += ContinueButtonClickedCmodel;
        }
        void ContinueButtonClickedCmodel()
        {
            Console.WriteLine("clicked");

            cmodel.LabelColumn = (string)ClassificationPagePopup.LabelColumnNameSelector.SelectedItem;
            cmodel.TextColumn = (string)ClassificationPagePopup.TextColumnNameSelector.SelectedItem;

            string[] words = cmodel.Text.Split(' ');
            string csvWords = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (i != words.Length - 1)
                {
                    csvWords += words[i] + ",";
                } else
                {
                    csvWords += words[i];
                }
            }

            cmodel.Text = csvWords;

            string args = String.Format("{0} {1} {2} {3}",
                cmodel.Data, cmodel.TextColumn, cmodel.LabelColumn, cmodel.Text);

            Console.WriteLine("cmodel args: " + args);

            string result = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "NLP.py",
                args, pythonPath);

            Console.WriteLine("result before split: " + result);
        }
        //so, this function actually calls the regression method. 
        void OnModelCreate()
        {
            Console.WriteLine("clicked model create button");

            //these are columns
            string categorical = model.IndependentColumnsCategorical.Substring(0, model.IndependentColumnsCategorical.Length - 1);
            string continuous = model.IndependentColumnsContinuous.Substring(0, model.IndependentColumnsContinuous.Length - 1);

            Console.WriteLine(String.Format("{0} {1} {2} {3} {4}",
                regression.Data, model.DependentColumn, categorical, continuous, model.Cycles));

            //so, if the point exists, so just check model.columnandvalue
            string args = "";

            string fileName = "point.csv";

            int shouldPlot = model.Plot ? 1 : 0;

            if (model.ColumnAndValue == null)
            {
                args = String.Format("{0} {1} {2} {3} {4} {5}",
                regression.Data, model.DependentColumn, categorical, continuous, model.Cycles, shouldPlot);
            }
            else
            {
                args = String.Format("{0} {1} {2} {3} {4} {5} {6}",
                regression.Data, model.DependentColumn, categorical, continuous, model.Cycles, shouldPlot,
                fileName);
            }

            Console.WriteLine("args: " + args);

            string result = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "regression.py",
                args, pythonPath);

            Console.WriteLine("result before split: " + result);

            string[] beforeBruh = result.Split(new string[] { "bruh"}, StringSplitOptions.None);

            string prediction = "";

            prediction = GetLastValueInResult(result);

            result = GetLastResultLine(beforeBruh);

            ModelCreationPopup.Visibility = Visibility.Collapsed;

            SetUpRegressionPage(result, prediction);

        }

        void SetUpRegressionPage(string result, string prediction)
        {
            RegressionPagePopup.Visibility = Visibility.Visible;

            regressionData = new RegressionPageData();
            RegressionPagePopup.DataContext = regressionData;

            List<string> values = ExtractValuesFromRegressionResult(result);

            regressionData.Accuracy = values[3];
            regressionData.TrainingLoss = values[1];
            regressionData.Prediction = prediction;

            Console.WriteLine("result: " + result);
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
                }
                else if (nextChar != ' ' && lastChar == ' ' || lastChar != ' ' && nextChar != ' ')
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
        string GetLastValueInResult(string result)
        {
            string ans = "";
            for (int i = result.Length - 2; i >= 0; i--)
            {
                int nextChar = result[i - 1];
                if (nextChar == ' ')
                {
                    return ans;
                } else
                {
                    ans = result[i] + ans;
                }
            }
            return ans;
        }

        string GetLastResultLine(string[] input)
        {
            string inputString = input[0];

            string[] splitInput = inputString.Split('\n');

            Console.WriteLine("Length: " + splitInput.Length);

            for (int i = 0; i < splitInput.Length; i++)
            {
                Console.WriteLine(splitInput[i] + "; " + i);
            }
            return splitInput[splitInput.Length - 2];
        }

        void PredictPoint()
        {
            string categorical = model.IndependentColumnsCategorical.Substring(0, model.IndependentColumnsCategorical.Length - 1);
            string continuous = model.IndependentColumnsContinuous.Substring(0, model.IndependentColumnsContinuous.Length - 1);

            PointSelector selector = new PointSelector(categorical, continuous, pythonPath);
            selector.SetColumnAndValue += SetColumnAndValue;
            selector.ShowDialog();
        }

        //this also means that the window closed
        void SetColumnAndValue(ObservableCollection<TextWrapperClass> columnAndVal)
        {
            model.ColumnAndValue = columnAndVal;
            WritePointToCSV();
        }

        void WritePointToCSV()
        {
            //model.CategoricalsPoint + "," + model.ContinuousPoint;
            //so, I already have the model. That means this shouldn't be that difficult. 
            //Hm. 
            //alright, so each value already has a column. 
            //Hm. There's only one point, right? So it should be fine. We just loop through twice

            string output = "";
            for (int i = 0; i < model.ColumnAndValue.Count; i++)
            {
                if (i != model.ColumnAndValue.Count - 1)
                {
                    output += model.ColumnAndValue[i].Column + ",";
                }
                else
                {
                    output += model.ColumnAndValue[i].Column;
                }
            }
            output += "\n";
            for (int i = 0; i < model.ColumnAndValue.Count; i++)
            {
                if (i != model.ColumnAndValue.Count - 1)
                {
                    output += model.ColumnAndValue[i].Text + ",";
                }
                else
                {
                    output += model.ColumnAndValue[i].Text;
                }
            }

            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "point.csv", output);


        }



        public static string RunFileAndReturnOutput(string path, string args, string pythonPath)
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
