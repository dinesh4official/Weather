# Weather
This application provides the weather conditions of different cities in the US region with respect to city name and postal code.

## Known Issue
When the device is rotated, weather forecast view does not resize properly. Because, `Orientation` changes are not notified to child elements of the `CollectionView` 

https://github.com/xamarin/Xamarin.Forms/issues/6869
