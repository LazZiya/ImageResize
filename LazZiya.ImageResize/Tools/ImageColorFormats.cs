using LazZiya.ImageResize.ColorFormats;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize.Tools
{  
    /// <summary>
    /// Detect image color format.
    /// <para>https://stackoverflow.com/a/9899904/5519026</para>
    /// </summary>
    public static class ImageColorFormats
    {
        /// <summary>
        /// Get image color format
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageColorFormat GetColorFormat(this Bitmap bitmap)
        {
            // Check image flags
            var flags = (ImageFlags)bitmap.Flags;
            if (flags.HasFlag(ImageFlags.ColorSpaceCmyk) || flags.HasFlag(ImageFlags.ColorSpaceYcck))
            {
                return ImageColorFormat.Cmyk;
            }
            else if (flags.HasFlag(ImageFlags.ColorSpaceGray))
            {
                return ImageColorFormat.Grayscale;
            }

            // Check pixel format
            var pixelFormat = (int)bitmap.PixelFormat;
            if (pixelFormat == (int)ImagePixelFormat.PixelFormat32bppCMYK)
            {
                return ImageColorFormat.Cmyk;
            }
            else if ((pixelFormat & (int)ImagePixelFormat.PixelFormatIndexed) != 0)
            {
                return ImageColorFormat.Indexed;
            }
            else if (pixelFormat == (int)ImagePixelFormat.PixelFormat16bppGrayScale)
            {
                return ImageColorFormat.Grayscale;
            }

            // Default to RGB
            return ImageColorFormat.Rgb;
        }
    }
}
