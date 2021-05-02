using System;
using System.Collections;
using System.Linq;
using Weather.Helper.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Weather.Models;

namespace Weather.Controls
{
    [Preserve(AllMembers = true)]
    public class GridExt : Grid, IDisposable
    {
        #region Fields

        /// <summary>
        /// Increaments the next index id.
        /// </summary>
        private static int NextId = 0;

        /// <summary>
        /// Contains the previous width of <see cref="CollectionView"/> to avoid the repeated layout. 
        /// </summary>
        double previousWidth = 0.0;

        /// <summary>
        /// Contains the previous height of <see cref="CollectionView"/> to avoid the repeated layout. 
        /// </summary>
        double previousHeight = 0.0;

        #endregion

        #region Constructor

        public GridExt()
        {
            // Resetting the item index once it reaches the collection count.
            if (NextId > 3)
                NextId = 0;

            Index = NextId++;
        }

        #endregion

        #region Internal Properties

        internal int ItemsCount { get; set; }

        internal WeatherReport WeatherReport { get; set; }

        internal int Index { get; private set; }

        /// <summary>
        /// Contains the previous width of <see cref="CollectionView"/> to avoid the repeated layout. 
        /// </summary>
        internal double PreviousParentWidth { get; set; } = 0.0;

        #endregion

        #region Override Methods

        /// <summary>
        /// This method is called when the binding context of the <see cref="GridExt"/> is updated.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext is WeatherReport weatherReport)
            {
                this.WeatherReport = weatherReport;
                this.UpdateView();
            }
        }

        /// <summary>
        /// This method is called when the parent view is assigned.
        /// </summary>
        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (this.Parent is CollectionView collectionView)
            {
                collectionView.SizeChanged -= CollectionView_SizeChanged;
                collectionView.SizeChanged += CollectionView_SizeChanged;
                this.UpdateItemsCount(collectionView.ItemsSource);
                this.UpdateView();
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Updates the opacity and width of the view.
        /// </summary>
        internal void UpdateView()
        {
            if (WeatherReport != null && ItemsCount > 0)
            {
                int percentileValue = ItemsCount + 1;
                double actualValue = (double)Index / percentileValue;
                double percentile = (double)ItemsCount / percentileValue;
                this.Opacity = 1 - percentile + actualValue;

                if (this.Parent is CollectionView collectionView)
                {
                    this.WidthRequest = collectionView.Width / ItemsCount;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the items count with respect to the parent collection.
        /// </summary>
        /// <param name="itemsSource">Indicates the items source of <see cref="CollectionView"/>.</param>
        private void UpdateItemsCount(IEnumerable itemsSource)
        {
            ItemsCount = itemsSource.ToList<object>().ToList().Count;
            this.UpdateView();
        }

        #endregion

        #region Callback Methods

        private void CollectionView_SizeChanged(object sender, EventArgs e)
        {
            CollectionView collectionView = sender as CollectionView;
            //To avoid the repeated layout arrangement when device orientation is changed.
            if (previousWidth != collectionView.Width || previousHeight != collectionView.Height)
            {
                this.previousWidth = collectionView.Width;
                this.previousHeight = collectionView.Height;
                this.UpdateView();
            }
        }

        #endregion

        #region Dispose Methods

        public void Dispose()
        {
            if (this.Parent is CollectionView collectionView)
            {
                collectionView.SizeChanged -= CollectionView_SizeChanged;
            }
        }

        #endregion
    }
}