#if Android
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using UIColor = Android.Graphics.Color;
#elif iOS
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
#endif
using Color = Xamarin.Forms.Color;

namespace Weather.Platform.Mapping
{
    [Preserve(AllMembers = true)]
    internal static partial class XamarinExtensions
    {
        #region Methods

        internal static UIColor GetNativePlatformColor(this Color color)
        {
#if Android
            return color.ToAndroid();
#elif iOS
            return color.ToUIColor();
#endif
        }

#if iOS
        internal static UIImage ConvertFontIconToUIImage(string fontFamily, double fontSize, UIColor iconcolor, string glyph)
        {
            var cleansedname = CleanseFontName(fontFamily);
            var font = UIFont.FromName(cleansedname ?? string.Empty, (float)fontSize) ?? UIFont.SystemFontOfSize((float)fontSize);
            var attString = new NSAttributedString(glyph, font: font, foregroundColor: iconcolor);
            var imagesize = ((NSString)glyph).GetSizeUsingAttributes(attString.GetUIKitAttributes(0, out _));
            UIGraphics.BeginImageContextWithOptions(imagesize, false, 0f);
            var ctx = new NSStringDrawingContext();
            var boundingRect = attString.GetBoundingRect(imagesize, 0, ctx);
            attString.DrawString(new System.Drawing.RectangleF((float)(imagesize.Width / 2 - boundingRect.Size.Width / 2), (float)(imagesize.Height / 2 - boundingRect.Size.Height / 2), (float)imagesize.Width, (float)imagesize.Height));
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        internal static string CleanseFontName(string fontName)
        {
            var (hasFontAlias, fontPostScriptName) = Xamarin.Forms.Internals.FontRegistrar.HasFont(fontName);
            if (hasFontAlias)
                return fontPostScriptName;

            var fontFile = FontFile.FromString(fontName);
            if (!string.IsNullOrWhiteSpace(fontFile.Extension))
            {
                var (hasFont, filePath) = Xamarin.Forms.Internals.FontRegistrar.HasFont(fontFile.FileNameWithExtension());
                if (hasFont)
                    return filePath ?? fontFile.PostScriptName;
            }
            else
            {
                foreach (var ext in FontFile.Extensions)
                {
                    var formated = fontFile.FileNameWithExtension(ext);
                    var (hasFont, filePath) = Xamarin.Forms.Internals.FontRegistrar.HasFont(formated);
                    if (hasFont)
                        return filePath;
                }
            }

            return fontFile.PostScriptName;
        }
#endif

        #endregion
    }
}