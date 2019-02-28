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
        private Size _sourceSize { get; set; }
        private Size _targetSize { get; set; }

        private Point _sourceOrigin { get; set; }
        private Point _targetOrigin { get; set; }

        public Rectangle SourceRect => new Rectangle(_sourceOrigin, _sourceSize);
        public Rectangle TargetRect => new Rectangle(_targetOrigin, _targetSize);

        public ScaleAndCrop(Size imgSize, Size targetSize, TargetSpot targetSpot)
        {
            _targetSize = targetSize;
            _targetOrigin = new Point(0, 0);

            _sourceSize = SourceSize(imgSize, targetSize);
            _sourceOrigin = SourceOrigin.GetSourceOrigin(imgSize, _sourceSize, targetSpot);
        }

        /// <summary>
        /// define the max rect size and pos to read from source image
        /// </summary>
        private Size SourceSize(Size imgSize, Size targetSize)
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
