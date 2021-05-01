using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Models;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Interface
{
    [Preserve(AllMembers = true)]
    public interface IWeatherDatabase
    {
        Task<List<CityInfo>> GetItemsAsync();

        Task<CityInfo> GetItemAsync(int id);

        void SaveItemAsync(CityInfo item);

        void DeleteItemAsync(CityInfo item);
    }
}