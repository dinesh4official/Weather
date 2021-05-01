using System;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Interface
{
    [Preserve(AllMembers = true)]
    public interface INetworkDetector
    {
        bool HasNetworkConnection { get; set; }
    }
}
