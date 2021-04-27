using System;
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
        bool isreportLoading;
        string searchText;

        #endregion

        #region Constructor

        public DashboardViewModel()
        {
            searchbarPosition = SearchBarPosition.Center;
            GetWeatherCommand = new Command(GetWeather);
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

        public ICommand GetWeatherCommand { get; set; }

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

        public bool IsReportLoading
        {
            get { return isreportLoading; }
            set
            {
                isreportLoading = value;
                OnPropertyChanged(nameof(IsReportLoading));
            }
        }

        #endregion

        #region Callbacks

        public async void GetWeather()
        {
            if (IsReportLoading)
            {
                DeviceUtils.ShowAlert(AppConstants.DataLoadingMessage, false);
                return;
            }
            else if (string.IsNullOrEmpty(searchText))
            {
                DeviceUtils.ShowAlert(AppConstants.SearchTextEmpty, false);
                return;
            }

            IsReportLoading = true;

            //Restricting the search bar position to top only after first time due to specific requirement.
            if (searchbarPosition == SearchBarPosition.Center)
            {
                SearchBarPosition = searchbarPosition ==
                    SearchBarPosition.Center ? SearchBarPosition.Top :
                    SearchBarPosition.Center;
            }

            if(!IsNetworkDetected || HasNetworkConnection)
            {
                bool haspostalCode = int.TryParse(searchText, out int postalCode);
                SelectedCityReport = haspostalCode ? await WeatherService.Instance.GetWeatherReportByPostal(postalCode)
                    : await WeatherService.Instance.GetWeatherReportByCity(searchText);
            }

            if (SelectedCityReport == null)
            {
                DeviceUtils.ShowAlert(AppConstants.UnableTogetData, true);
            }

            IsReportLoading = false;
        }

        #endregion

    }
}