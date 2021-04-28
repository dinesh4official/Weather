# Weather
This application provides the weather information for the given city in the US region with respect to city name and postal code using Open Weather service.

## Architecture

In this application, we have followed the MVVM Pattern.

## Screenshot

### Android
<img src="https://github.com/dinesh4official/BorderView/blob/master/ScreenShot/Weather_Android.Png">

### iOS
<img src="https://github.com/dinesh4official/BorderView/blob/master/ScreenShot/Weather_iOS.Png">

## Known Issue
When the device is rotated, weather forecast view does not resize properly. Because, `Orientation` changes are not notified to child elements of the `CollectionView` 

https://github.com/xamarin/Xamarin.Forms/issues/6869
