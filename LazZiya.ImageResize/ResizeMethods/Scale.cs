using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// scale image size down till both width and height are in the target image size
    /// keep target image aspect ratio = original image aspect ratio
    /// </summary>
    public class Scale : IResizeMethod
    {
        private Size _imgSize { get; set; }
        private Size _targetSize { get; set; }
        private Point _origin { get; set; }

        public Rectangle SourceRect => new Rectangle(_origin, _imgSize);

        public Rectangle TargetRect => new Rectangle(_origin, _targetSize);

        public Scale(Size imgSize, Size targetSize)
        {
            _imgSize = imgSize;
            _origin = new Point(0, 0);

            _targetSize
                = targetSize.Height == 0 && targetSize.Width > 0 ? ScaleByWidth(imgSize, targetSize.Width)
                : targetSize.Width == 0 && targetSize.Height > 0 ? ScaleByHeight(imgSize, targetSize.Height)
                : AutoScale(imgSize, targetSize);
        }

        private Size ScaleByWidth(Size size, float width)
        {
            var ratio = (float)size.Width / (float)size.Height;
            return new Size((int)width, (int)(width / ratio));
        }

        private Size ScaleByHeight(Size size, float height)
        {
            var ratio = (float)size.Width / (float)size.Height;
            return new Size((int)(height * ratio), (int)height);
        }

        private Size AutoScale(Size imgSize, Size targetSize)
        {
            var orgRatio = (float)imgSize.Width / (float)imgSize.Height;

            // if new image size is larger than original then use original image size 
            var _newWidth = (float)targetSize.Width == 0 ? (float)targetSize.Height * orgRatio : (float)targetSize.Width;

            var _newHeight = (float)targetSize.Height == 0 ? (float)targetSize.Height / orgRatio : (float)targetSize.Height;
            
            // org W > org H ==> contain new W and reduce new H according to org ratio
            if (orgRatio > 1)
                _newHeight = targetSize.Width / orgRatio;
            // org H > org W ==> contain new H and reduce new W according to org ratio
            else
                _newWidth = targetSize.Height * orgRatio;

            return new Size((int)_newWidth, (int)_newHeight);
        }
    }
}
