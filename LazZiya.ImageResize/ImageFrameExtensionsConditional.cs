using System.Drawing;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Draw frame, background, etc...
    /// </summary>
    public static class ImageFrameExtensionsConditional
    {
        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image, depending on a condition parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Image AddFrameIf(this Image img, bool condition)
        {
            return condition 
                ? ImageFrameExtensions.AddFrame(img, img.Width, img.Height, new ImageFrameOptions())
                : img;
        }
        
        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image, depending on a condition parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Image AddFrameIf(this Image img, bool condition, ImageFrameOptions options)
        {
            return condition 
                ? ImageFrameExtensions.AddFrame(img, img.Width, img.Height, options)
                : img;
        }
        
        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image, depending on a condition parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <returns></returns>
        public static Image AddFrameIf(this Image img, bool condition, int frameWidth, int frameHeight)
        {
            return condition 
                ? ImageFrameExtensions.AddFrame(img, frameWidth, frameHeight, new ImageFrameOptions())
                : img;
        }

        /// <summary>
        /// This method will not resize the front image, but it will draw a frame with a specified width around the image, depending on a condition parameter.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Image AddFrameIf(this Image img, bool condition, int frameWidth, int frameHeight, ImageFrameOptions options)
        {
            return condition
                ? ImageFrameExtensions.AddFrame(img, frameWidth, frameHeight, options)
                : img;
        }
    }
}
