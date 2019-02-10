using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LazZiya.ImageResize
{
    public static class ImageResize
    {
        /// <summary>
        /// receives image file from specific path and resize, you may need to save the file with a different name or you may get memory error (due to file in use)
        /// </summary>
        /// <param name="filePath">the full path to the file e.g. D:\\Hosting\\MyWebSite\\wwwroot\\images\\xx.jpg</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="method">resize method</param>
        /// <param name="spot">reference cropping spot</param>
        /// <returns>System.Drawing.Image</returns>
        public static Image Resize(string filePath, int newWidth, int newHeight, ResizeMethod method = ResizeMethod.Crop, TargetSpot spot = TargetSpot.Center)
        {
            var img = Image.FromFile(filePath);
            return Resize(img, newWidth, newHeight, method, spot);
        }

        /// <summary>
        /// receives image file from stream (e.g. upload stream) and resize
        /// </summary>
        /// <param name="stream">upload stream, file stream, memory stream...</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="method">resize method</param>
        /// <param name="spot">reference cropping spot</param>
        /// <returns>System.Drawing.Image</returns>
        public static Image Resize(Stream stream, int newWidth, int newHeight, ResizeMethod method = ResizeMethod.Crop, TargetSpot spot = TargetSpot.Center)
        {
            var img = Image.FromStream(stream);
            return Resize(img, newWidth, newHeight, method, spot);
        }

        /// <summary>
        /// receives System.Drawing.Image and resize, you may need to save the file with a different name or you may get memory error (due to file in use)
        /// </summary>
        /// <param name="img">System.Drwing.Image to be resized</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="method">resize method</param>
        /// <param name="spot">reference cropping spot</param>
        /// <returns>System.Drawing.Image</returns>
        public static Image Resize(Image img, int newWidth, int newHeight, ResizeMethod method = ResizeMethod.Crop, TargetSpot spot = TargetSpot.Center)
        {
            if (newWidth == 0 && newHeight == 0)
            {
                newWidth = img.Width;
                newHeight = img.Height;
            }
            else if (newWidth <= 0 && newHeight > 0)
            {
                // if only height is defined, then find new width and keep aspect ratio
                newWidth = GetNewWidth(img.Width, img.Height, newHeight);
            }
            else if (newWidth > 0 && newHeight <= 0)
            {
                // if only width is defined, then find new height and keep aspect ratio
                newHeight = GetNewHeight(img.Width, img.Height, newWidth);
            }

            // the rect pos and size to read from the original image
            var srcRect = new Rectangle(0, 0, img.Width, img.Height);

            // the target rrect pos and size to draw the resized image on
            var targetRect = new Rectangle(0, 0, newWidth, newHeight);

            if (method == ResizeMethod.Contain)
            {
                // ResizeMethod.Contain ==> resized image will have similar aspect ratio with original image
                // this means the resized image may not have the defined new width and height excactly,
                targetRect = ReDefineNewImageSize(img.Width, img.Height, newWidth, newHeight);
            }
            // crop
            else if (method == ResizeMethod.Crop)
            {
                srcRect = CropSourceImage(img.Width, img.Height, newWidth, newHeight, spot);
            }
            // spot crop, no scale
            else if(method == ResizeMethod.SpotCrop)
            {
                srcRect = DirectCropSourceImage(img.Width, img.Height, newWidth, newHeight, spot);
            }

            Bitmap outputImage = new Bitmap(targetRect.Width, targetRect.Height, img.PixelFormat);

            outputImage.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            try
            {
                Graphics graphics = Graphics.FromImage(outputImage);

                graphics.DrawImage(
                    img,
                    targetRect,
                    srcRect,
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
        /// re-scale newWidth / newHeight to match original image aspect ratio,
        /// so the resized image will have similar aspect raio with the original one.
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        private static Rectangle ReDefineNewImageSize(int imgWidth, int imgHeight, int newWidth, int newHeight)
        {
            float orgRatio = (float)imgWidth / (float)imgHeight;

            // if new image size is larger than original then use original image size 
            var _newWidth = (float)newWidth;
            var _newHeight = (float)newHeight;

            // org W > org H ==> contain new W and reduce new H according to org ratio
            if (orgRatio > 1)
                _newHeight = newWidth / orgRatio;
            // org H > org W ==> contain new H and reduce new W according to org ratio
            else
                _newWidth = newHeight * orgRatio;

            return new Rectangle(0, 0, (int)_newWidth, (int)_newHeight);
        }

        /// <summary>
        /// define the max rect size and pos to read from source image
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        private static Rectangle CropSourceImage(int imgWidth, int imgHeight, int newWidth, int newHeight, TargetSpot spot)
        {
            // aspect ratio of the resized image
            float croppedRatio = (float)newWidth / (float)newHeight;

            // aspect ratio of original image
            float orgRatio = (float)imgWidth / (float)imgHeight;

            // find aspect ratio difference
            float diffRatio = croppedRatio / orgRatio;

            var _srcWidth = (float)imgWidth;
            var _srcHeight = (float)imgHeight;

            if (diffRatio > 1)
                // new W > org W ==> crop org H
                _srcHeight = _srcWidth / croppedRatio;
            else
                // new H > org H ==> crop org W
                _srcWidth = _srcHeight * croppedRatio;

            // image will be cropped after scaling, so we send source rect size
            (int x, int y) = GetCroppingPos(imgWidth, imgHeight, (int)_srcWidth, (int)_srcHeight, spot);

            return new Rectangle(x, y, (int)_srcWidth, (int)_srcHeight);
        }

        /// <summary>
        /// just crop, don't scale the image
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot"></param>
        /// <returns></returns>
        private static Rectangle DirectCropSourceImage(int imgWidth, int imgHeight, int newWidth, int newHeight, TargetSpot spot)
        {
            // image will be scaled after being cropped, so we send new image size as source rect size
            (int x, int y) = GetCroppingPos(imgWidth, imgHeight, newWidth, newHeight, spot);

            return new Rectangle(x, y, (int)newWidth, (int)newHeight);
        }

        /// <summary>
        /// define x and y of source rect to read the image data from original image depending on CroppingSpot
        /// </summary>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <param name="spot"></param>
        /// <returns></returns>
        private static (int x, int y) GetCroppingPos(int imgWidth, int imgHeight, int srcWidth, int srcHeight, TargetSpot spot)
        {
            float x, y = 0;
            switch (spot)
            {
                case TargetSpot.Center: x = (imgWidth - srcWidth) / 2; y = (imgHeight - srcHeight) / 2; break;
                case TargetSpot.TopMiddle: x = (imgWidth - srcWidth) / 2; y = 0; break;
                case TargetSpot.BottomMiddle: x = (imgWidth - srcWidth) / 2; y = imgHeight - srcHeight; break;

                case TargetSpot.MiddleRight: x = imgWidth - srcWidth; y = (imgHeight - srcHeight) / 2; break;
                case TargetSpot.BottomRight: x = imgWidth - srcWidth; y = imgHeight - srcHeight; break;
                case TargetSpot.TopRight: x = imgWidth - srcWidth; y = 0; break;

                case TargetSpot.MiddleLeft: x = 0; y = (imgHeight - srcHeight) / 2; break;
                case TargetSpot.BottomLeft: x = 0; y = imgHeight - srcHeight * 1; break;
                case TargetSpot.TopLeft:
                default: x = 0; y = 0; break;
            }

            return ((int)x, (int)y);
        }

        private static int GetNewHeight(int imgWidth, int imgHeight, int newWidth)
        {
            double fracioanlPercentage = (double)newWidth / (double)imgWidth;

            return (int)(imgHeight * fracioanlPercentage);
        }

        private static int GetNewWidth(int imgWidth, int imgHeight, int newHeight)
        {
            double fractionalPercentage = (double)newHeight / (double)imgHeight;

            return (int)(imgWidth * fractionalPercentage);
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
