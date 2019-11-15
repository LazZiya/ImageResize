using LazZiya.ImageResize.Exceptions;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Image extnsion methods to get encoder info
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Get image codec information for the given extension.
        /// </summary>
        /// <param name="ext">extension of the image file</param>
        /// <returns><see cref="ImageCodecInfo"/></returns>
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
            myImageCodecInfo = GetEncoderInfo(ext);
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            img.Save(path, myImageCodecInfo, myEncoderParameters);
        }
    }
}
