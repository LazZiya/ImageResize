using LazZiya.ImageResize.ColorFormats;
using LazZiya.ImageResize.ResizeMethods;
using LazZiya.ImageResize.Tools;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Resize images
    /// </summary>
    public static class ImageResize
    {
        /// <summary>
        /// Auto scale image by width or height till longest border (width/height) is equal to new width/height.
        /// Final image aspect ratio is equal to original image aspect ratio.
        /// If the aspect ratio of new w/h != aspect ratio of original image then 
        /// one border will be in different size than the given value in order to keep original aspect ratio
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static Image Scale(this Image img, int newWidth, int newHeight)
        {
            var resize = new Scale(img.Size, new Size(newWidth, newHeight));

            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        /// <summary>
        /// Auto scale image by width or height till longest border (width/height) is equal to new width/height.
        /// Final image aspect ratio is equal to original image aspect ratio.
        /// If the aspect ratio of new w/h != aspect ratio of original image then 
        /// one border will be in different size than the given value in order to keep original aspect ratio
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static Image Scale(this Image img, int newWidth, int newHeight, GraphicOptions ops)
        {
            var resize = new Scale(img.Size, new Size(newWidth, newHeight));

            return Resize(img, resize.SourceRect, resize.TargetRect, ops);
        }

        /// <summary>
        /// Scale image by width and keep same aspect ratio of target image same as the original image.
        /// Height will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <returns></returns>
        public static Image ScaleByWidth(this Image img, int newWidth)
        {
            var resize = new Scale(img.Size, new Size(newWidth, 0));

            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        /// <summary>
        /// Scale image by width and keep same aspect ratio of target image same as the original image.
        /// Height will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static Image ScaleByWidth(this Image img, int newWidth, GraphicOptions ops)
        {
            var resize = new Scale(img.Size, new Size(newWidth, 0));

            return Resize(img, resize.SourceRect, resize.TargetRect, ops);
        }

        /// <summary>
        /// Scale image by height and keep same aspect ratio of target image same as the original image.
        /// Width will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static Image ScaleByHeight(this Image img, int newHeight)
        {
            var resize = new Scale(img.Size, new Size(0, newHeight));

            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        /// <summary>
        /// Scale image by height and keep same aspect ratio of target image same as the original image.
        /// Width will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newHeight"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static Image ScaleByHeight(this Image img, int newHeight, GraphicOptions ops)
        {
            var resize = new Scale(img.Size, new Size(0, newHeight));

            return Resize(img, resize.SourceRect, resize.TargetRect, ops);
        }

        /// <summary>
        /// Scale target image till shortest border are equal to target value, 
        /// then crop the additonal pixels from the longest border.
        /// Final image aspect ratio is equal to the given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot"></param>
        /// <returns></returns>
        public static Image ScaleAndCrop(this Image img, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            var resize = new ScaleAndCrop(img.Size, new Size(newWidth, newHeight), spot);

            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        /// <summary>
        /// Scale target image till shortest border are equal to target value, 
        /// then crop the additonal pixels from the longest border.
        /// Final image aspect ratio is equal to the given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static Image ScaleAndCrop(this Image img, int newWidth, int newHeight, GraphicOptions ops, TargetSpot spot = TargetSpot.Center)
        {
            var resize = new ScaleAndCrop(img.Size, new Size(newWidth, newHeight), spot);

            return Resize(img, resize.SourceRect, resize.TargetRect, ops);
        }

        /// <summary>
        /// Directly crop original image without scaling it.
        /// Final image aspect ratio is equal to given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot">target spot to crop and save</param>
        /// <returns></returns>
        public static Image Crop(this Image img, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            var resize = new Crop(img.Size, new Size(newWidth, newHeight), spot);
            return Resize(img, resize.SourceRect, resize.TargetRect);
        }

        /// <summary>
        /// Directly crop original image without scaling it.
        /// Final image aspect ratio is equal to given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot">target spot to crop and save</param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static Image Crop(this Image img, int newWidth, int newHeight, GraphicOptions ops, TargetSpot spot = TargetSpot.Center)
        {
            var resize = new Crop(img.Size, new Size(newWidth, newHeight), spot);
            return Resize(img, resize.SourceRect, resize.TargetRect, ops);
        }

        /// <summary>
        /// Specify custom resize options
        /// </summary>
        /// <param name="img">the image to resize</param>
        /// <param name="source">The coordinates to read as source from the image, 
        /// can be the whole image or part of it</param>
        /// <param name="target">The coordinates of the target image size</param>
        /// <returns></returns>
        public static Image Resize(this Image img, Rectangle source, Rectangle target)
        {
            return img.Resize(source, target, new GraphicOptions());
        }

        /// <summary>
        /// Specify custom resize options
        /// </summary>
        /// <param name="img">the image to resize</param>
        /// <param name="source">The coordinates to read as source from the image, 
        /// can be the whole image or part of it</param>
        /// <param name="target">The coordinates of the target image size</param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static Image Resize(this Image img, Rectangle source, Rectangle target, GraphicOptions ops)
        {
            // check for CMYK pixel format to use Format32bppArgb
            // or use the image pixel format
            var pixF = ImageColorFormats.GetColorFormat((Bitmap)img) == ImageColorFormat.Cmyk
                ? PixelFormat.Format32bppArgb
                : img.PixelFormat;

            using (Bitmap outputImage = new Bitmap(target.Width, target.Height, pixF))
            {
                var hRes = img.HorizontalResolution == 0 ? 72 : img.HorizontalResolution;
                var vRes = img.VerticalResolution == 0 ? 72 : img.VerticalResolution;

                outputImage.SetResolution(hRes, vRes);

                using (var graphics = Graphics.FromImage(outputImage))
                {
                    graphics.SmoothingMode = ops.SmoothingMode;
                    graphics.InterpolationMode = ops.InterpolationMode;
                    graphics.PixelOffsetMode = ops.PixelOffsetMode;
                    graphics.CompositingQuality = ops.CompositingQuality;
                    graphics.CompositingMode = ops.CompositingMode;
                    graphics.PageUnit = ops.PageUnit;

                    graphics.DrawImage(
                        img,
                        target,
                        source,
                        ops.PageUnit);

                }

                // If the image has alpha channel (png) return image memory stream
                if (img.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    using (var ms = new MemoryStream())
                    {
                        outputImage.Save(ms, img.RawFormat);
                        return Image.FromStream(ms);
                    }
                }

                return Image.FromHbitmap(outputImage.GetHbitmap());
            }
        }
    }
}