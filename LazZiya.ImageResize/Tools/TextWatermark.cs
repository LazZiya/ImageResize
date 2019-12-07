using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;

namespace LazZiya.ImageResize.Tools
{
    /// <summary>
    /// Add a text watermark over the main image
    /// </summary>
    public static partial class Watermark
    {
        /// <summary>
        /// Add a text watermark over the main image
        /// </summary>
        /// <param name="img">The main image</param>
        /// <param name="text">The text to add as watermark</param>
        /// <param name="color">The color of the text. 
        /// use 8 digit hex code to specify alpha channed as well.
        /// sample: #77FFFFFF (77 is the alpha channed (00 - FF)
        /// </param>
        /// <param name="bgColor">The color of the text background. 
        /// use 8 digit hex code to specify alpha channed as well.
        /// sample: #77FFFFFF (77 is the alpha channed (00 - FF)</param>
        /// <param name="fontFamily">Font family name</param>
        /// <param name="size">Text size</param>
        /// <param name="spot">Target spot to draw the watermark text over the main image. 
        /// See <see cref="TargetSpot"/></param>
        /// <param name="style">Font style</param>
        /// <param name="margin">The distance in pixels between the watermark text and the nearest border of the main image.</param>
        [Obsolete("This mehtod is obsolete and will be removed in a feature release, use AddTextWatermark instead.")]
        public static void TextWatermark(this Image img,
            string text,
            string color = "#77FFFFFF", string bgColor = "#00000000",
            string fontFamily = "Arial", int size = 24,
            TargetSpot spot = TargetSpot.BottomLeft, FontStyle style = FontStyle.Regular, int margin = 10)
        {
            var graphics = Graphics.FromImage(img);

            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.TextContrast = 12;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.CompositingMode = CompositingMode.SourceOver;

            var _bgAlpha = int.Parse(bgColor.Substring(1, 2), NumberStyles.HexNumber);
            var _bgColor = bgColor.Substring(3, 6);
            var bgBrush = new SolidBrush(
                Color.FromArgb(_bgAlpha, ColorTranslator.FromHtml($"#{_bgColor}")));

            var rectPos = TextWatermarkPosition.SetBGPos(img.Width, img.Height, size, spot, margin);
            graphics.FillRectangle(bgBrush, rectPos);

            var textFont = new Font(fontFamily, size, style, GraphicsUnit.Pixel);
            var _alpha = int.Parse(color.Substring(1, 2), NumberStyles.HexNumber);
            var _color = color.Substring(3, 6);
            var textBrush = new SolidBrush(
                Color.FromArgb(_alpha, ColorTranslator.FromHtml($"#{_color}")));

            var textMetrics = graphics.MeasureString(text, textFont);
            var beforeText = TextWatermarkPosition.SetTextAlign(textMetrics, img.Width, spot);

            var drawPoint = new PointF(beforeText, rectPos.Y + (rectPos.Height / 4));
            graphics.DrawString(text, textFont, textBrush, drawPoint);

            graphics.Dispose();
        }
    }
}
