using System;
using Xamarin.UITest;

namespace Weather.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            string deviceSerial;
            string packageName;
            if (platform == Platform.Android)
            {
                //Update the android device serial id. Refer the following link to find the serial id.
                //https://www.howtogeek.com/414617/how-to-find-your-android-devices-serial-number/"
                deviceSerial = "JNIJAI95NRV8EY79";
                packageName = "com.dinesh.weather";
                return ConfigureApp.Android
                    .EnableLocalScreenshots().InstalledApp(packageName)
                    .DeviceSerial(deviceSerial)
                    .WaitTimes(new WaitTimes())
                    .StartApp();
            }
            else
            {
                //Update the iOS device serial id. Refer the following link to find the serial id.
                //https://www.sourcefuse.com/blog/how-to-find-udid-in-the-new-iphone-xs-iphone-xr-and-iphone-xs-max/
                deviceSerial = "0ed971165c5be099228d968d4de97d80f06a8ed9";
                packageName = "com.dinesh.Weather";
                return ConfigureApp.iOS
                       .EnableLocalScreenshots()
                       .DeviceIdentifier(deviceSerial).InstalledApp(packageName)
                       .WaitTimes(new WaitTimes())
                       .StartApp();
            }
        }
    }
}