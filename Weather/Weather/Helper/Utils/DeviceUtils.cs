using System;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Utils
{
    [Preserve(AllMembers = true)]
    public static class DeviceUtils
    {

        public static bool IsLandscapeOrientation => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape;

    }
}
