using System.Collections.Generic;
using System.Drawing;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Add animated text watermark over image
    /// </summary>
    public static class ImageAnimatedTextWatermark
    {
        /// <summary>
        /// Add animated text watermark over a static image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        public static AnimatedImage AddAnimatedTextWatermark(this Image img, string text)
        {
            return AddAnimatedTextWatermark(img, text, new TextWatermarkOptions(), new AnimatedTextWatermarkOptions());
        }

        /// <summary>
        /// Add animated text watermark over a static image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="animOps"></param>
        public static AnimatedImage AddAnimatedTextWatermark(this Image img, string text, AnimatedTextWatermarkOptions animOps)
        {
            return AddAnimatedTextWatermark(img, text, new TextWatermarkOptions(), animOps);
        }

        /// <summary>
        /// Add animated text watermark over a static image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermark(this Image img, string text, TextWatermarkOptions ops)
        {
            return AddAnimatedTextWatermark(img, text, ops, new AnimatedTextWatermarkOptions());
        }

        /// <summary>
        /// Add animated text watermark over a static image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        /// <param name="animOps">Animated text options <see cref="AnimatedTextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermark(this Image img, string text, TextWatermarkOptions ops, AnimatedTextWatermarkOptions animOps)
        {
            var fList = new List<Image>();
            var animImg = new AnimatedImage(new List<Image> { img });

            for (int i = 0; i < text.Length; i++)
            {
                var subStr = text.Substring(0, i);
                var clone = img.Clone() as Image;
                var frame = clone.AddTextWatermark(subStr, ops);
                fList.Add(frame);
            }

            img.Dispose();

            animImg.RepeatCount = 0;
            animImg.Frames = fList;

            return animImg;
        }
    }
}
