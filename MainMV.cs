using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ExchangeRate
{
    public class MainMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string[] _qtyConnection = new string[] { "1", "5", "10", "15", "20", "25" };
        private string[] _qtyQueries = new string[] { "25", "50", "75", "100" };
        private string _selected_qtyConnection;
        private string _selected_qtyQueries;
        private string _remoteEPAdress;
        private string _remoteEPPort;
        private string _remoteEPState;
        private bool _isCheked;
        public ICommand start { get; set; }
        public MainMV()
        {
            start = new CommandStart(this);
        }
        public bool IsCheked
        {
            get
            {
                return _isCheked;
            }
            set
            {
                _isCheked = value;
                OnPropertyChanged(nameof(IsCheked));
            }
        }
        public string RemoteEPAdress
        {
            get
            {
                return _remoteEPAdress;
            }
            set
            {
                _remoteEPAdress = value;
                OnPropertyChanged(nameof(RemoteEPAdress));
            }
        }
        public string RemoteEPPort
        {
            get
            {
                return _remoteEPPort;
            }
            set
            {
                _remoteEPPort = value;
                OnPropertyChanged(nameof(RemoteEPPort));
            }
        }
        public string RemoteEPState
        {
            get
            {
                return _remoteEPState;
            }
            set
            {
                _remoteEPState= value;
                OnPropertyChanged(nameof(RemoteEPState));
            }
        }
        public string Selected_qtyConnection
        {
            get
            {
                return _selected_qtyConnection;
            }
            set
            {
                _selected_qtyConnection = value;
                OnPropertyChanged(nameof(Selected_qtyConnection));
            }
        }
        public string Selected_qtyQueries
        {
            get
            {
                return _selected_qtyQueries;
            }
            set
            {
                _selected_qtyQueries = value;
                OnPropertyChanged(nameof(Selected_qtyQueries));
            }
        }
        public string[] QtyConnection
        {
            get
            {
                return _qtyConnection;
            }
            set
            {
                _qtyConnection = value;
                OnPropertyChanged(nameof(QtyConnection));
            }
        }
        public string[] QtyQueries
        {
            get
            {
                return _qtyQueries;
            }
            set
            {
                _qtyQueries = value;
                OnPropertyChanged(nameof(_qtyQueries));
            }
        }

        
    }
}
