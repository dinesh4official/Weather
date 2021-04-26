using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Weather.Data;
using Weather.Enum;
using Weather.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class DashboardViewModel : BaseViewModel
    {
        #region Fields

        SearchBarPosition searchbarPosition;

        ObservableCollection<CityInfo> cityInfos;

        #endregion

        #region Constructor

        public DashboardViewModel()
        {
            this.GetCities();
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

        public ObservableCollection<CityInfo> Cities
        {
            get => cityInfos;
            set
            {
                cityInfos = value;
                OnPropertyChanged(nameof(Cities));
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

        #region Private Methods

        private void GetCities()
        {
            string jsonFileName = AppConstants.CityJSONFilePath;
            Assembly assembly = Application.Current.MainPage.GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                List<CityInfo> citieslist = JsonConvert.DeserializeObject<List<CityInfo>>(json);
                cityInfos = new ObservableCollection<CityInfo>(citieslist);
            }
        }

        #endregion
    }
}