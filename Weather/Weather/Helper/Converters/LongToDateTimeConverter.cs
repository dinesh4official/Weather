using System;
using System.Globalization;
using Weather.Data;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Converters
{
    [Preserve(AllMembers = true)]
    public class LongToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long dateTime = (long)value;
            DateTime actualDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            return actualDate.AddSeconds(dateTime) + " " + AppConstants.UTC;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}