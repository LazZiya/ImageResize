using LazZiya.ImageResize.Exceptions;
using LazZiya.ImageResize.ResizeMethods;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize
{
    public static class ImageResize
    {
        public static Image Scale(Image img, int newWidth, int newHeight)
        {
            var resize = new Scale(img, new Size(newWidth, newHeight));

            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        public static Image ScaleAndCrop(Image img, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            var resize = new ScaleAndCrop(img, new Size(newWidth, newHeight), spot);

            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        public static Image Crop(Image img, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            var resize = new Crop(img, new Size(newWidth, newHeight), spot);
            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

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
                throw new ImageResizeException(new ImageResizeResult()
                {
                    Reason = FailureReasonType.GraphicsException,
                    Success = false,
                    Value = e.Message
                });
            }

            return outputImage;
        }
    }
}
