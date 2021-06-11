namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Add animated text watermark over animated image, depending on a condition parameter.
    /// </summary>
    public static class AnimatedImageAnimatedTextWatermarkConditional
    {
        /// <summary>
        /// Add animated text watermark over animated image, depending on a conditon parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text"></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this AnimatedImage img, bool condition, string text)
        {
            return condition ? AnimatedImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text) : img;
        }

        /// <summary>
        /// Add animated text watermark over animated image, depending on a conditon parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text"></param>
        /// <param name="animOps"></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this AnimatedImage img, bool condition, string text, AnimatedTextWatermarkOptions animOps)
        {
            return condition ? AnimatedImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text, animOps) : img;
        }

        /// <summary>
        /// Add animated text watermark over animated image, depending on a conditon parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this AnimatedImage img, bool condition, string text, TextWatermarkOptions ops)
        {
            return condition ? AnimatedImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text, ops) : img;
        }

        /// <summary>
        /// Add animated text watermark over animated image, depending on a conditon parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        /// <param name="animOps">Animated text options <see cref="AnimatedTextWatermarkOptions"/></param>
        public static AnimatedImage AddAnimatedTextWatermarkIf(this AnimatedImage img, bool condition, string text, TextWatermarkOptions ops, AnimatedTextWatermarkOptions animOps)
        {
            return condition ? AnimatedImageAnimatedTextWatermark.AddAnimatedTextWatermark(img, text, ops, animOps) : img;
        }
    }
}
