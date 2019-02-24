using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize.Watermark
{
    public static class ImageWM
    {
        /// <summary>
        /// add image watermark over the uploaded image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="wmFileName"></param>
        /// <param name="spot"></param>
        /// <param name="stickToBorder"></param>
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
