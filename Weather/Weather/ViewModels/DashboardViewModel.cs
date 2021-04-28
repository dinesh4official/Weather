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

        SearchBarPosition searchbarPosition;
        WeatherReport selectedCityReport;
        ObservableCollection<WeatherReport> forecastreports;
        string searchText;

        #endregion

        #region Constructor

        public DashboardViewModel()
        {
            searchbarPosition = SearchBarPosition.Center;
            GetWeatherCommand = new Command(GetWeather);
            SelectFavourite = new Command(() =>
            {
                SelectedCityReport.IsFavourie = !SelectedCityReport.IsFavourie;
                UpdateCityInfoInDatabase(SelectedCityReport.CityName, SelectedCityReport.IsFavourie);
            });
        }

        #endregion

        #region Properties

        public ICommand GetWeatherCommand { get; set; }

        public SearchBarPosition SearchBarPosition
        {
            get => searchbarPosition;
            set
            {
                searchbarPosition = value;
                OnPropertyChanged(nameof(SearchBarPosition));
            }
        }

        public string SearchBarText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SelectedCityReport));
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

        public ObservableCollection<WeatherReport> ForecastReports
        {
            get => forecastreports;
            set
            {
                forecastreports = value;
                OnPropertyChanged(nameof(ForecastReports));
            }
        }

        #endregion

        #region Callbacks

        public async void GetWeather()
        {
            if (!this.ValidateSearchText()) return;

            IsLoading = true;
            this.UpdateSearchBarPosition();
            WeatherForecast forecast = null;
            if (!IsNetworkDetected || HasNetworkConnection)
            {
                bool haspostalCode = int.TryParse(searchText, out int postalCode);
                SelectedCityReport = haspostalCode ? await WeatherService.Instance.GetWeatherReportByPostal(postalCode)
                    : await WeatherService.Instance.GetWeatherReportByCity(searchText);
                forecast = haspostalCode ? await WeatherService.Instance.GetWeatherForecastByPostal(postalCode)
                    : await WeatherService.Instance.GetWeatherForecastByCity(searchText);
            }

            if (SelectedCityReport == null)
            {
                AppUtils.ShowAlert(AppConstants.UnableTogetData, true);
                HasDataError = true;
            }
            else
            {
                HasDataError = false;
                this.UpdateForecastReports(forecast);
            }

            IsLoading = false;
        }

        private bool ValidateSearchText()
        {
            if (IsLoading)
            {
                AppUtils.ShowAlert(AppConstants.DataLoadingMessage, false);
                return false;
            }
            else if (string.IsNullOrEmpty(searchText))
            {
                AppUtils.ShowAlert(AppConstants.SearchTextEmpty, false);
                return false;
            }

            return true;
        }

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

        private void UpdateForecastReports(WeatherForecast forecast)
        {
            if (forecast != null)
            {
                List<WeatherReport> weatherReports = new List<WeatherReport>();
                foreach (WeatherReport report in forecast.WeatherReport)
                {
                    DateTime date = AppUtils.GetDateTime(report.Date);
                    if (date != null && date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                        weatherReports.Add(report);
                }

                if (weatherReports.Count > 3)
                {
                    weatherReports = weatherReports.Take(4).ToList();
                }

                ForecastReports = new ObservableCollection<WeatherReport>(weatherReports);
            }
            else
            {
                ForecastReports = new ObservableCollection<WeatherReport>();
            }
        }

        #endregion

    }
}