using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace LazZiya.ImageResize
{
    public static class ImageExtensions
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="img"></param>
        /// <param name="text"></param>
        /// <param name="color">RGBA Color e.g. rgba(200,100,50,0.5)</param>
        /// <param name="bgColor">RGBA Color e.g. rgba(50,100,200,0.95)</param>
        /// <param name="fontFamily"></param>
        /// <param name="size"></param>
        /// <param name="align"></param>
        /// <param name="vAlign"></param>
        /// <param name="style"></param>
        public static void AddWatermark(this Image img,
            string text,
            string color = "rgba(255, 255, 255, 0.9)", string bgColor = "rgba(89, 89, 89, 0.5)",
            string fontFamily = "Tahoma", int size = 24,
            Align align = Align.Left, VerticalAlign vAlign = VerticalAlign.BottomFree, TextStyle style = TextStyle.Regular)
        {
            Graphics graphics = Graphics.FromImage(img);

            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.TextContrast = 12;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.CompositingMode = CompositingMode.SourceOver;

            var _bgColor = new RgbaToArray(bgColor);
            SolidBrush bgBrush = new SolidBrush(Color.FromArgb(_bgColor.Alpha, _bgColor.Red, _bgColor.Green, _bgColor.Blue));

            var rectPos = RectanglePos(img.Width, img.Height, size, vAlign);
            graphics.FillRectangle(bgBrush, rectPos);

            var tStyle = FontStyle.Regular;
            switch (style)
            {
                case TextStyle.Bold: tStyle = FontStyle.Bold; break;
                case TextStyle.Italic: tStyle = FontStyle.Italic; break;

                case TextStyle.Regular:
                default: tStyle = FontStyle.Regular; break;
            }

            Font textFont = new Font(fontFamily, size, tStyle, GraphicsUnit.Pixel);
            var textColor = new RgbaToArray(color);
            SolidBrush textBrush = new SolidBrush(Color.FromArgb(textColor.Alpha, textColor.Red, textColor.Green, textColor.Blue));

            var textMetrics = graphics.MeasureString(text, textFont);
            var beforeText = GetTextAlign(textMetrics, img.Width, align);

            Point drawPoint = new Point(beforeText, rectPos.Y + (rectPos.Height / 4));

            graphics.DrawString(text, textFont, textBrush, drawPoint);
            graphics.Dispose();
        }

        private static Rectangle RectanglePos(int imgWidth, int imgHeight, int fontSize, VerticalAlign pos)
        {
            Rectangle rect;

            switch (pos)
            {
                case VerticalAlign.TopFixed:
                    rect = new Rectangle(0, 0, imgWidth, fontSize * 2);
                    break;

                case VerticalAlign.TopFree:
                    rect = new Rectangle(0, fontSize, imgWidth, fontSize * 2);
                    break;

                case VerticalAlign.Middle:
                    rect = new Rectangle(0, imgHeight / 2 - fontSize, imgWidth, fontSize * 2);
                    break;

                case VerticalAlign.BottomFree:
                    rect = new Rectangle(0, imgHeight - fontSize * 3, imgWidth, fontSize * 2);
                    break;

                case VerticalAlign.BottomFixed:
                    rect = new Rectangle(0, imgHeight - fontSize * 2, imgWidth, fontSize * 2);
                    break;

                default:
                    rect = new Rectangle(0, 0, imgWidth, fontSize * 2);
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

            throw new ImageResizeException(new ImageResizeResult() {
                Reason = FailureReasonType.EncoderNotFound,
                Success = false,
                Value = ext
            });
        }

        private static int GetTextAlign(SizeF textMetrics, int imgWidth, Align align)
        {
            int space;
            switch (align)
            {
                case Align.Left: space = 5; break;
                case Align.Center: space = (int)(imgWidth - textMetrics.Width) / 2; break;
                case Align.Right: space = (int)(imgWidth - textMetrics.Width) - 5; break;
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
