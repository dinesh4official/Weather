using System;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Interface
{
    [Preserve(AllMembers = true)]
    public interface IMessage
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
