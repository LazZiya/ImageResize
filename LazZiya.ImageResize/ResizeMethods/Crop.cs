using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    public class Crop : IResizeMethod
    {
        public Rectangle SourceRect => new Rectangle(_sourceOrigin, _sourceSize);
        public Rectangle TargetRect => new Rectangle(_targetOrigin, _targetSize);

        private Point _sourceOrigin { get; set; }
        private Point _targetOrigin { get; set; }

        private Size _sourceSize { get; set; }
        private Size _targetSize { get; set; }

        public Crop(Size imgSize, Size targetSize, TargetSpot targetSpot)
        {
            _targetSize = targetSize;
            _sourceSize = targetSize;

            _sourceOrigin = SourceOrigin.GetSourceOrigin(imgSize, targetSize, targetSpot);
            _targetOrigin = new Point(0, 0);
        }
    }
}
