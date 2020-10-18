using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MLGui
{
    public class ClassificationModel
    {

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
    }
}
