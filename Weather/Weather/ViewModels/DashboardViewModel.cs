using System;
using Weather.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class DashboardViewModel : BaseViewModel
    {
        #region Fields

        SearchBarPosition searchbarPosition;

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

        public Command GetWeatherCommand { get; set; }

        #endregion

        #region Callbacks

        public void GetWeather()
        {
            SearchBarPosition = searchbarPosition ==
                SearchBarPosition.Center ? SearchBarPosition.Top :
                SearchBarPosition.Center;
        }

        #endregion
    }
}