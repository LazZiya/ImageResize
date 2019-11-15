using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// Defines the image resize / crop method
    /// </summary>
    public class Crop : IResizeMethod
    {
        /// <summary>
        /// The rectangle info to read from the source image, the whole image size by default
        /// </summary>
        public Rectangle SourceRect => new Rectangle(SourceOrigin, SourceSize);

        /// <summary>
        /// The target image size
        /// </summary>
        public Rectangle TargetRect => new Rectangle(TargetOrigin, TargetSize);

        /// <summary>
        /// The origin point to start reading from the source image
        /// </summary>
        private Point SourceOrigin { get; set; }

        /// <summary>
        /// The origin point to start writing the target image
        /// </summary>
        private Point TargetOrigin { get; set; }

        /// <summary>
        /// Source image size
        /// </summary>
        private Size SourceSize { get; set; }

        /// <summary>
        /// Target image size
        /// </summary>
        private Size TargetSize { get; set; }

        /// <summary>
        /// Crop an image according to the specified values
        /// </summary>
        /// <param name="imgSize">The source image size</param>
        /// <param name="targetSize">The target image size</param>
        /// <param name="targetSpot">The pre-defined spot of the source image to read and crop.
        /// <see cref="TargetSpot"/></param>
        public Crop(Size imgSize, Size targetSize, TargetSpot targetSpot)
        {
            TargetSize = targetSize;
            SourceSize = targetSize;

            SourceOrigin = ResizeMethods.SourceOrigin.GetSourceOrigin(imgSize, targetSize, targetSpot);
            TargetOrigin = new Point(0, 0);
        }
    }
}
