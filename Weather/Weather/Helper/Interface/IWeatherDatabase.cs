using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Interface
{
    [Preserve(AllMembers = true)]
    public interface IWeatherDatabase<T>
    {
        Task<List<T>> GetItemsAsync();

        Task<T> GetItemAsync(int id);

        void SaveItemAsync(T item);

        void DeleteItemAsync(T item);
    }
}