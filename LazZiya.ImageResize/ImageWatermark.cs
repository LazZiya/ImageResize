using LazZiya.ImageResize.Tools;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Draw image watermark
    /// </summary>
    public static class ImageWatermark
    {

        /// <summary>
        /// Draw image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        public static Image AddImageWatermark(this Image img, string wmImgPath)
        {
            var wm = Image.FromFile(wmImgPath);
            return img.AddImageWatermark(wm, new ImageWatermarkOptions());
        }

        /// <summary>
        /// Draw image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImage">Watermark image</param>
        public static Image AddImageWatermark(this Image img, Image wmImage)
        {
            return img.AddImageWatermark(wmImage, new ImageWatermarkOptions());
        }

        /// <summary>
        /// Draw image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static Image AddImageWatermark(this Image img, string wmImgPath, ImageWatermarkOptions ops)
        {
            var wm = Image.FromFile(wmImgPath);
            return img.AddImageWatermark(wm, ops);
        }

        /// <summary>
        /// Draw image watermark.
        /// <para>Notice regarding watermark opacity:</para>
        /// <para>If watermark image needs to be resized, first resize the watermark image, 
        /// then save it to the disc, and read it again with Image.FromFile.</para>
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImage">Watermak image</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static Image AddImageWatermark(this Image img, Image wmImage, ImageWatermarkOptions ops)
        {
            if (ops.Opacity > 0)
            {
                using (var graphics = Graphics.FromImage(img))
                {
                    graphics.SmoothingMode = SmoothingMode.None;
                    graphics.CompositingMode = CompositingMode.SourceOver;

                    if (ops.Opacity < 100)
                        wmImage = ImageOpacity.ChangeImageOpacityMethod1(wmImage, ops.Opacity);

                    var wmW = wmImage.Width;
                    var wmH = wmImage.Height;

                    var drawingPoint = ImageWatermarkPosition.ImageWatermarkPos(img.Width, img.Height, wmW, wmH, ops.Location, ops.Margin);

                    graphics.DrawImage(wmImage, drawingPoint.X, drawingPoint.Y, wmW, wmH);
                }
            }

            wmImage.Dispose();
            return img;
        }
    }
}
