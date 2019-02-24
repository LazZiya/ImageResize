using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Text;

namespace LazZiya.ImageResize.Watermark
{
    public static class TextWM
    {
        /// <summary>
        /// add watermark text over image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="color">hex8 Color e.g. #77FFFFFF</param>
        /// <param name="bgColor">hex8 Color e.g. #00000000</param>
        /// <param name="fontFamily"></param>
        /// <param name="size"></param>
        /// <param name="align"></param>
        /// <param name="vAlign"></param>
        /// <param name="style"></param>
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


        /// <summary>
        /// watermark text pos
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="fontSize"></param>
        /// <param name="pos"></param>
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
