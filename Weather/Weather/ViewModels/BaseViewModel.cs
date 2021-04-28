using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Weather.Data;
using Weather.Helper.Utils;
using Weather.Models;
using Weather.Services;
using Weather.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
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
            FavouriteCities = new ObservableCollection<CityInfo>();
            FavouriteCitiesCommand = new Command(async()=>
            {
                FavoriteCityPage favoriteCityPage = new FavoriteCityPage();
                favoriteCityPage.BindingContext = this;
                await Application.Current.MainPage.Navigation.PushAsync(favoriteCityPage);
            });
            InitializeDatabase();
        }

        ~BaseViewModel()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        #endregion

        #region Properties

        public ICommand FavouriteCitiesCommand { get; set; }

        public ICommand SelectFavourite { get; set; }

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

        public WeatherDatabase WeatherDatabase { get; set; }

        public ObservableCollection<CityInfo> FavouriteCities { get; set; }

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

        #region Weather Database

        public async void InitializeDatabase()
        {
            IsLoading = true;
            WeatherDatabase = await WeatherDatabase.Instance;
            List<CityInfo> favcities  = await WeatherDatabase.GetItemsAsync();
            FavouriteCities = favcities.ToObservableCollection();
            IsLoading = false;
        }

        public void UpdateCityInfoInDatabase(string cityName, bool shouldAdd)
        {
            CityInfo cityinfo = FavouriteCities.FirstOrDefault(i => i.CityName == cityName);

            if (shouldAdd)
            {
                if (cityinfo == null)
                {
                    cityinfo = new CityInfo();
                    cityinfo.CityName = cityName;
                    FavouriteCities.Add(cityinfo);
                }

                WeatherDatabase.SaveItemAsync(cityinfo);
            }
            else if (cityinfo == null)
            {
                FavouriteCities.Remove(cityinfo);
                WeatherDatabase.DeleteItemAsync(cityinfo);
            }
        }

        #endregion
    }
}