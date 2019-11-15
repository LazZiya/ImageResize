using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize.Watermark
{
    /// <summary>
    /// Add image watermark over another image
    /// </summary>
    public static class ImageWM
    {
        /// <summary>
        /// Add image watermark over another image.
        /// </summary>
        /// <param name="img">The main image</param>
        /// <param name="wmFileName">full path to the image that will be used as watermark</param>
        /// <param name="spot">The taret spot on the main image to draw the watermark over. See <see cref="TargetSpot"/></param>
        /// <param name="margin">The distance of the watermark image in pixels from the nearest border.</param>
        /// <param name="opacity">The opacity of the watermark image (0 - 100)</param>
        public static void ImageWatermark(this Image img, string wmFileName, TargetSpot spot = TargetSpot.TopRight, int margin = 10, int opacity = 35)
        {
            if (opacity > 0)
            {
                var graphics = Graphics.FromImage(img);

                graphics.SmoothingMode = SmoothingMode.None;
                graphics.CompositingMode = CompositingMode.SourceOver;

                var wmImage = Image.FromFile(wmFileName);

                if (opacity < 100)
                    wmImage = ImageOpacity.ChangeImageOpacity(wmImage, opacity);

                var wmW = wmImage.Width;
                var wmH = wmImage.Height;

                var drawingPoint = ImageWatermarkPos(img.Width, img.Height, wmW, wmH, spot, margin);

                graphics.DrawImage(wmImage, drawingPoint.X, drawingPoint.Y, wmW, wmH);

                graphics.Dispose();
            }
        }

        private static PointF ImageWatermarkPos(int imgWidth, int imgHeight, int wmWidth, int wmHeight, TargetSpot spot, int margin)
        {
            PointF point;

            switch (spot)
            {
                case TargetSpot.BottomLeft: point = new PointF(margin, imgHeight - (wmHeight + margin)); break;
                case TargetSpot.BottomMiddle: point = new PointF((imgWidth / 2) - (wmWidth / 2), imgHeight - (wmHeight + margin)); break;
                case TargetSpot.BottomRight: point = new PointF(imgWidth - (wmWidth + margin), imgHeight - (wmHeight + margin)); break;
                case TargetSpot.MiddleLeft: point = new PointF(margin, (imgHeight / 2) - (wmHeight / 2)); break;
                case TargetSpot.Center: point = new PointF((imgWidth / 2) - (wmWidth / 2), (imgHeight / 2) - (wmHeight / 2)); break;
                case TargetSpot.MiddleRight: point = new PointF(imgWidth - (wmWidth + margin), (imgHeight / 2) - (wmHeight / 2)); break;
                case TargetSpot.TopLeft: point = new PointF(margin, margin); break;
                case TargetSpot.TopMiddle: point = new PointF((imgWidth / 2) - (wmWidth / 2), margin); break;

                case TargetSpot.TopRight:
                default: point = new PointF(imgWidth - (margin + wmWidth), margin); break;
            }

            return point;
        }
    }
}
