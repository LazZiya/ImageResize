using System.Drawing;

namespace LazZiya.ImageResize.Tools
{
    internal abstract class ImageWatermarkPosition
    {
        internal static PointF ImageWatermarkPos(int imgWidth, int imgHeight, int wmWidth, int wmHeight, TargetSpot spot, int margin)
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
