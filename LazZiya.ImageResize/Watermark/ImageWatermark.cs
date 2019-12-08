using LazZiya.ImageResize.Tools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize.Watermark
{
    /// <summary>
    /// Add image watermark over another image
    /// </summary>
    public static partial class Watermark
    {
        /// <summary>
        /// Add image watermark over another image.
        /// </summary>
        /// <param name="img">The main image</param>
        /// <param name="wmFileName">full path to the image that will be used as watermark</param>
        /// <param name="spot">The taret spot on the main image to draw the watermark over. See <see cref="TargetSpot"/></param>
        /// <param name="margin">The distance of the watermark image in pixels from the nearest border.</param>
        /// <param name="opacity">The opacity of the watermark image (0 - 100)</param>
        [Obsolete("This mehtod is obsolete and will be removed in a feature release, use AddImageWatermark instead.")]
        public static void ImageWatermark(this Image img, string wmFileName, TargetSpot spot = TargetSpot.TopRight, int margin = 10, int opacity = 35)
        {
            if (opacity > 0)
            {
                var graphics = Graphics.FromImage(img);

                graphics.SmoothingMode = SmoothingMode.None;
                graphics.CompositingMode = CompositingMode.SourceOver;

                var wmImage = Image.FromFile(wmFileName);

                if (opacity < 100)
                    wmImage = ImageOpacity.ChangeImageOpacityMethod1(wmImage, opacity);

                var wmW = wmImage.Width;
                var wmH = wmImage.Height;

                var drawingPoint = ImageWatermarkPosition.ImageWatermarkPos(img.Width, img.Height, wmW, wmH, spot, margin);

                graphics.DrawImage(wmImage, drawingPoint.X, drawingPoint.Y, wmW, wmH);

                graphics.Dispose();
            }
        }
    }
}
