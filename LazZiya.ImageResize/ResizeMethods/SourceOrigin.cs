using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// calculate the source origin point from image 
    /// </summary>
    internal class SourceOrigin
    {
        internal static Point GetSourceOrigin(Size imgSize, Size targetSize, TargetSpot targetSpot)
        {
            float x, y = 0;
            switch (targetSpot)
            {
                case TargetSpot.Center: x = (imgSize.Width - targetSize.Width) / 2; y = (imgSize.Height - targetSize.Height) / 2; break;
                case TargetSpot.TopMiddle: x = (imgSize.Width - targetSize.Width) / 2; y = 0; break;
                case TargetSpot.BottomMiddle: x = (imgSize.Width - targetSize.Width) / 2; y = imgSize.Height - targetSize.Height; break;

                case TargetSpot.MiddleRight: x = imgSize.Width - targetSize.Width; y = (imgSize.Height - targetSize.Height) / 2; break;
                case TargetSpot.BottomRight: x = imgSize.Width - targetSize.Width; y = imgSize.Height - targetSize.Height; break;
                case TargetSpot.TopRight: x = imgSize.Width - targetSize.Width; y = 0; break;

                case TargetSpot.MiddleLeft: x = 0; y = (imgSize.Height - targetSize.Height) / 2; break;
                case TargetSpot.BottomLeft: x = 0; y = imgSize.Height - targetSize.Height * 1; break;
                case TargetSpot.TopLeft:
                default: x = 0; y = 0; break;
            }

            return new Point((int)x, (int)y);
        }
    }
}
