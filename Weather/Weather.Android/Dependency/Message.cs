using System;
using Weather.Helper.Interface;
#if Android
using Android.App;
using Android.Runtime;
using Android.Widget;
using Weather.Droid.Dependency;
#elif iOS
using Foundation;
using Weather.iOS.Dependency;
using UIKit;
#endif

[assembly: Xamarin.Forms.Dependency(typeof(Message))]
#if Android
namespace Weather.Droid.Dependency
#elif iOS
namespace Weather.iOS.Dependency
#endif
{
    [Preserve(AllMembers = true)]
    public class Message : IMessage
    {
        #region Fields

#if iOS
        const double longdelay = 3.5;
        const double shortdelay = 0.75;
#endif

        #endregion

        #region Constructor

        public Message()
        {

        }

        #endregion

        public void LongAlert(string message)
        {
#if Android
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
#elif iOS
            DisplayAlert(message, longdelay);
#endif
        }

        public void ShortAlert(string message)
        {
#if Android
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
#elif iOS
            DisplayAlert(message, shortdelay);
#endif
        }

#if iOS
        void DisplayAlert(string message, double seconds)
        {
            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
            {
                DismissAlert(alert, obj);
            });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void DismissAlert(UIAlertController alert, NSTimer alertDelay)
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }

            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
#endif
    }
}