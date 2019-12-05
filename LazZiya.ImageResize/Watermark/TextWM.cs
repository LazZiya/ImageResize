using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Text;

namespace LazZiya.ImageResize.Watermark
{
    /// <summary>
    /// Add a text watermark over the main image
    /// </summary>
    public static class TextWM
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
        /*public static void TextWatermark(this Image img,
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

            var rectPos = SetBGPos(img.Width, img.Height, size, spot, margin);
            graphics.FillRectangle(bgBrush, rectPos);

            var textFont = new Font(fontFamily, size, style, GraphicsUnit.Pixel);
            var _alpha = int.Parse(color.Substring(1, 2), NumberStyles.HexNumber);
            var _color = color.Substring(3, 6);
            var textBrush = new SolidBrush(
                Color.FromArgb(_alpha, ColorTranslator.FromHtml($"#{_color}")));

            var textMetrics = graphics.MeasureString(text, textFont);
            var beforeText = SetTextAlign(textMetrics, img.Width, spot);

            var drawPoint = new PointF(beforeText, rectPos.Y + (rectPos.Height / 4));
            graphics.DrawString(text, textFont, textBrush, drawPoint);

            graphics.Dispose();
        }
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="bgColor"></param>
        /// <param name="outlineColor"></param>
        /// <param name="fontFamily"></param>
        /// <param name="size"></param>
        /// <param name="spot"></param>
        /// <param name="outlineWidth"></param>
        /// <param name="style"></param>
        /// <param name="margin"></param>
        public static void TextWatermark(this Image img,
            string text,
            string color = "#77FFFFFF", string bgColor = "#00000000", string outlineColor = "#00000000",
            string fontFamily = "Arial", int size = 24, float outlineWidth = 1.0f,
            TargetSpot spot = TargetSpot.BottomLeft, FontStyle style = FontStyle.Regular, int margin = 10)
        {


            var _bgAlpha = int.Parse(bgColor.Substring(1, 2), NumberStyles.HexNumber);
            var _bgColor = bgColor.Substring(3, 6);
            var bgBrush = new SolidBrush(Color.FromArgb(_bgAlpha, ColorTranslator.FromHtml($"#{_bgColor}")));
            var bgPos = SetBGPos(img.Width, img.Height, size, spot, margin);
            

            var textFont = new Font(fontFamily, size, style, GraphicsUnit.Pixel);
            var _alpha = int.Parse(color.Substring(1, 2), NumberStyles.HexNumber);
            var _color = color.Substring(3, 6);
            var textBrush = new SolidBrush(Color.FromArgb(_alpha, ColorTranslator.FromHtml($"#{_color}")));

            var _outlineAlpha = int.Parse(outlineColor.Substring(1, 2), NumberStyles.HexNumber);
            var _outlineColor = outlineColor.Substring(3, 6);
            var outlineBrush = new SolidBrush(Color.FromArgb(_outlineAlpha, ColorTranslator.FromHtml($"#{_outlineColor}")));

            var sf = new StringFormat()
            {
                FormatFlags = StringFormatFlags.NoWrap
            };

            var fFamily = new FontFamily(fontFamily);

            using (var graphics = Graphics.FromImage(img))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //graphics.TextContrast = 12;
                //graphics.CompositingMode = CompositingMode.SourceOver;
                //graphics.InterpolationMode = InterpolationMode.High;

                // Draw background
                graphics.FillRectangle(bgBrush, bgPos);

                // Measure text size
                var textMetrics = graphics.MeasureString(text, textFont, img.Width, sf);
                var beforeText = SetTextAlign(textMetrics, img.Width, spot);
                var drawPoint = new PointF(beforeText, bgPos.Y + (bgPos.Height / 4));

                using (var pen = new Pen(outlineBrush, outlineWidth))
                {
                    using (var p = new GraphicsPath())
                    {
                        p.AddString(text, fFamily, (int)style, size, drawPoint, sf);
                        
                        // Draw text outline
                        graphics.DrawPath(pen, p);

                        // Draw text
                        graphics.FillPath(textBrush, p);
                    }
                }
            }
        }


        /// <summary>
        /// Calculate the watermark text background size and position according to the taret spot, 
        /// main image size and font size.
        /// </summary>
        /// <param name="imgWidth">Main image width</param>
        /// <param name="imgHeight">Main image height</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="spot">target spot</param>
        /// <param name="margin">Distance from the nearest border</param>
        /// <returns></returns>
        private static Rectangle SetBGPos(int imgWidth, int imgHeight, int fontSize, TargetSpot spot, int margin)
        {
            Rectangle rect;

            var bgHeight = fontSize * 2;

            switch (spot)
            {
                case TargetSpot.TopLeft:
                case TargetSpot.TopMiddle:
                case TargetSpot.TopRight:
                    rect = new Rectangle(0, margin, imgWidth, bgHeight);
                    break;

                case TargetSpot.MiddleLeft:
                case TargetSpot.MiddleRight:
                case TargetSpot.Center:
                    rect = new Rectangle(0, imgHeight / 2 - bgHeight / 2, imgWidth, bgHeight);
                    break;

                case TargetSpot.BottomLeft:
                case TargetSpot.BottomMiddle:
                case TargetSpot.BottomRight:
                default:
                    rect = new Rectangle(0, imgHeight - bgHeight - margin, imgWidth, bgHeight);
                    break;
            }

            return rect;
        }

        private static int SetTextAlign(SizeF textMetrics, int imgWidth, TargetSpot spot)
        {
            int space;
            switch (spot)
            {
                case TargetSpot.BottomMiddle:
                case TargetSpot.TopMiddle:
                case TargetSpot.Center:
                    space = (int)(imgWidth - textMetrics.Width) / 2; break;

                case TargetSpot.BottomRight:
                case TargetSpot.MiddleRight:
                case TargetSpot.TopRight:
                    space = (int)(imgWidth - textMetrics.Width) - 5; break;

                case TargetSpot.BottomLeft:
                case TargetSpot.MiddleLeft:
                case TargetSpot.TopLeft:
                default: space = 5; break;
            }

            return space;
        }
    }
}
