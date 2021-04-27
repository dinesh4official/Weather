using System;
using Xamarin.Forms;
using Weather.Controls;
#if Android
using Android.Content;
using Android.Runtime;
using Xamarin.Forms.Platform.Android;
#elif iOS
using Foundation;
using Xamarin.Forms.Platform.iOS;
#endif

//[assembly: ExportRenderer(typeof(GridExt), typeof(GridExtRenderer))]
#if Android
namespace Weather.Droid.Mapping
#elif iOS
namespace Weather.iOS.Mapping
#endif
{
    [Preserve(AllMembers = true)]
    public class GridExtRenderer : ViewRenderer
    {
        #region Constructor

#if Android
        public GridExtRenderer(Context context) : base(context)
#elif iOS
        public GridExtRenderer()
#endif
        {

        }

        #endregion

        #region Properties

        public GridExt GridExt
        {
            get => this.Element as GridExt;
        }

        #endregion

        #region Override Methods

#if Android
        /// <summary>
        /// This method is called when the layout needs to be updated.
        /// </summary>
        /// <param name="changed">Represents whether the layout is changed or not.</param>
        /// <param name="left">Represents the X position.</param>
        /// <param name="top">Represents the Y position.</param>
        /// <param name="right">Represents the width of the view.</param>
        /// <param name="bottom">Represents the height of the view.</param>
        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            this.ValidateDeviceOrientation();
        }
#elif iOS
        /// <summary>
        /// This method is called when the layout needs to be updated.
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this.ValidateDeviceOrientation();
        }
#endif
        #endregion

        #region Private Methods

        /// <summary>
        /// Method to find the device orientation changes and then update the forms view.
        /// </summary>
        /// <remarks>When the device orientation is changed, <see cref="Xamarin.Forms.CollectionView"/>
        /// won't notify the layout changes to it's child elements. So, we have provided a work around
        /// to resove this issue.</remarks>
        private void ValidateDeviceOrientation()
        {
            if (GridExt.Parent is CollectionView collectionView)
            {
                double width = collectionView.Width;
                if (GridExt.PreviousParentWidth != width)
                {
                    GridExt.PreviousParentWidth = collectionView.Width;
                    GridExt.UpdateView();
                }
            }
        }

        #endregion
    }
}
