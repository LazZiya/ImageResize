using System.Drawing;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Conditionally resize animated images with reference to a condition token
    /// </summary>
    public static class AnimatedImageResizeConditional
    {
        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Auto scale image by width or height till longest border (width/height) is equal to new width/height.
        /// Final image aspect ratio is equal to original image aspect ratio.
        /// If the aspect ratio of new w/h != aspect ratio of original image then 
        /// one border will be in different size than the given value in order to keep original aspect ratio
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static AnimatedImage ScaleIf(this AnimatedImage img, bool condition, int newWidth, int newHeight)
        {
            return condition ? AnimatedImageResize.Scale(img, newWidth, newHeight) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Auto scale image by width or height till longest border (width/height) is equal to new width/height.
        /// Final image aspect ratio is equal to original image aspect ratio.
        /// If the aspect ratio of new w/h != aspect ratio of original image then 
        /// one border will be in different size than the given value in order to keep original aspect ratio
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static AnimatedImage ScaleIf(this AnimatedImage img, bool condition, int newWidth, int newHeight, GraphicOptions ops)
        {
            return condition ? AnimatedImageResize.Scale(img, newWidth, newHeight, ops) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Scale image by width and keep same aspect ratio of target image same as the original image.
        /// Height will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <returns></returns>
        public static AnimatedImage ScaleByWidthIf(this AnimatedImage img, bool condition, int newWidth)
        {
            return condition ? AnimatedImageResize.ScaleByWidth(img, newWidth) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Scale image by width and keep same aspect ratio of target image same as the original image.
        /// Height will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static AnimatedImage ScaleByWidthIf(this AnimatedImage img, bool condition, int newWidth, GraphicOptions ops)
        {
            return condition ? AnimatedImageResize.ScaleByWidth(img, newWidth, ops) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Scale image by height and keep same aspect ratio of target image same as the original image.
        /// Width will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        public static AnimatedImage ScaleByHeightIf(this AnimatedImage img, bool condition, int newHeight)
        {
            return condition ? AnimatedImageResize.ScaleByHeight(img, newHeight) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Scale image by height and keep same aspect ratio of target image same as the original image.
        /// Width will be adjusted automatically
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newHeight"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static AnimatedImage ScaleByHeightIf(this AnimatedImage img, bool condition, int newHeight, GraphicOptions ops)
        {
            return condition ? AnimatedImageResize.ScaleByHeight(img, newHeight, ops) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Scale target image till shortest border are equal to target value, 
        /// then crop the additonal pixels from the longest border.
        /// Final image aspect ratio is equal to the given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot"></param>
        /// <returns></returns>
        public static AnimatedImage ScaleAndCropIf(this AnimatedImage img, bool condition, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            return condition ? AnimatedImageResize.ScaleAndCrop(img, newWidth, newHeight, spot) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Scale target image till shortest border are equal to target value, 
        /// then crop the additonal pixels from the longest border.
        /// Final image aspect ratio is equal to the given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot"></param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static AnimatedImage ScaleAndCropIf(this AnimatedImage img, bool condition, int newWidth, int newHeight, GraphicOptions ops, TargetSpot spot = TargetSpot.Center)
        {
            return condition ? AnimatedImageResize.ScaleAndCrop(img, newWidth, newHeight, ops, spot) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Directly crop original image without scaling it.
        /// Final image aspect ratio is equal to given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot">target spot to crop and save</param>
        /// <returns></returns>
        public static AnimatedImage CropIf(this AnimatedImage img, bool condition, int newWidth, int newHeight, TargetSpot spot = TargetSpot.Center)
        {
            return condition ? AnimatedImageResize.Crop(img, newWidth, newHeight, spot) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Directly crop original image without scaling it.
        /// Final image aspect ratio is equal to given new width/height
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="spot">target spot to crop and save</param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static AnimatedImage CropIf(this AnimatedImage img, bool condition, int newWidth, int newHeight, GraphicOptions ops, TargetSpot spot = TargetSpot.Center)
        {
            return condition ? AnimatedImageResize.Crop(img, newWidth, newHeight, ops, spot) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Specify custom resize options
        /// </summary>
        /// <param name="img">the image to resize</param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="source">The coordinates to read as source from the image, 
        /// can be the whole image or part of it</param>
        /// <param name="target">The coordinates of the target image size</param>
        /// <returns></returns>
        public static AnimatedImage ResizeIf(this AnimatedImage img, bool condition, Rectangle source, Rectangle target)
        {
            return condition ? AnimatedImageResize.Resize(img, source, target) : img;
        }

        /// <summary>
        /// Do conditional resize if the the condition is true, otherwise return without resizing.
        /// Specify custom resize options
        /// </summary>
        /// <param name="img">the image to resize</param>
        /// <param name="condition">true to resize, false will return the img</param>
        /// <param name="source">The coordinates to read as source from the image, 
        /// can be the whole image or part of it</param>
        /// <param name="target">The coordinates of the target image size</param>
        /// <param name="ops">Graphic options <see cref="GraphicOptions"/></param>
        /// <returns></returns>
        public static AnimatedImage ResizeIf(this AnimatedImage img, bool condition, Rectangle source, Rectangle target, GraphicOptions ops)
        {
            return condition ? AnimatedImageResize.Resize(img, source, target, ops) : img;
        }
    }
}