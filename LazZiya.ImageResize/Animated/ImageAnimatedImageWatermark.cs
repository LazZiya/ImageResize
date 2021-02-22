using System.Collections.Generic;
using System.Drawing;

namespace LazZiya.ImageResize.Animated
{

    /// <summary>
    /// Add an animated image watermark to static image
    /// </summary>
    public static class ImageAnimatedImageWatermark
    {
        /// <summary>
        /// Draw animated image watermark over a static image
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        public static AnimatedImage AddAnimatedImageWatermark(this Image img, string wmImgPath)
        {
            var wm = AnimatedImage.FromFile(wmImgPath);
            return img.AddAnimatedImageWatermark(wm, new ImageWatermarkOptions());
        }

        /// <summary>
        /// Draw animated image watermark over a static image
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImage">Watermark image</param>
        public static AnimatedImage AddAnimatedImageWatermark(this Image img, AnimatedImage wmImage)
        {
            return img.AddAnimatedImageWatermark(wmImage, new ImageWatermarkOptions());
        }

        /// <summary>
        /// Draw animated image watermark over a static image
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedImageWatermark(this Image img, string wmImgPath, ImageWatermarkOptions ops)
        {
            var wm = AnimatedImage.FromFile(wmImgPath);
            return img.AddAnimatedImageWatermark(wm, ops);
        }

        /// <summary>
        /// Draw animated image watermark over a static image
        /// <para>Notice regarding watermark opacity:</para>
        /// <para>If watermark image needs to be resized, first resize the watermark image, 
        /// then save it to the disc, and read it again with Image.FromFile.</para>
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImage">Watermak image</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedImageWatermark(this Image img, AnimatedImage wmImage, ImageWatermarkOptions ops)
        {
            var fList = new List<Image>();
            var animImg = new AnimatedImage(new[] { img });

            for (int i = 0; i < wmImage.FramesCount; i++)
            {
                var clone = img.Clone() as Image;
                var imgI = clone.AddImageWatermark(wmImage.Frames[i], ops, false);
                fList.Add(imgI);
            }

            img.Dispose();
            wmImage.Dispose();

            //animImg.Frames?.Clear();
            animImg.Frames = fList;

            return animImg;
        }
    }
}
