using LazZiya.ImageResize.Tools;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    public static class SaveImage
    {
        /// <summary>
        /// Save the image to the specified path
        /// </summary>
        /// <param name="img">Image to save</param>
        /// <param name="path">Full path including file name and extension to save the image to</param>
        public static void SaveAs(this Image img, string path)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            var dotIndex = path.LastIndexOf('.');
            var ext = path.Substring(dotIndex, path.Length - dotIndex - 1);

            myImageCodecInfo = EncoderInfo.GetEncoderInfo(ext);
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            img.Save(path, myImageCodecInfo, myEncoderParameters);
        }
    }
}
