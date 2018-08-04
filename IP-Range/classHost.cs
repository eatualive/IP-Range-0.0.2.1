using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IP_Range
{
    public class classHost : INotifyPropertyChanged
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

        private string _ip;
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged("IP");
            }
        }

        private string _dns_name;
        public string DNS_Name
        {
            get { return _dns_name; }
            set
            {
                _dns_name = value;
                OnPropertyChanged("DNS_Name");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        
        public classHost(string ip = "", string dns_name = "", string description = "")
        {
            IP = ip;
            DNS_Name = dns_name;
            Description = description;
        }
    }
}
