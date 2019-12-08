using LazZiya.ImageResize.Exceptions;
using System;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize.Tools
{
    /// <summary>
    /// Available image formats and GUID values
    /// </summary>
    public abstract class ImageFormats
    {
        /// <summary>
        /// return image format by comparing the ImageFormat.Guid param
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>System.Drawing.Imaging.ImageFormat</returns>
        public static ImageFormat GetImageFormat(Guid guid)
        {
            return
                guid == ImageFormat.Bmp.Guid ? ImageFormat.Bmp :
                guid == ImageFormat.Emf.Guid ? ImageFormat.Emf :
                guid == ImageFormat.Exif.Guid ? ImageFormat.Exif :
                guid == ImageFormat.Gif.Guid ? ImageFormat.Gif :
                guid == ImageFormat.Icon.Guid ? ImageFormat.Icon :
                guid == ImageFormat.Jpeg.Guid ? ImageFormat.Jpeg :
                guid == ImageFormat.MemoryBmp.Guid ? ImageFormat.MemoryBmp :
                guid == ImageFormat.Png.Guid ? ImageFormat.Png :
                guid == ImageFormat.Tiff.Guid ? ImageFormat.Tiff :
                guid == ImageFormat.Wmf.Guid ? ImageFormat.Wmf :

                throw new ImageResizeException(new ImageResizeResult()
                {
                    Reason = FailureReasonType.UnknownImageFormatGuid,
                    Success = false,
                    Value = guid.ToString()
                });
        }

        /// <summary>
        /// return image format by reading file extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>System.Drawing.Imaging.ImageFormat</returns>
        public static ImageFormat GetImageFormat(string fileName)
        {
            var dotIndex = fileName.LastIndexOf('.');
            var ext = fileName.Substring(dotIndex, fileName.Length - dotIndex).ToLower();

            return
                ext == ".bmp" ? ImageFormat.Bmp :
                ext == ".emf" ? ImageFormat.Emf :
                ext == ".exif" ? ImageFormat.Exif :
                ext == ".gif" ? ImageFormat.Gif :
                ext == ".icon" ? ImageFormat.Icon :
                ext == ".jpg" ? ImageFormat.Jpeg :
                ext == ".png" ? ImageFormat.Png :
                ext == ".tiff" ? ImageFormat.Tiff :
                ext == ".wmf" ? ImageFormat.Wmf :
                throw new ImageResizeException(new ImageResizeResult()
                {
                    Reason = FailureReasonType.ExtensionNotSupported,
                    Success = false,
                    Value = ext
                });
        }
    }
}
