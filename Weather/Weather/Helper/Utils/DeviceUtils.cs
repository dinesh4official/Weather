using System;
using Weather.Helper.Interface;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Utils
{
    [Preserve(AllMembers = true)]
    public static class DeviceUtils
    {

        #region Property

        public static bool IsLandscapeOrientation => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape;

        #endregion

        #region Internal Methods

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

        #endregion
    }
}
