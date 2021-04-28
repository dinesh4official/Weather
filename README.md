# Weather
This application provides the weather information for the given city in the US region with respect to city name and postal code using Open Weather service.

## Architecture

In this application, we have followed the MVVM Pattern.

## Feature

* City Weather Information like temperature, humidity, pressure, minimum and maximum temperature.
* Forecast Weather Information for upcoming four days.
* Favourite Cities
* Supported for both Portrait and Landscape mode.

## Information

* To store the favourite cities, we have internally created a database to store the information and loaded in the list.
* Continuously monitoring the data connection in the application.
* If data connection is not available, respective alert is shown.

## Screenshot

### Android
<img src="https://github.com/dinesh4official/Weather/blob/main/Screenshot/Weather_Android.png">

### iOS
<img src="https://github.com/dinesh4official/Weather/blob/main/Screenshot/Weather_iOS.png">

## Known Issue
When the device is rotated, weather forecast view does not resize properly. Because, `Orientation` changes are not notified to child elements of the `CollectionView` 

https://github.com/xamarin/Xamarin.Forms/issues/6869
