using System.Collections.Generic;
using System.Drawing;

namespace LazZiya.ImageResize.Animated
{

    /// <summary>
    /// Add an animated image watermark to static image adn return an AnimatedImage, depending on a condition parameter.
    /// </summary>
    public static class ImageAnimatedImageWatermarkConditional
    {
        /// <summary>
        /// Draw animated image watermark over a static image, depending on a condition parameter.
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add animated image watermark, false will return the img as AnimatedImage</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        public static AnimatedImage AddAnimatedImageWatermarkIf(this Image img, bool condition, string wmImgPath)
        {
            return condition ? ImageAnimatedImageWatermark.AddAnimatedImageWatermark(img, wmImgPath) : new AnimatedImage(new[] { img });
        }

        /// <summary>
        /// Draw animated image watermark over a static image, depending on a condition parameter.
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add animated image watermark, false will return the img as AnimatedImage</param>
        /// <param name="wmImage">Watermark image</param>
        public static AnimatedImage AddAnimatedImageWatermarkIf(this Image img, bool condition, AnimatedImage wmImage)
        {
            return condition ? ImageAnimatedImageWatermark.AddAnimatedImageWatermark(img, wmImage) : new AnimatedImage(new[] { img });
        }

        /// <summary>
        /// Draw animated image watermark over a static image, depending on a condition parameter.
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add animated image watermark, false will return the img as AnimatedImage</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedImageWatermarkIf(this Image img, bool condition, string wmImgPath, ImageWatermarkOptions ops)
        {
            return condition ? ImageAnimatedImageWatermark.AddAnimatedImageWatermark(img, wmImgPath, ops) : new AnimatedImage(new[] { img });
        }

        /// <summary>
        /// Draw animated image watermark over a static image, depending on a condition parameter.
        /// <para>Notice regarding watermark opacity:</para>
        /// <para>If watermark image needs to be resized, first resize the watermark image, 
        /// then save it to the disc, and read it again with Image.FromFile.</para>
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="condition">true to add animated image watermark, false will return the img as AnimatedImage</param>
        /// <param name="wmImage">Watermak image</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedImageWatermarkIf(this Image img, bool condition, AnimatedImage wmImage, ImageWatermarkOptions ops)
        {
            return condition ? ImageAnimatedImageWatermark.AddAnimatedImageWatermark(img, wmImage, ops) : new AnimatedImage(new[] { img });
        }
    }
}
