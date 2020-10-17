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
                //ask for python install 

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
                //define a regression model
                //get a list of the names
                //set model creation list sources.

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
            string result = RunFileAndReturnOutput(System.AppDomain.CurrentDomain.BaseDirectory + "regression.py", String.Format("{0} {1} {2} {3}", 
                regression.Data, model.DependentColumn, model.IndependentColumnsCategorical, model.IndependentColumnsContinuous));
            
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

    }
}
