using LazZiya.ImageResize.Tools;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Save the image file to disc
    /// </summary>
    public static class SaveImage
    {
        /// <summary>
        /// Save the image to the specified path then dispose the Image object.
        /// </summary>
        /// <param name="img">Image to save</param>
        /// <param name="path">Full path including file name and extension to save the image to</param>
        public static void SaveAs(this Image img, string path)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;

            var dotIndex = path.LastIndexOf('.');
            var ext = path.Substring(dotIndex, path.Length - dotIndex - 1);

            myImageCodecInfo = EncoderInfo.GetEncoderInfo(ext);
            myEncoder = Encoder.Quality;

            using (var myEncoderParameters = new EncoderParameters(1))
            {
                using (var myEncoderParameter = new EncoderParameter(myEncoder, 100L))
                {
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    img.Save(path, myImageCodecInfo, myEncoderParameters);
                    img.Dispose();
                }
            }
        }
    }
}
