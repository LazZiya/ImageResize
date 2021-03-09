using System.Drawing;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Add a static image watermark over animated image, depending on a condition parameter.
    /// </summary>
    public static class AnimatedImageImageWatermarkConditional
    {
        /// <summary>
        /// Add a static image watermark over animated image, depending on a condition parameter.
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        public static AnimatedImage AddImageWatermarkIf(this AnimatedImage img, bool condition, string wmImgPath)
        {
            return condition ? AnimatedImageImageWatermark.AddImageWatermark(img, wmImgPath) : img;
        }

        /// <summary>
        /// Add a static image watermark over animated image, depending on a condition parameter.
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImage">Watermark image</param>
        public static AnimatedImage AddImageWatermarkIf(this AnimatedImage img, bool condition, Image wmImage)
        {
            return condition ? AnimatedImageImageWatermark.AddImageWatermark(img, wmImage) : img;
        }

        /// <summary>
        /// Add a static image watermark over animated image, depending on a condition parameter.
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedImage AddImageWatermarkIf(this AnimatedImage img, bool condition, string wmImgPath, ImageWatermarkOptions ops)
        {
            return condition ? AnimatedImageImageWatermark.AddImageWatermark(img, wmImgPath, ops) : img;
        }

        /// <summary>
        /// Add a static image watermark over animated image, depending on a condition parameter.
        /// <para>Notice regarding watermark opacity:</para>
        /// <para>If watermark image needs to be resized, first resize the watermark image, 
        /// then save it to the disc, and read it again with Image.FromFile.</para>
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add image watermark, false will return the img</param>
        /// <param name="wmImage">Watermak image</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedImage AddImageWatermarkIf(this AnimatedImage img, bool condition, Image wmImage, ImageWatermarkOptions ops)
        {
            return condition ? AnimatedImageImageWatermark.AddImageWatermark(img, wmImage, ops) : img;
        }
    }
}
