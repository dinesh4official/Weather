using System;
using Weather.Helper.Interface;
using Android.App;
using Android.Runtime;
using Android.Widget;
using Weather.Droid.Dependency;

[assembly: Xamarin.Forms.Dependency(typeof(Message))]
namespace Weather.Droid.Dependency
{
    [Preserve(AllMembers = true)]
    public class Message : IMessage
    {
        #region Constructor

        public Message()
        {

        }

        #endregion

        #region Interface Methods

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        #endregion
    }
}