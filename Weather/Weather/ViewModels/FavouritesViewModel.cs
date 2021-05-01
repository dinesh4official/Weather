using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Weather.Helper.Utils;
using Weather.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.ViewModels
{
    [Preserve(AllMembers = true)]
    public class FavouritesViewModel : BaseViewModel
    {
        #region Fields

        ObservableCollection<CityInfo> cities;

        #endregion

        #region Constructor

        public FavouritesViewModel()
        { 
            IsLoading = true;
            PageAppearingCommand = new Command(OnPageAppearing);
        }

        #endregion

        #region Properties

        public ObservableCollection<CityInfo> FavouriteCities
        {
            get => cities;
            set
            {
                cities = value;
                OnPropertyChanged(nameof(FavouriteCities));
            }
        }

        #endregion

        #region Command Callbacks

        private async void OnPageAppearing()
        {
            FavouriteCities = await GetCitiesAsync();
            IsLoading = false;
        }

        #endregion

        #region Private Methods

        private async Task<ObservableCollection<CityInfo>> GetCitiesAsync()
        {
            List<CityInfo> favcities = await WeatherDatabase.GetItemsAsync();
            return favcities.ToObservableCollection() ?? new ObservableCollection<CityInfo>();
        }

        #endregion
    }
}
