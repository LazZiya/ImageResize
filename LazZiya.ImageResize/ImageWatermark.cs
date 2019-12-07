using LazZiya.ImageResize.Tools;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImageWatermark
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="wmImgPath"></param>
        public static Image AddImageWatermark(this Image img, string wmImgPath)
        {
            var wm = Image.FromFile(wmImgPath);
            return img.AddImageWatermark(wm, new ImageWatermarkOptions());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="wmImage"></param>
        public static Image AddImageWatermark(this Image img, Image wmImage)
        {
            return img.AddImageWatermark(wmImage, new ImageWatermarkOptions());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="wmImgPath"></param>
        /// <param name="ops"></param>
        public static Image AddImageWatermark(this Image img, string wmImgPath, ImageWatermarkOptions ops)
        {
            var wm = Image.FromFile(wmImgPath);
            return img.AddImageWatermark(wm, ops);
        }

        /// <summary>
        /// Add image watermark
        /// </summary>
        /// <param name="img">The main image</param>
        /// <param name="wmImage">full path to the image that will be used as watermark</param>
        /// <param name="ops">Image watermark options</param>
        public static Image AddImageWatermark(this Image img, Image wmImage, ImageWatermarkOptions ops)
        {
            if (ops.Opacity > 0)
            {
                var graphics = Graphics.FromImage(img);

                graphics.SmoothingMode = SmoothingMode.None;
                graphics.CompositingMode = CompositingMode.SourceOver;

                if (ops.Opacity < 100)
                    wmImage = ImageOpacity.ChangeImageOpacityMethod1(wmImage, ops.Opacity);

                var wmW = wmImage.Width;
                var wmH = wmImage.Height;

                var drawingPoint = ImageWatermarkPosition.ImageWatermarkPos(img.Width, img.Height, wmW, wmH, ops.Location, ops.Margin);

                graphics.DrawImage(wmImage, drawingPoint.X, drawingPoint.Y, wmW, wmH);

                graphics.Dispose();
            }

            return img;
        }
    }
}
