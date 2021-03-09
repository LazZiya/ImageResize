using LazZiya.ImageResize.Animated;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Add a static text watermark over animated image, depending on a condition parameter
    /// </summary>
    public static class AnimatedImageTextWatermarkConditional
    {
        /// <summary>
        /// Add a static text watermark over animated image depending on a condition parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text"></param>
        public static AnimatedImage AddTextWatermarkIf(this AnimatedImage img, bool condition, string text)
        {
            return condition ? AnimatedImageTextWatermark.AddTextWatermark(img, text) : img;
        }

        /// <summary>
        /// Add a static text watermark over animated image depending on a condition parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static AnimatedImage AddTextWatermarkIf(this AnimatedImage img, bool condition, string text, TextWatermarkOptions ops)
        {
            return condition ? AnimatedImageTextWatermark.AddTextWatermark(img, text, ops) : img;
        }
    }
}
