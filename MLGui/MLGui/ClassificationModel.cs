using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MLGui
{
    public class ClassificationModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action ContinueClickedEvent = delegate { };

        ICommand continueClickedCommand;

        public ICommand ContinueClicked
        {
            get
            {
                if (continueClickedCommand == null)
                {
                    continueClickedCommand = new RelayCommand(param => ContinueClickedEvent());
                }
                return continueClickedCommand;
            }
        }
        public string Text
        {
            get;
            set;
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        string output;

        public string Data
        {
            get;
            set;
        }
        public string TextColumn
        {
            get;
            set;
        }
        public String LabelColumn
        {
            get;
            set;
        }
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                OnPropertyChanged();
            }
        }
    }
}
