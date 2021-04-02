using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Weather.Enum;
using Weather.Constants;
using Weather.Helper.Utils;

namespace Weather.Controls
{
    /// <summary>
    /// This class is used to arrange its position with respect to <see cref="Enum.SearchBarPosition"/> value.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SearchBarGrid : Grid
    {

        #region Fields

        /// <summary>
        /// Contains the previous width of <see cref="SearchBarGrid"/> to avoid the repeated layout. 
        /// </summary>
        double previousWidth = 0.0;

        /// <summary>
        /// Contains the previous height of <see cref="SearchBarGrid"/> to avoid the repeated layout. 
        /// </summary>
        double previousHeight = 0.0;

        #endregion

        #region Bindable Properties

        /// <summary>
        /// Bindable property for <see cref="SearchBarPosition"/>
        /// </summary>
        public static readonly BindableProperty SearchBarPositionProperty =
             BindableProperty.Create(AppConstants.SearchBarPosition, typeof(SearchBarPosition), typeof(SearchBarGrid),
                 SearchBarPosition.Center, propertyChanged: OnSearchBarPositionChanged);

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of <see cref="SearchBarGrid"/>.
        /// </summary>
        public SearchBarGrid()
        {
            this.UpdateGridPosition();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the position of <see cref="SearchBarGrid"/>.
        /// </summary>
        /// <remarks><see cref="SearchBarPosition.Center"/> is the default value.</remarks>
        public SearchBarPosition SearchBarPosition
        {
            get { return (SearchBarPosition)GetValue(SearchBarPositionProperty); }
            set { SetValue(SearchBarPositionProperty, value); }
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// This method is called when the size of the <see cref="SearchBarGrid"/> is set during a layout cycle.
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
                    this.UpdateGridPosition();
                }
            }
        }

        #endregion

        #region Property Changed

        /// <summary>
        /// Notified when <see cref="SearchBarPosition"/> property gets updated.
        /// </summary>
        /// <param name="bindable">Represents the <see cref="SearchView"/>.</param>
        /// <param name="oldValue">Indicates the previous value of <see cref="SearchViewPosition"/>.</param>
        /// <param name="newValue">Indicates the current value of <see cref="SearchViewPosition"/>.</param>
        private static void OnSearchBarPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SearchBarGrid searchBarGrid)
            {
                searchBarGrid.UpdateGridPosition();
            }
            else
            {
                throw new Exception(AppConstants.BindableError);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Layouts the <see cref="SearchBarGrid"/> with respect to <see cref="SearchBarPosition"/> property.
        /// </summary>
        void UpdateGridPosition()
        {
            double yPosition = this.SearchBarPosition == SearchBarPosition.Center ? 0.5 : 0;
            double height = DeviceUtils.IsLandscapeOrientation ? 0.2 : 0.1;
            Rectangle rectangle = new Rectangle(0, yPosition, 1, height);
            AbsoluteLayout.SetLayoutBounds(this, rectangle);
        }

        #endregion
    }
}