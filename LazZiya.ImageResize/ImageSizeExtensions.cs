using System.Drawing;

namespace LazZiya.ImageResize
{
    internal static class ImageSizeExtensions
    {
        internal static Size ConfirmNewSizeValues(this Image img, int newWidth, int newHeight)
        {
            if (newWidth == 0 && newHeight == 0)
            {
                newWidth = img.Width;
                newHeight = img.Height;
            }
            else if (newWidth <= 0 && newHeight > 0)
            {
                // if only height is defined, then find new width and keep aspect ratio
                newWidth = GetNewWidth(img.Width, img.Height, newHeight);
            }
            else if (newWidth > 0 && newHeight <= 0)
            {
                // if only width is defined, then find new height and keep aspect ratio
                newHeight = GetNewHeight(img.Width, img.Height, newWidth);
            }

            return new Size(newWidth, newHeight);
        }

        private static int GetNewHeight(int imgWidth, int imgHeight, int newWidth)
        {
            double fracioanlPercentage = (double)newWidth / (double)imgWidth;

            return (int)(imgHeight * fracioanlPercentage);
        }

        private static int GetNewWidth(int imgWidth, int imgHeight, int newHeight)
        {
            double fractionalPercentage = (double)newHeight / (double)imgHeight;

            return (int)(imgWidth * fractionalPercentage);
        }

        /// <summary>
        /// define x and y of source rect to read the image data from original image depending on CroppingSpot
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="spot"></param>
        /// <returns></returns>
        internal static Point GetCroppingPos(this Image img, Size size, TargetSpot spot)
        {
            float x, y = 0;
            switch (spot)
            {
                case TargetSpot.Center: x = (img.Width - size.Width) / 2; y = (img.Height - size.Height) / 2; break;
                case TargetSpot.TopMiddle: x = (img.Width - size.Width) / 2; y = 0; break;
                case TargetSpot.BottomMiddle: x = (img.Width - size.Width) / 2; y = img.Height - size.Height; break;

                case TargetSpot.MiddleRight: x = img.Width - size.Width; y = (img.Height - size.Height) / 2; break;
                case TargetSpot.BottomRight: x = img.Width - size.Width; y = img.Height - size.Height; break;
                case TargetSpot.TopRight: x = img.Width - size.Width; y = 0; break;

                case TargetSpot.MiddleLeft: x = 0; y = (img.Height - size.Height) / 2; break;
                case TargetSpot.BottomLeft: x = 0; y = img.Height - size.Height * 1; break;
                case TargetSpot.TopLeft:
                default: x = 0; y = 0; break;
            }

            return new Point((int)x, (int)y);
        }

        /// <summary>
        /// define the max rect size and pos to read from source image
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        internal static Size SourceSize(this Image img, Size size, TargetSpot spot)
        {
            // aspect ratio of the resized image
            float croppedRatio = (float)size.Width / (float)size.Height;

            // aspect ratio of original image
            float orgRatio = (float)img.Width / (float)img.Height;

            // find aspect ratio difference
            float diffRatio = croppedRatio / orgRatio;

            var _srcWidth = (float)img.Width;
            var _srcHeight = (float)img.Height;

            if (diffRatio > 1)
                // new W > org W ==> crop org H
                _srcHeight = _srcWidth / croppedRatio;
            else
                // new H > org H ==> crop org W
                _srcWidth = _srcHeight * croppedRatio;

            return new Size((int)_srcWidth, (int)_srcHeight);
        }


        /// <summary>
        /// re-scale newWidth / newHeight to match original image aspect ratio,
        /// so the resized image will have similar aspect raio with the original one.
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        internal static Size ScaledImageSize(this Image img, Size size)
        {
            float orgRatio = (float)img.Width / (float)img.Height;

            // if new image size is larger than original then use original image size 
            var _newWidth = (float)size.Width;
            var _newHeight = (float)size.Height;

            // org W > org H ==> contain new W and reduce new H according to org ratio
            if (orgRatio > 1)
                _newHeight = size.Width / orgRatio;
            // org H > org W ==> contain new H and reduce new W according to org ratio
            else
                _newWidth = size.Height * orgRatio;

            return new Size((int)_newWidth, (int)_newHeight);
        }
    }
}
