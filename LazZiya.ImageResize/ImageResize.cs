using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    public static class ImageResize
    {
        /// <summary>
        /// scale image file from Image
        /// new image will have same aspect ratio of the original image, 
        /// even if specified new width and height has a different aspect ratio
        /// </summary>
        /// <param name="img">System.Drawing.Image</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static Image Scale(Image img, int newWidth, int newHeight)
        {
            // confirm new defined sizes
            var newSize = img.ConfirmNewSizeValues(newWidth, newHeight);

            // the rect pos and size to read from the original image
            var srcRect = new Rectangle(0, 0, img.Width, img.Height);

            // get target image size
            var targetSize = img.ScaledImageSize(newSize);

            var targetOrigin = new Point(0, 0);
            // ResizeMethod.Contain ==> resized image will have similar aspect ratio with original image
            // this means the resized image may not have the defined new width and height excactly,
            var targetRect = new Rectangle(targetOrigin, targetSize);

            return Resize(img, srcRect, targetRect);
        }


        /// <summary>
        /// scale image and crop file from Image
        /// new image will have same aspect ratio of the original image, 
        /// even if specified new width and height has a different aspect ratio
        /// </summary>
        /// <param name="img">System.Drawing.Image</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static Image ScaleAndCrop(Image img, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            // confirm new size, no zero width / height
            var newSize = img.ConfirmNewSizeValues(newWidth, newHeight);

            // define the source image size to read data from it
            var srcSize = img.SourceSize(newSize, spot);

            // define origin point to start reading from source image data
            var srcOrigin = img.GetCroppingPos(srcSize, spot);

            // the rect pos and size to read from the original image
            var srcRect = new Rectangle(srcOrigin, srcSize);

            var targetOrigin = new Point(0, 0);
            // the target rect pos and size to draw the resized image on
            var targetRect = new Rectangle(targetOrigin, newSize);

            return Resize(img, srcRect, targetRect);
        }

        public static Image Crop(Image img, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            var newSize = img.ConfirmNewSizeValues(newWidth, newHeight);

            // get origin point for new size
            var srcOrigin = img.GetCroppingPos(newSize, spot);
            var srcRect = new Rectangle(srcOrigin, newSize);

            var targetOrigin = new Point(0, 0);
            // the target rect pos and size to draw the resized image on
            var targetRect = new Rectangle(targetOrigin, newSize);

            return Resize(img, srcRect, targetRect);
        }

        /// <summary>
        /// receives System.Drawing.Image and resize, you may need to save the file with a different name or you may get memory error (due to file in use)
        /// </summary>
        /// <param name="img">System.Drwing.Image to be resized</param>
        /// <param name="_newWidth"></param>
        /// <param name="_newHeight"></param>
        /// <param name="method">resize method</param>
        /// <param name="spot">reference cropping spot</param>
        /// <returns>System.Drawing.Image</returns>
        public static Image Resize(Image img, Rectangle source, Rectangle target)
        {
            Bitmap outputImage = new Bitmap(target.Width, target.Height, img.PixelFormat);

            outputImage.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            try
            {
                Graphics graphics = Graphics.FromImage(outputImage);

                graphics.DrawImage(
                    img,
                    target,
                    source,
                    GraphicsUnit.Pixel);

                graphics.Dispose();
            }
            catch (Exception e)
            {
                //outputImage = new Bitmap(img);
                throw new ImageResizeException(new ImageResizeResult()
                {
                    Reason = FailureReasonType.GraphicsException,
                    Success = false,
                    Value = e.Message
                });
            }

            return outputImage;
        }

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
