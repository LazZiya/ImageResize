using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// scale image size down till both width and height are in the target image size
    /// keep target image aspect ratio = original image aspect ratio
    /// </summary>
    public class Scale : IResizeMethod
    {
        private Size ImgSize { get; set; }
        private Size TargetSize { get; set; }
        private Point Origin { get; set; }

        /// <summary>
        /// The source reading rectangle from the source image
        /// </summary>
        public Rectangle SourceRect => new Rectangle(Origin, ImgSize);

        /// <summary>
        /// the target image size and position
        /// </summary>
        public Rectangle TargetRect => new Rectangle(Origin, TargetSize);

        /// <summary>
        /// Scale an image as per given size and keep aspect ratio. 
        /// The final result of the scale may have different width or hight
        /// </summary>
        /// <param name="imgSize"></param>
        /// <param name="targetSize"></param>
        public Scale(Size imgSize, Size targetSize)
        {
            ImgSize = imgSize;
            Origin = new Point(0, 0);

            TargetSize
                = targetSize.Height == 0 && targetSize.Width > 0 ? ScaleByWidth(imgSize, targetSize.Width)
                : targetSize.Width == 0 && targetSize.Height > 0 ? ScaleByHeight(imgSize, targetSize.Height)
                : AutoScale(imgSize, targetSize);
        }

        /// <summary>
        /// Get the height of the scaled image according to its given width
        /// </summary>
        /// <param name="size">The source image size</param>
        /// <param name="width">The desired image width</param>
        /// <returns>Size result of the scaling calculation</returns>
        private Size ScaleByWidth(Size size, float width)
        {
            var ratio = (float)size.Width / (float)size.Height;
            return new Size((int)width, (int)(width / ratio));
        }

        /// <summary>
        /// Get the width of the scaled image according to its given height
        /// </summary>
        /// <param name="size">The source image size</param>
        /// <param name="height">The desired image height</param>
        /// <returns>Size result of the scaling calculation</returns>
        private Size ScaleByHeight(Size size, float height)
        {
            var ratio = (float)size.Width / (float)size.Height;
            return new Size((int)(height * ratio), (int)height);
        }

        /// <summary>
        /// Get new sizes of the image to resize,
        /// The scale calculation will fit the new size till both width and height are contianed,
        /// So the final image is not cropped and completely fits in the new size.
        /// </summary>
        /// <param name="imgSize"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
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
