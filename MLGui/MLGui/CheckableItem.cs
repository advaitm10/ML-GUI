using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace MLGui
{
    public class CheckableItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<CheckableItem> CheckedChanged = delegate { };

        bool isChecked;

        string content;

        public CheckableItem(string _content, bool _checked)
        {
            Checked = _checked;
            Content = _content;
        }

        public bool Checked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                CheckedChanged(this);
                OnPropertyChanged();
            }
        }
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
