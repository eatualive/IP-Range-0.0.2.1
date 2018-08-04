using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static IP_Range.classTools;

namespace IP_Range
{
    public class classContainer : INotifyPropertyChanged
    {
        // We need this so that DataBindings are refreshed
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string strPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
        }

        #endregion

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _isexpanded;
        public bool IsExpanded
        {
            get { return _isexpanded; }
            set
            {
                _isexpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }
        
        public ObservableCollection<classContainer> Children { get; set; } = new ObservableCollection<classContainer>();
        public ObservableCollection<classHost> Hosts { get; set; } = new ObservableCollection<classHost>();


        public classContainer(string name, bool isexpanded = false)
        {
            Name = name;
            IsExpanded = isexpanded;
        }

        public classContainer()
        {
            IsExpanded = true;
        }
    }
}
