using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Converters
{
    [Preserve(AllMembers = true)]
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName = $"Weather.Images.w04d.png";

            if (value is string imagePath && string.IsNullOrEmpty(imagePath))
                imageName = $"Weather.Images.w{imagePath}.png";

            return ImageSource.FromResource(imageName, typeof(IconConverter).GetTypeInfo().Assembly);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
