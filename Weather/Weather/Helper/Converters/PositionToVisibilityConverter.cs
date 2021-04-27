using System;
using System.Globalization;
using Weather.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Converters
{
    [Preserve(AllMembers = true)]
    public class PositionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SearchBarPosition searchBarPosition)
            {
                return searchBarPosition == SearchBarPosition.Top;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
