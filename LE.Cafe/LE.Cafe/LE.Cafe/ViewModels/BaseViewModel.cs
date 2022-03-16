using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LE.Cafe.ViewModels
{
    public class BaseViewModel :ObservableBaseClass
    {
        

        public BaseViewModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        ~BaseViewModel()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; OnPropertyChanged(nameof(IsExpanded)); }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        private bool _isEnable;

        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; OnPropertyChanged(nameof(IsEnable)); }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
                UserDialogs.Instance.Toast("Oops, looks like you don't have internet connection :(");
            else
                UserDialogs.Instance.Toast("Your internet connection is back :)");
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
