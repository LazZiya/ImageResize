using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// scale image size down till both width and height are in the target image size
    /// keep target image aspect ratio = original image aspect ratio
    /// </summary>
    public class Scale : IResizeMethod
    {
        private Image _img { get; set; }
        private Size _targetSize { get; set; }
        private Point _origin { get; set; }

        public Rectangle SourceRect => new Rectangle(_origin, _img.Size);

        public Rectangle TargetRect => new Rectangle(_origin, _targetSize);

        public Scale(Image img, Size targetSize)
        {
            _img = img;
            _origin = new Point(0, 0);
            _targetSize = ScaledImageSize(img.Size, targetSize);
        }

        private Size ScaledImageSize(Size imgSize, Size targetSize)
        {
            float orgRatio = (float)imgSize.Width / (float)imgSize.Height;

            // if new image size is larger than original then use original image size 
            var _newWidth = (float)targetSize.Width;
            var _newHeight = (float)targetSize.Height;

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
