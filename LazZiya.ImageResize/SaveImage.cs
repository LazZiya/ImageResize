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
        /// <param name="quality">Image save quality (0 - 100)</param>
        public static void SaveAs(this Image img, string path, int quality = 95)
        {
            using (var myEncoderParameters = new EncoderParameters(1))
            {
                var myEncoder = Encoder.Quality;
                using (var myEncoderParameter = new EncoderParameter(myEncoder, (long)quality))
                {
                    myEncoderParameters.Param[0] = myEncoderParameter;

                    using (img)
                    {
                        var dotIndex = path.LastIndexOf('.');
                        var ext = path.Substring(dotIndex, path.Length - dotIndex - 1);
                        var myImageCodecInfo = EncoderInfo.GetEncoderInfo(ext);

                        img.Save(path, myImageCodecInfo, myEncoderParameters);
                    }
                }
            }
        }
    }
}
