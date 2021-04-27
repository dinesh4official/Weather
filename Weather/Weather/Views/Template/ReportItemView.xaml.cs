using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Views.Template
{
    [Preserve(AllMembers = true)]
    public partial class ReportItemView : StackLayout
    {
        #region Bindable Properties

        /// <summary>
        /// Bindable property for <see cref="Source"/>
        /// </summary>
        public static readonly BindableProperty SourceProperty =
             BindableProperty.Create(nameof(Source), typeof(ImageSource), typeof(ReportItemView),null);

        /// <summary>
        /// Bindable property for <see cref="ItemTitle"/>
        /// </summary>
        public static readonly BindableProperty ItemTitleProperty =
             BindableProperty.Create(nameof(ItemTitle), typeof(string), typeof(ReportItemView), string.Empty);

        /// <summary>
        /// Bindable property for <see cref="ItemValue"/>
        /// </summary>
        public static readonly BindableProperty ItemValueProperty =
             BindableProperty.Create(nameof(ItemValue), typeof(string), typeof(ReportItemView), string.Empty);

        #endregion

        #region Constructor

        public ReportItemView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the icon of the weather.
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title of the weather.
        /// </summary>
        public string ItemTitle
        {
            get { return (string)GetValue(ItemTitleProperty); }
            set { SetValue(ItemTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the actual value of the weather.
        /// </summary>
        public string ItemValue
        {
            get { return (string)GetValue(ItemValueProperty); }
            set { SetValue(ItemValueProperty, value); }
        }

        #endregion
    }
}
