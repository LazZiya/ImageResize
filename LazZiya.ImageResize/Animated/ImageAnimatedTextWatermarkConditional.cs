using System.Drawing;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Add animated text watermark over image, depending on a conditional parameter.
    /// </summary>
    public static class ImageAnimatedTextWatermarkConditional
    {
        /// <summary>
        /// Add animated text watermark over a static image, depending on a conditional parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add animated text watermark, false will return the img as AnimatedImage</param>
        /// <param name="text"></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this Image img, bool condition, string text)
        {
            return condition ? ImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text) : new AnimatedImage(new[] { img });
        }

        /// <summary>
        /// Add animated text watermark over a static image, depending on a conditional parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add animated text watermark, false will return the img as AnimatedImage</param>
        /// <param name="text"></param>
        /// <param name="animOps"></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this Image img, bool condition, string text, AnimatedTextWatermarkOptions animOps)
        {
            return condition ? ImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text, animOps) : new AnimatedImage(new[] { img });
        }

        /// <summary>
        /// Add animated text watermark over a static image, depending on a conditional parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add animated text watermark, false will return the img as AnimatedImage</param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this Image img, bool condition, string text, TextWatermarkOptions ops)
        {
            return condition ? ImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text, ops) : new AnimatedImage(new[] { img });
        }

        /// <summary>
        /// Add animated text watermark over a static image, depending on a conditional parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add animated text watermark, false will return the img as AnimatedImage</param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        /// <param name="animOps">Animated text options <see cref="AnimatedTextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this Image img, bool condition, string text, TextWatermarkOptions ops, AnimatedTextWatermarkOptions animOps)
        {
            return condition ? ImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text, ops, animOps) : new AnimatedImage(new[] { img });
        }
    }
}
