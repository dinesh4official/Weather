using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Weather.Data;
using Weather.Helper.Interface;
using Weather.Models;
using Weather.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class BaseViewModel : NotifyListener, INetworkDetector
    {
        #region Fields

        bool hasNetworkConnection;
        bool hasDataError;
        bool isLoading;
        string selectedcityName;

        #endregion

        #region Constructor

        public BaseViewModel()
        {
            SubscribeNetworkDetector();
            InitializeDatabase();
            SelectFavouriteCity = new Command<string>(SelectCity);
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

        public string SelectedCityName
        {
            get => selectedcityName;
            set
            {
                selectedcityName = value;
                OnPropertyChanged(nameof(SelectedCityName));
            }
        }

        public WeatherDatabase WeatherDatabase { get; set; }

        #endregion

        #region Command

        public ICommand PageAppearingCommand { get; set; }

        public ICommand SelectFavouriteCity { get; set; }

        #endregion

        #region Network Detector

        private void SubscribeNetworkDetector()
        {
            HasNetworkConnection = Connectivity.NetworkAccess == NetworkAccess.Internet;
            MessagingCenter.Subscribe<INetworkDetector, bool>(this, AppConstants.NetworkDetector, (sender, arg) =>
            {
                HasNetworkConnection = arg;
            });
        }

        #endregion

        #region Weather Database

        public async void InitializeDatabase()
        {
            IsLoading = true;
            WeatherDatabase = await WeatherDatabase.Instance;
            IsLoading = false;
        }

        public async void UpdateCityInfoInDatabase(string cityName, bool shouldAdd)
        {
            List<CityInfo> favcities = await WeatherDatabase.GetItemsAsync();
            CityInfo cityinfo = favcities.FirstOrDefault(i => i.CityName.Equals(cityName));

            if (shouldAdd)
            {
                if (cityinfo == null)
                {
                    cityinfo = new CityInfo();
                    cityinfo.CityName = cityName;
                }

                WeatherDatabase.SaveItemAsync(cityinfo);
            }
            else if (cityinfo != null)
            {
                WeatherDatabase.DeleteItemAsync(cityinfo);
            }
        }

        public async Task<bool> IsDatabaseHasCity(string cityName)
        {
            List<CityInfo> favcities = await WeatherDatabase.GetItemsAsync();
            return favcities.Any(i => i.CityName.Equals(cityName));
        }

        #endregion

        #region Command Callbacks

        private async void SelectCity(string favouriteCity)
        {
            SelectedCityName = favouriteCity;
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}