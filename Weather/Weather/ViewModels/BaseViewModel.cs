using System;
using Weather.Data;
using Weather.Helper.Utils;
using Weather.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class BaseViewModel : NotifyListener
    {
        #region Fields

        bool hasNetworkConnection;
        bool hasDataError;
        bool isLoading;

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

        public bool HasDataError
        {
            get => hasDataError;
            set
            {
                hasDataError = value;
                OnPropertyChanged(nameof(HasDataError));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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
                AppUtils.ShowAlert(AppConstants.AvailableConnection, false);
            }
            else
            {
                AppUtils.ShowAlert(AppConstants.NoConnection, true);
            }
        }

        #endregion
    }
}