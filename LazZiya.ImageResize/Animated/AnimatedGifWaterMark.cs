﻿using System.Collections.Generic;
using System.Drawing;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Add an image watermark to animated gif
    /// </summary>
    public static class AnimatedGifWaterMark
    {
        /// <summary>
        /// Draw image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        public static AnimatedGif AddImageWatermark(this AnimatedGif img, string wmImgPath)
        {
            var wm = Image.FromFile(wmImgPath);
            return img.AddImageWatermark(wm, new ImageWatermarkOptions());
        }

        /// <summary>
        /// Draw image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImage">Watermark image</param>
        public static AnimatedGif AddImageWatermark(this AnimatedGif img, Image wmImage)
        {
            return img.AddImageWatermark(wmImage, new ImageWatermarkOptions());
        }

        /// <summary>
        /// Draw image watermark
        /// </summary>
        /// <param name="img">The original image</param>
        /// <param name="wmImgPath">Path to the watermark image file e.g. wwwroot\images\watermark.png</param>
        /// <param name="ops">Image watermark options <see cref="ImageWatermarkOptions"/></param>
        public static AnimatedGif AddImageWatermark(this AnimatedGif img, string wmImgPath, ImageWatermarkOptions ops)
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
        public static AnimatedGif AddImageWatermark(this AnimatedGif img, Image wmImage, ImageWatermarkOptions ops)
        {
            var fList = new List<Image>();

            foreach (var f in img.Frames)
            {
                f.AddImageWatermark(wmImage, ops, false);
                fList.Add(f);
            }

            wmImage.Dispose();

            img.Frames.Clear();
            img.Frames = fList;

            return img;
        }
    }
}
