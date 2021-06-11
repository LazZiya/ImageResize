using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Add animated text watermark over animated image.
    /// </summary>
    public static class AnimatedImageAnimatedTextWatermark
    {
        /// <summary>
        /// Add animated text watermark over animated image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        public static AnimatedImage AddAnimatedTextWatermark(this AnimatedImage img, string text)
        {
            return AddAnimatedTextWatermark(img, text, new TextWatermarkOptions(), new AnimatedTextWatermarkOptions());
        }

        /// <summary>
        /// Add animated text watermark over animated image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="animOps"></param>
        public static AnimatedImage AddAnimatedTextWatermark(this AnimatedImage img, string text, AnimatedTextWatermarkOptions animOps)
        {
            return AddAnimatedTextWatermark(img, text, new TextWatermarkOptions(), animOps);
        }

        /// <summary>
        /// Add animated text watermark over animated image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermark(this AnimatedImage img, string text, TextWatermarkOptions ops)
        {
            return AddAnimatedTextWatermark(img, text, ops, new AnimatedTextWatermarkOptions());
        }

        /// <summary>
        /// Add animated text watermark over animated image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        /// <param name="animOps">Animated text options <see cref="AnimatedTextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermark(this AnimatedImage img, string text, TextWatermarkOptions ops, AnimatedTextWatermarkOptions animOps)
        {
            var fList = new List<Image>();

            var charPerFrame = img.FramesCount >= text.Length ? 1 : text.Length / img.FramesCount;

            for (int i = 0; i < img.FramesCount; i++)
            {
                var subLength = charPerFrame * i > text.Length ? text.Length : charPerFrame * i;
                var subStr = text.Substring(0, subLength);
                var frame = img.Frames[i].AddTextWatermark(subStr, ops);
                fList.Add(frame);
            }

            img.Frames.Clear();
            img.Frames = fList;

            return img;
        }
    }
}
