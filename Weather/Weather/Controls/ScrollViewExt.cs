using System;
using System.Runtime.CompilerServices;
using Weather.Helper.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Controls
{
    [Preserve(AllMembers = true)]
    public class ScrollViewExt : ScrollView
    {
        #region Fields

        /// <summary>
        /// Contains the previous width of <see cref="ScrollViewExt"/> to avoid the repeated layout. 
        /// </summary>
        double previousWidth = 0.0;

        /// <summary>
        /// Contains the previous height of <see cref="ScrollViewExt"/> to avoid the repeated layout. 
        /// </summary>
        double previousHeight = 0.0;

        #endregion

        #region Constructor

        public ScrollViewExt()
        {
            this.BackgroundColor = Color.Transparent;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// This method is called when the size of the <see cref="ScrollViewExt"/> is set during a layout cycle.
        /// </summary>
        /// <param name="width">The new width of the element.</param>
        /// <param name="height">The new height of the element.</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > 0 && height > 0)
            {
                //To avoid the repeated layout arrangement when device orientation is changed.
                if (previousWidth != width && previousHeight != height)
                {
                    this.previousWidth = width;
                    this.previousHeight = height;
                    this.UpdatePosition();
                }
            }
        }

        /// <summary>
        /// This method is called when any of the <see cref="ScrollViewExt"/> property is changed.
        /// </summary>
        /// <param name="propertyName">Represents the property name.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == SearchBar.IsVisibleProperty.PropertyName && this.IsVisible)
            {
                this.UpdatePosition();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the layout bounds when visibility or orientation is changed.
        /// </summary>
        private void UpdatePosition()
        {
            double position = DeviceUtils.IsLandscapeOrientation ? 0.8 : 0.9;
            Rectangle rectangle = new Rectangle(0, position, 1, position); ;
            AbsoluteLayout.SetLayoutBounds(this, rectangle);
        }

        #endregion
    }
}