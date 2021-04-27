using System;
using System.Globalization;
using Weather.Data;
using Weather.Helper.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Converters
{
    [Preserve(AllMembers = true)]
    public class LongToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = AppUtils.GetDateTime((long)value);
            string format = (string)parameter;
            if (format != null)
            {
                string actualFormat = format.Contains(AppConstants.DayFormat)
                    ? AppConstants.DayFormat : AppConstants.DateMonthFormat;
                return date.ToString(actualFormat, DateTimeFormatInfo.InvariantInfo);
            }
            else
            {
                return date.ToString(AppConstants.DateFormat, DateTimeFormatInfo.InvariantInfo);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}