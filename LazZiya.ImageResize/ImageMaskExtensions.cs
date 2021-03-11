using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Add image mask
    /// </summary>
    public static class ImageMaskExtensions
    {
        /// <summary>
        /// Add image mask with the specified paameters
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rectangle"></param>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static Image Mask(this Image img, Rectangle rectangle, ImageFrameShape shape)
        {
            var bitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

            using (var g = Graphics.FromImage(bitmap))
            {
                using (var gp = new GraphicsPath())
                {
                    g.Clear(Color.Transparent);

                    g.SmoothingMode = SmoothingMode.HighQuality;

                    switch (shape)
                    {
                        case ImageFrameShape.Ellipse: gp.AddEllipse(rectangle); break;
                        default: gp.AddRectangle(rectangle); break;
                    }

                    Brush brush = new TextureBrush(img);
                    g.FillPath(brush, gp);
                }
            }

            return bitmap;
        }
    }
}
