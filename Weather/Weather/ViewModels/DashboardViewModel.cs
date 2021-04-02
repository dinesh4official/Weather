using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Weather.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class DashboardViewModel : INotifyPropertyChanged
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}