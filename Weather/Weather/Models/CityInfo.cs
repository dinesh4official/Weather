using System;
using SQLite;

namespace Weather.Models
{
    [Preserve(AllMembers = true)]
    public class CityInfo : NotifyListener
    {
        #region Fields

        string cityName;
        int id;

        #endregion

        #region Constructor

        public CityInfo()
        {

        }

        #endregion

        #region Properties

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string CityName
        {
            get => cityName;
            set
            {
                cityName = value;
                OnPropertyChanged(nameof(CityName));
            }
        }

        #endregion
    }
}