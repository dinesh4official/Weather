using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Weather.Data;
using Weather.Helper.Utils;
using Xamarin.Essentials;
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

        public bool IsNetworkDetected { get; set; }

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
            IsNetworkDetected = true;
            HasNetworkConnection = !(e.NetworkAccess == NetworkAccess.None || e.NetworkAccess == NetworkAccess.Unknown);
            if (HasNetworkConnection)
            {
                DeviceUtils.ShowAlert(AppConstants.AvailableConnection, false);
            }
            else
            {
                DeviceUtils.ShowAlert(AppConstants.NoConnection, true);
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