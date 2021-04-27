using System;
using System.Globalization;
using Weather.Data;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Converters
{
    [Preserve(AllMembers = true)]
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                throw new InvalidOperationException(AppConstants.BooleanTarget);
            }

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
