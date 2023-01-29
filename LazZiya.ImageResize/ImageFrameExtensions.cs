using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Draw frame, background, etc...
    /// </summary>
    public static class ImageFrameExtensions
    {
        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Image AddFrame(this Image img)
        {
            return img.AddFrame(img.Width, img.Height, new ImageFrameOptions());
        }

        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Image AddFrame(this Image img, ImageFrameOptions options)
        {
            return img.AddFrame(img.Width, img.Height, options);
        }

        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <returns></returns>
        public static Image AddFrame(this Image img, int frameWidth, int frameHeight)
        {
            return img.AddFrame(frameWidth, frameHeight, new ImageFrameOptions());
        }

        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Image AddFrame(this Image img, int frameWidth, int frameHeight, ImageFrameOptions options)
        {
            // Create an empty background to grant the additional border space
            var bg = new Bitmap(frameWidth + options.Thickness, frameHeight + options.Thickness);

            // Get the origin where to start drawing the frame
            var origin = options.Thickness == 1 ? 1 : options.Thickness / 2;

            // Prepare frame rect
            var rect = new Rectangle(origin, origin, bg.Width - options.Thickness, bg.Height - options.Thickness);

            // Add image mask to hide parts outside the frame
            //img.Mask(rect, options.FrameShape);

            // Clear the pixels inside the new bg
            using (var g = Graphics.FromImage(bg))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);

                using (var pen = new Pen(options.FrameColor))
                {
                    pen.Width = options.Thickness;
                    pen.LineJoin = options.LineJoin;
                    pen.DashStyle = options.DashStyle;

                    if (options.LineJoin == LineJoin.Miter || options.LineJoin == LineJoin.MiterClipped)
                    {
                        pen.MiterLimit = options.MitterLimit;
                    }

                    if (options.DashStyle == DashStyle.Custom)
                    {
                        pen.DashPattern = options.DashPattern;
                    }

                    if (options.Thickness >= 0)
                    {
                        var brush = new SolidBrush(options.FillColor);
                        switch (options.FrameShape)
                        {
                            case ImageFrameShape.Ellipse:
                                g.DrawEllipse(pen, rect);
                                g.FillEllipse(brush, rect);
                                break;
                            default:
                                g.DrawRectangle(pen, rect);
                                g.FillRectangle(brush, rect);
                                break;
                        }
                    }
                }
            }

            // Use AddImageWatermark to draw the front image inside the border
            return bg.AddImageWatermark(img, new ImageWatermarkOptions { Location = TargetSpot.Center });
        }
    }
}
