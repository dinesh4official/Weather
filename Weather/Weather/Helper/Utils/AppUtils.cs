using System;
using System.Collections;
using System.Collections.Generic;
using Weather.Helper.Interface;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Utils
{
    [Preserve(AllMembers = true)]
    public static class AppUtils
    {
        #region Property

        public static bool IsLandscapeOrientation => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape;

        #endregion

        #region Public Methods

        public static void ShowAlert(string message, bool isLong)
        {
            if (isLong)
            {
                DependencyService.Get<IMessage>().LongAlert(message);
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(message);
            }
        }

        public static DateTime GetDateTime(long dateTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(dateTime);
        }

        public static IEnumerable<T> ToList<T>(this IEnumerable itemsSource)
        {
            foreach(var item in itemsSource)
            {
                yield return (T)item;
            }
        }

        #endregion
    }
}
