using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Weather.Data;
using Weather.Enum;
using Weather.Helper.Utils;
using Weather.Models;
using Weather.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class DashboardViewModel : BaseViewModel
    {
        #region Fields

        ObservableCollection<WeatherReport> forecastreports;
        SearchBarPosition searchbarPosition;
        WeatherReport selectedCityReport;
        bool isFavourie;

        #endregion

        #region Constructor

        public DashboardViewModel() 
        {
            searchbarPosition = SearchBarPosition.Center;
            GetWeatherCommand = new Command(() => GetWeather(false));
            UpdateFavouriteCity = new Command(GetUpdatedFavouriteCity);
            FavouriteCitiesCommand = new Command(GetFavouriteCities);
            PageAppearingCommand = new Command(OnPageAppearing);
            FavouritesViewModel = new FavouritesViewModel();
        }

        #endregion

        #region Properties

        public SearchBarPosition SearchBarPosition
        {
            get => searchbarPosition;
            set
            {
                searchbarPosition = value;
                OnPropertyChanged(nameof(SearchBarPosition));
            }
        }

        public WeatherReport SelectedCityReport
        {
            get => selectedCityReport;
            set
            {
                selectedCityReport = value;
                OnPropertyChanged(nameof(SelectedCityReport));
            }
        }

        public bool IsFavourie
        {
            get => isFavourie;
            set
            {
                isFavourie = value;
                OnPropertyChanged(nameof(IsFavourie));
            }
        }


        public ObservableCollection<WeatherReport> ForecastReports
        {
            get => forecastreports;
            set
            {
                forecastreports = value;
                OnPropertyChanged(nameof(ForecastReports));
            }
        }

        public WeatherForecast WeatherForecast { get; set; }

        public FavouritesViewModel FavouritesViewModel { get; set; }

        #endregion

        #region Commands

        public ICommand GetWeatherCommand { get; set; }

        public ICommand FavouriteCitiesCommand { get; set; }

        public ICommand UpdateFavouriteCity { get; set; }

        #endregion

        #region Callbacks

        public async void GetWeather(bool isPageAppeared)
        {
            if (!this.ValidateSearchText(isPageAppeared)) return;

            IsLoading = true;
            this.UpdateSearchBarPosition();
            if (HasNetworkConnection)
            {
                bool haspostalCode = int.TryParse(SelectedCityName, out int postalCode);
                SelectedCityReport = haspostalCode ? await WeatherService.Instance.GetWeatherReportByPostal(postalCode)
                    : await WeatherService.Instance.GetWeatherReportByCity(SelectedCityName);
                WeatherForecast = haspostalCode ? await WeatherService.Instance.GetWeatherForecastByPostal(postalCode)
                    : await WeatherService.Instance.GetWeatherForecastByCity(SelectedCityName);
            }

            if (SelectedCityReport == null)
            {
                AppUtils.ShowAlert(AppConstants.UnableTogetData, true);
                HasDataError = true;
            }
            else
            {
                HasDataError = false;
                ForecastReports = this.GetForecastReports();
                IsFavourie = await IsDatabaseHasCity(SelectedCityReport.CityName);
            }

            IsLoading = false;
        }

        #region Command Callbacks

        private void GetUpdatedFavouriteCity()
        {
            IsFavourie = !IsFavourie;
            UpdateCityInfoInDatabase(SelectedCityReport.CityName, IsFavourie);
        }

        private async void GetFavouriteCities()
        {
            Views.FavoriteCityPage favoriteCityPage = new Views.FavoriteCityPage();
            favoriteCityPage.BindingContext = FavouritesViewModel;
            await Application.Current.MainPage.Navigation.PushAsync(favoriteCityPage);
           
        }

        private void OnPageAppearing()
        {
            this.SelectedCityName = FavouritesViewModel.SelectedCityName;
            this.GetWeather(true);
        }

        #endregion

        #endregion

        #region Private Methods

        private void UpdateSearchBarPosition()
        {
            //Restricting the search bar position to top only after first time due to specific requirement.
            if (searchbarPosition == SearchBarPosition.Center)
            {
                SearchBarPosition = searchbarPosition ==
                    SearchBarPosition.Center ? SearchBarPosition.Top :
                    SearchBarPosition.Center;
            }
        }

        private bool ValidateSearchText(bool isPageAppeared)
        {
            if (IsLoading)
            {
                if (!isPageAppeared)
                {
                    AppUtils.ShowAlert(AppConstants.DataLoadingMessage, false);
                }

                return false;
            }
            else if (string.IsNullOrEmpty(SelectedCityName))
            {
                if (!isPageAppeared)
                {
                    AppUtils.ShowAlert(AppConstants.SearchTextEmpty, false);
                }

                return false;
            }

            return true;
        }


        private ObservableCollection<WeatherReport> GetForecastReports()
        {
            if (WeatherForecast != null)
            {
                List<WeatherReport> weatherReports = new List<WeatherReport>();
                foreach (WeatherReport report in WeatherForecast.WeatherReport)
                {
                    DateTime date = AppUtils.GetDateTime(report.Date);
                    if (date != null && date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                        weatherReports.Add(report);
                }

                if (weatherReports.Count > 3)
                {
                    weatherReports = weatherReports.Take(4).ToList();
                }

                return new ObservableCollection<WeatherReport>(weatherReports);
            }
            else
            {
                return new ObservableCollection<WeatherReport>();
            }
        }

        #endregion
    }
}