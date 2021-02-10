using LazZiya.ImageResize.Animated;
using System.Collections.Generic;
using System.Drawing;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Add text watermark over the image.
    /// </summary>
    public static class AnimatedTextWatermark
    {

        /// <summary>
        /// Add text watermark over the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        public static AnimatedGif AddTextWatermark(this AnimatedGif img, string text)
        {
            return AddTextWatermark(img, text, new TextWatermarkOptions());
        }

        /// <summary>
        /// Add text watermark over the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static AnimatedGif AddTextWatermark(this AnimatedGif img, string text, TextWatermarkOptions ops)
        {
            var fList = new List<Image>();

            foreach (var f in img.Frames)
            {
                f.AddTextWatermark(text, ops);
                fList.Add(f);
            }

            img.Frames.Clear();
            img.Frames = fList;

            return img;
        }
    }
}
