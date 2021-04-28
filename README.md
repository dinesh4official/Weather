# Weather
This application provides the weather information for the given city in the US region with respect to city name and postal code using Open Weather service.

## Architecture

In this application, we have followed the MVVM Pattern.

## Screenshot

### Android
<img src="https://github.com/dinesh4official/Weather/blob/main/Screenshot/Weather_Android.png">

### iOS
<img src="https://github.com/dinesh4official/Weather/blob/main/Screenshot/Weather_iOS.png">

## Known Issue
When the device is rotated, weather forecast view does not resize properly. Because, `Orientation` changes are not notified to child elements of the `CollectionView` 

https://github.com/xamarin/Xamarin.Forms/issues/6869
