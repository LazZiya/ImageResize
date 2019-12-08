using System.Drawing;

namespace LazZiya.ImageResize.Tools
{
    internal abstract class TextWatermarkPosition
    {
        /// <summary>
        /// Calculate the watermark text background size and position according to the taret spot, 
        /// main image size and font size.
        /// </summary>
        /// <param name="imgWidth">Main image width</param>
        /// <param name="imgHeight">Main image height</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="spot">target spot</param>
        /// <param name="margin">Distance from the nearest border</param>
        /// <returns></returns>
        internal static Rectangle SetBGPos(int imgWidth, int imgHeight, int fontSize, TargetSpot spot, int margin)
        {
            Rectangle rect;

            var bgHeight = fontSize * 2;

            switch (spot)
            {
                case TargetSpot.TopLeft:
                case TargetSpot.TopMiddle:
                case TargetSpot.TopRight:
                    rect = new Rectangle(0, margin, imgWidth, bgHeight);
                    break;

                case TargetSpot.MiddleLeft:
                case TargetSpot.MiddleRight:
                case TargetSpot.Center:
                    rect = new Rectangle(0, imgHeight / 2 - bgHeight / 2, imgWidth, bgHeight);
                    break;

                case TargetSpot.BottomLeft:
                case TargetSpot.BottomMiddle:
                case TargetSpot.BottomRight:
                default:
                    rect = new Rectangle(0, imgHeight - bgHeight - margin, imgWidth, bgHeight);
                    break;
            }

            return rect;
        }

        internal static int SetTextAlign(SizeF textMetrics, int imgWidth, TargetSpot spot)
        {
            int space;
            switch (spot)
            {
                case TargetSpot.BottomMiddle:
                case TargetSpot.TopMiddle:
                case TargetSpot.Center:
                    space = (int)(imgWidth - textMetrics.Width) / 2; break;

                case TargetSpot.BottomRight:
                case TargetSpot.MiddleRight:
                case TargetSpot.TopRight:
                    space = (int)(imgWidth - textMetrics.Width) - 5; break;

                case TargetSpot.BottomLeft:
                case TargetSpot.MiddleLeft:
                case TargetSpot.TopLeft:
                default: space = 5; break;
            }

            return space;
        }
    }
}
