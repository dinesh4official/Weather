using System;
#if Android
using Android.Runtime;
#elif iOS
using Foundation;
#endif

#if Android
namespace Weather.Droid.Constants
#elif iOS
namespace Weather.iOS.Constants
#endif
{
    [Preserve(AllMembers = true)]
    public static class NativeAppConstants
    {
        public const string PrimaryColor = "#131E27";
        public const string SecondaryColor = "#c21f55";
#if Android
        public const string SearchPlate = "android:id/search_plate";
        public const string SearchImageIcon = "android:id/search_mag_icon";
#elif iOS
        public const string SearchField = "searchField";
        public const string ClearButton = "_clearButton";
        public const string FontAwesomeLight = "FontAwesome5Pro-Light";
        public const string SearchIconGlyph = "\uf057";
#endif
    }
}