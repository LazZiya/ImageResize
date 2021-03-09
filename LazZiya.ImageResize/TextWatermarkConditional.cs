using System.Drawing;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Add text watermark over the image depending on a conditional parameter.
    /// </summary>
    public static class TextWatermarkConditional
    {

        /// <summary>
        /// Add a text watermark if the condition is true, otherwise return without textwatermark.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text"></param>
        public static Image AddTextWatermarkIf(this Image img, bool condition, string text)
        {
            return condition ? TextWatermark.AddTextWatermark(img, text) : img;
        }

        /// <summary>
        /// Add a text watermark if the condition is true, otherwise return without textwatermark.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to add text watermark, false will return the img</param>
        /// <param name="text">text to draw over the image</param>
        /// <param name="ops">Text watermark options <see cref="TextWatermarkOptions"/></param>
        public static Image AddTextWatermarkIf(this Image img, bool condition, string text, TextWatermarkOptions ops)
        {
            return condition ? TextWatermark.AddTextWatermark(img, text, ops) : img;
        }
    }
}
