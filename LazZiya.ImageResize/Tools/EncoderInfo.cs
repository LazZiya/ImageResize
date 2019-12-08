using LazZiya.ImageResize.Exceptions;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize.Tools
{
    /// <summary>
    /// Image extension methods to get encoder info
    /// </summary>
    public abstract class EncoderInfo
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
    }
}
