using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// scale down image size till one of width or height are equal to target image size
    /// extra pixels will be cropped out
    /// target image aspect ratio is equal to defined new size aspect ratio
    /// </summary>
    public class ScaleAndCrop : IResizeMethod
    {
        private Size SourceSize { get; set; }
        private Size TargetSize { get; set; }

        private Point SourceOrigin { get; set; }
        private Point TargetOrigin { get; set; }

        /// <summary>
        /// The source reading rectangle from the source image
        /// </summary>
        public Rectangle SourceRect => new Rectangle(SourceOrigin, SourceSize);

        /// <summary>
        /// the target image size and position
        /// </summary>
        public Rectangle TargetRect => new Rectangle(TargetOrigin, TargetSize);

        /// <summary>
        /// Scale and crop the image,
        /// If the final width or heghit is out of the target area it will be cropped out.
        /// </summary>
        /// <param name="imgSize">Source image size</param>
        /// <param name="targetSize">Target image size</param>
        /// <param name="targetSpot">The target spot to read from the source image. See <see cref="TargetSpot"/></param>
        public ScaleAndCrop(Size imgSize, Size targetSize, TargetSpot targetSpot)
        {
            TargetSize = targetSize;
            TargetOrigin = new Point(0, 0);

            SourceSize = GetSourceSize(imgSize, targetSize);
            SourceOrigin = ResizeMethods.SourceOrigin.GetSourceOrigin(imgSize, SourceSize, targetSpot);
        }

        /// <summary>
        /// define the max rect size and pos to read from source image
        /// </summary>
        private Size GetSourceSize(Size imgSize, Size targetSize)
        {
            // aspect ratio of the resized image
            float croppedRatio = (float)targetSize.Width / (float)targetSize.Height;

            // aspect ratio of original image
            float orgRatio = (float)imgSize.Width / (float)imgSize.Height;

            // find aspect ratio difference
            float diffRatio = croppedRatio / orgRatio;

            var _srcWidth = (float)imgSize.Width;
            var _srcHeight = (float)imgSize.Height;

            if (diffRatio > 1)
                // new W > org W ==> crop org H
                _srcHeight = _srcWidth / croppedRatio;
            else
                // new H > org H ==> crop org W
                _srcWidth = _srcHeight * croppedRatio;

            return new Size((int)_srcWidth, (int)_srcHeight);
        }
    }
}
