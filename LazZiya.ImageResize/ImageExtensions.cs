using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;

namespace LazZiya.ImageResize
{
    public static class ImageExtensions
    {
        /// <summary>
        /// add watermark text over image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="color">hex8 Color e.g. #ddaaaaaa</param>
        /// <param name="bgColor">hex8 Color e.g. #aadddddd</param>
        /// <param name="fontFamily"></param>
        /// <param name="size"></param>
        /// <param name="align"></param>
        /// <param name="vAlign"></param>
        /// <param name="style"></param>
        public static void TextWatermark(this Image img,
            string text,
            string color = "#ddaaaaaa", string bgColor = "#aadddddd",
            string fontFamily = "Arial", int size = 24,
            TargetSpot spot = TargetSpot.BottomLeft, FontStyle style = FontStyle.Regular, bool stick = false)
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

            var rectPos = SetBGPos(img.Width, img.Height, size, spot, stick);
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
        /// add image watermark over the uploaded image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="wmFileName"></param>
        /// <param name="spot"></param>
        /// <param name="stickToBorder"></param>
        public static void ImageWatermark(this Image img, string wmFileName, TargetSpot spot = TargetSpot.TopRight, bool stickToBorder = false)
        {
            var graphics = Graphics.FromImage(img);

            graphics.SmoothingMode = SmoothingMode.None;
            graphics.CompositingMode = CompositingMode.SourceOver;

            var wmImage = Image.FromFile(wmFileName);

            var wmW = wmImage.Width;
            var wmH = wmImage.Height;

            var drawingPoint = ImageWatermarkPos(img.Width, img.Height, wmW, wmH, spot, stickToBorder);

            graphics.DrawImage(wmImage, drawingPoint);

            graphics.Dispose();
        }

        private static PointF ImageWatermarkPos(int imgWidth, int imgHeight, int wmWidth, int wmHeight, TargetSpot spot, bool stickToBorder)
        {
            float marginW = stickToBorder ? 0F : imgWidth * 0.05F;
            float marginH = stickToBorder ? 0F : imgHeight * 0.05F;

            PointF point;

            switch (spot)
            {
                case TargetSpot.BottomLeft: point = new PointF(marginW, imgHeight - wmHeight - marginH); break;
                case TargetSpot.BottomMiddle: point = new PointF(imgWidth / 2 - wmWidth / 2, imgHeight - wmHeight - marginH); break;
                case TargetSpot.BottomRight: point = new PointF(imgWidth - wmWidth - marginW, imgHeight - wmHeight - marginH); break;
                case TargetSpot.MiddleLeft: point = new PointF(marginW, imgHeight / 2 - wmHeight / 2); break;
                case TargetSpot.Center: point = new PointF(imgWidth / 2 - wmWidth / 2, imgHeight / 2 - wmHeight / 2); break;
                case TargetSpot.MiddleRight: point = new PointF(imgWidth - wmWidth - marginW, imgHeight / 2 - wmHeight / 2); break;
                case TargetSpot.TopLeft: point = new PointF(marginW, marginH); break;
                case TargetSpot.TopMiddle: point = new PointF(imgWidth / 2 - wmWidth / 2, marginH); break;

                case TargetSpot.TopRight:
                default: point = new PointF(imgWidth - wmWidth - marginW, marginH); break;
            }

            return point;
        }

        /// <summary>
        /// watermark text pos
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="fontSize"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private static Rectangle SetBGPos(int imgWidth, int imgHeight, int fontSize, TargetSpot spot, bool stick)
        {
            Rectangle rect;

            var margin = stick ? 0 : fontSize;
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

        private static ImageCodecInfo GetEncoderInfo(string ext)
        {
            int j;

            ImageCodecInfo[] encoders;

            encoders = ImageCodecInfo.GetImageEncoders();

            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].FilenameExtension.ToLower().Contains(ext.ToLower()))
                    return encoders[j];
            }

            throw new ImageResizeException(new ImageResizeResult()
            {
                Reason = FailureReasonType.EncoderNotFound,
                Success = false,
                Value = ext
            });
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

        public static void SaveAs(this Image img, string path)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            var dotIndex = path.LastIndexOf('.');
            var ext = path.Substring(dotIndex, path.Length - dotIndex - 1);
            myImageCodecInfo = GetEncoderInfo(ext);
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            img.Save(path, myImageCodecInfo, myEncoderParameters);
        }
    }
}
