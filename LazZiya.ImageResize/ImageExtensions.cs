using LazZiya.ImageResize.Exceptions;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    public static class ImageExtensions
    {
        public static ImageCodecInfo GetEncoderInfo(string ext)
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
