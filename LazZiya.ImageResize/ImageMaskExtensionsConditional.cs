using System.Drawing;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Add image mask
    /// </summary>
    public static class ImageMaskExtensionsConditional
    {
        /// <summary>
        /// Add image mask with the specified paameters, depnding on a conditonal parameter
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition">true to apply mask, otherwise return image</param>
        /// <param name="rectangle"></param>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static Image MaskIf(this Image img, bool condition, Rectangle rectangle, ImageFrameShape shape)
        {
            return condition ? ImageMaskExtensions.Mask(img, rectangle, shape) : img;
        }
    }
}
