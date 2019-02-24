using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    public class Crop : IResizeMethod
    {
        public Rectangle SourceRect => new Rectangle(_sourceOrigin, _sourceSize);
        public Rectangle TargetRect => new Rectangle(_targetOrigin, _targetSize);

        private Image _img { get; set; }

        private Point _sourceOrigin { get; set; }
        private Point _targetOrigin { get; set; }

        private Size _sourceSize { get; set; }
        private Size _targetSize { get; set; }

        public Crop(Image img, Size targetSize, TargetSpot targetSpot)
        {
            _img = img;

            _targetSize = targetSize;
            _sourceSize = targetSize;

            _sourceOrigin = SourceOrigin.GetSourceOrigin(img.Size, targetSize, targetSpot);
            _targetOrigin = new Point(0, 0);
        }
    }
}
