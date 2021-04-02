using System;
using System.ComponentModel;
using Weather.Controls;
using Xamarin.Forms;
using Weather.Platform.Mapping;
#if Android
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Weather.Droid.Constants;
using Weather.Droid.Mapping;
using Xamarin.Forms.Platform.Android;
using AndroiView = Android.Views.View;
#elif iOS
using Foundation;
using UIKit;
using Weather.iOS.Constants;
using Weather.iOS.Mapping;
using Xamarin.Forms.Platform.iOS;
#endif

[assembly: ExportRenderer(typeof(SearchBarExt), typeof(SearchBarExtRenderer))]
#if Android
namespace Weather.Droid.Mapping
#elif iOS
namespace Weather.iOS.Mapping
#endif
{
    [Preserve(AllMembers = true)]
    public class SearchBarExtRenderer : SearchBarRenderer
    {
        #region Fields
#if Android
        AndroiView searchPlateView;

        ImageView searchImageView;
#elif iOS
        UITextField textField;

        UIButton clearButton;
#endif
        #endregion

        #region Constructor
#if Android
        public SearchBarExtRenderer(Context context) : base(context)
        {

        }
#elif iOS
        public SearchBarExtRenderer()
        {

        }
#endif
        #endregion

        #region Properties

        public SearchBarExt SearchBarExt
        {
            get => this.Element as SearchBarExt; 
        }

        #endregion

        #region Override Methods

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                this.UpdateUnderLineColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                this.UpdateSearchIconColor();
            }
        }

        #endregion

        #region Private Methods

        private void UpdateUnderLineColor()
        {
            if (Control == null || this.SearchBarExt == null)
                return;
#if Android
            if (searchPlateView == null)
            {
                int searchPlateId = Control.Context.Resources.GetIdentifier(NativeAppConstants.SearchPlate, null, null);
                searchPlateView = Control.FindViewById(searchPlateId);
            }

            searchPlateView.SetBackgroundColor(Color.Transparent.ToAndroid());
#elif iOS
            if (textField == null || clearButton == null)
            {
                NSString _searchField = new NSString(NativeAppConstants.SearchField);
                textField = (UITextField)Control.ValueForKey(_searchField);
                textField.BorderStyle = UITextBorderStyle.None;
                textField.BackgroundColor = UIColor.Clear;
                NSString _clearButton = new NSString(NativeAppConstants.ClearButton);
                clearButton = (UIButton)textField.ValueForKey(_clearButton);
                var imageicon = XamarinExtensions.ConvertFontIconToUIImage(NativeAppConstants.FontAwesomeLight, 20,
                    Color.FromHex(NativeAppConstants.PrimaryColor).ToUIColor(), NativeAppConstants.SearchIconGlyph).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                clearButton.SetImage(imageicon, UIControlState.Normal);
                Control.SearchBarStyle = UISearchBarStyle.Minimal;
                Control.Layer.CornerRadius = 0;
                Control.Layer.BorderWidth = 0;
                Control.Layer.BorderColor = UIColor.White.CGColor;
                Control.SetShowsCancelButton(false, false);
                Control.ShowsCancelButton = false;
                Control.TextChanged += delegate
                {
                    Control.ShowsCancelButton = false;
                };

                this.UpdateSearchIconColor();
            }
#endif
        }

        private void UpdateSearchIconColor()
        {
            var focusColor = this.Element.IsFocused ? Color.FromHex(NativeAppConstants.SecondaryColor) : Color.FromHex(NativeAppConstants.PrimaryColor);
            var nativeColor = focusColor.GetNativePlatformColor();
#if Android
            if (searchImageView == null)
            { 
                int imageSearchId = Control.Context.Resources.GetIdentifier(NativeAppConstants.SearchImageIcon, null, null);
                searchImageView = (ImageView)Control.FindViewById(imageSearchId);
            }

            searchImageView.SetColorFilter(nativeColor);
#elif iOS
            if (textField != null)
                textField.LeftView.TintColor = nativeColor;

            if (clearButton != null)
                clearButton.TintColor = nativeColor;
#endif
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
#if Android
            searchPlateView = null;
            searchImageView = null;
#elif iOS
            textField = null;
            clearButton = null;
#endif
        }

        #endregion
    }
}