using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        LinearRegression regression;
        public MainWindow()
        {
            InitializeComponent();
            regression = new LinearRegression();
            regression = new LinearRegression();
            RegressionTab.DataContext = regression;
            //set classification tab data context here
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
            

        }


    }
}
