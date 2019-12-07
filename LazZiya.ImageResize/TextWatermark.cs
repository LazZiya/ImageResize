using LazZiya.ImageResize.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LazZiya.ImageResize
{
    public static class TextWatermark
    {

        /// <summary>
        /// Add text watermark over the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        public static Image AddTextWatermark(this Image img, string text)
        {
            return AddTextWatermark(img, text, new TextWatermarkOptions());
        }

        /// <summary>
        /// Add text watermark over the image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="ops"></param>
        public static Image AddTextWatermark(this Image img, string text, TextWatermarkOptions ops)
        {
            using (var graphics = Graphics.FromImage(img))
            {
                var bgPos = TextWatermarkPosition.SetBGPos(img.Width, img.Height, ops.FontSize, ops.Location, ops.Margin);

                var sf = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.NoWrap
                };

                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //graphics.TextContrast = 12;
                //graphics.CompositingMode = CompositingMode.SourceOver;
                //graphics.InterpolationMode = InterpolationMode.High;

                // Draw background if not fully transparent
                if (ops.BGColor.A > 0)
                {
                    var bgBrush = new SolidBrush(ops.BGColor);
                    graphics.FillRectangle(bgBrush, bgPos);
                }

                // Set font to use
                var ff = new FontFamily(ops.FontName);
                var font = new Font(ff, ops.FontSize, ops.FontStyle, GraphicsUnit.Pixel);

                // Measure text size
                var textMetrics = graphics.MeasureString(text, font, img.Width, sf);
                var beforeText = TextWatermarkPosition.SetTextAlign(textMetrics, img.Width, ops.Location);
                var drawPoint = new PointF(beforeText, bgPos.Y + (bgPos.Height / 4));

                var outlineBrush = new SolidBrush(ops.OutlineColor);

                using (var pen = new Pen(outlineBrush, ops.OutlineWidth))
                {
                    using (var p = new GraphicsPath())
                    {
                        p.AddString(text, ff, (int)ops.FontStyle, ops.FontSize, drawPoint, sf);

                        // Draw text outline if not fully transparent
                        if (ops.OutlineColor.A > 0)
                        {
                            graphics.DrawPath(pen, p);
                        }

                        // Draw text if not fully transparent
                        if (ops.TextColor.A > 0)
                        {
                            var textBrush = new SolidBrush(ops.TextColor);
                            graphics.FillPath(textBrush, p);
                        }
                    }
                }
            }

            return img;
        }
    }
}
