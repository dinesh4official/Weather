using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Weather.Helper.Utils;
using Weather.Models;

namespace Weather.Services
{
    [Preserve(AllMembers = true)]
    public class WeatherDatabase
    {
        #region Fields

        static SQLiteAsyncConnection database;

        #endregion

        #region Constructor

        public WeatherDatabase()
        {
            database = new SQLiteAsyncConnection(DatabaseUtils.DatabasePath, DatabaseUtils.Flags);
        }

        #endregion

        #region Properties

        public static readonly AsyncLazy<WeatherDatabase> Instance = new AsyncLazy<WeatherDatabase>(async () =>
        {
            var instance = new WeatherDatabase();
            CreateTableResult result = await database.CreateTableAsync<CityInfo>();
            return instance;
        });

        #endregion

        #region Methods

        public Task<List<CityInfo>> GetItemsAsync()
        {
            return database.Table<CityInfo>().ToListAsync();
        }

        public Task<CityInfo> GetItemAsync(int id)
        {
            return database.Table<CityInfo>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public void SaveItemAsync(CityInfo item)
        {
            if (item.ID != 0)
            {
                database.UpdateAsync(item);
            }
            else
            {
                database.InsertAsync(item);
            }
        }

        public void DeleteItemAsync(CityInfo item)
        {
            database.DeleteAsync(item);
        }

        #endregion
    }
}
