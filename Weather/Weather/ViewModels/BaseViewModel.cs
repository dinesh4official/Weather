using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Weather.Data;
using Weather.Helper.Interface;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        bool hasNetworkConnection;

        #endregion

        #region Constructor

        public BaseViewModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        ~BaseViewModel()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        #endregion

        #region Properties

        public bool HasNetworkConnection
        {
            get => hasNetworkConnection;
            set
            {
                hasNetworkConnection = value;
                OnPropertyChanged(nameof(HasNetworkConnection));
            }
        }

        #endregion

        #region Network Detector

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            HasNetworkConnection = !(e.NetworkAccess == NetworkAccess.None || e.NetworkAccess == NetworkAccess.Unknown);
            if (HasNetworkConnection)
            {
                DependencyService.Get<IMessage>().ShortAlert(AppConstants.AvailableConnection);
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(AppConstants.NoConnection);
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}