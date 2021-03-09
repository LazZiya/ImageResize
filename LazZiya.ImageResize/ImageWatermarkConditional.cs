using LazZiya.ImageResize.Tools;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Draw image watermark depending on a conditional parameter
    /// </summary>
    public static class ImageWatermarkConditional
    {

        /// <summary>
        /// Draw image watermark if the condition is true, otherwise return without image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        public static Image AddImageWatermarkIf(this Image img, bool condition, string wmImgPath)
        {
            return condition ? ImageWatermark.AddImageWatermark(img, wmImgPath) : img;
        }

        /// <summary>
        /// Draw image watermark if the condition is true, otherwise return without image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImage">Watermark image</param>
        public static Image AddImageWatermarkIf(this Image img, bool condition, Image wmImage)
        {
            return condition ? ImageWatermark.AddImageWatermark(img, wmImage) : img;
        }

        /// <summary>
        /// Draw image watermark if the condition is true, otherwise return without image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static Image AddImageWatermarkIf(this Image img, bool condition, string wmImgPath, ImageWatermarkOptions ops)
        {
            return condition ? ImageWatermark.AddImageWatermark(img, wmImgPath, ops) : img;
        }

        /// <summary>
        /// Draw image watermark if the condition is true, otherwise return without image watermark
        /// <para>Notice regarding watermark opacity:</para>
        /// <para>If watermark image needs to be resized, first resize the watermark image, 
        /// then save it to the disc, and read it again with Image.FromFile.</para>
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImage">Watermak image</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        /// <param name="disposeWaterMark">Optional: dispose watermar image after finishing. Default: true</param>
        public static Image AddImageWatermarkIf(this Image img, bool condition, Image wmImage, ImageWatermarkOptions ops, bool disposeWaterMark = true)
        {
            return condition ? ImageWatermark.AddImageWatermark(img, wmImage, ops, disposeWaterMark) : img;
        }
    }
}
