using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Controls
{
    [Preserve(AllMembers = true)]
    public class SearchBarExt : SearchBar
    {
        #region Bindable Properties

        public static readonly BindableProperty UnderlineColorProperty =
		   BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(SearchBarExt), Color.Transparent);

        #endregion

        #region Constructor

        public SearchBarExt()
        {

        }

        #endregion

        #region Properties

        public Color UnderlineColor
        {
            get { return (Color)GetValue(UnderlineColorProperty); }
            set { SetValue(UnderlineColorProperty, value); }
        }

        #endregion
    }
}
