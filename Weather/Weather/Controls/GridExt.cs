﻿using System;
using System.Collections;
using System.Linq;
using Weather.Helper.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Weather.Models;

namespace Weather.Controls
{
    [Preserve(AllMembers = true)]
    public class GridExt : Grid
    {
        #region Fields

        /// <summary>
        /// Increaments the next index id.
        /// </summary>
        private static int NextId = 0;

        #endregion

        #region Constructor

        public GridExt()
        {
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
            if(this.BindingContext is WeatherReport weatherReport)
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
            if(this.Parent is CollectionView collectionView)
            {
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
    }
}