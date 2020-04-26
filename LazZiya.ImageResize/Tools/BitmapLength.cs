using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize.Tools
{
    /// <summary>
    /// Calculate a bitmap length in bytes
    /// </summary>
    public static class BitmapLength
    {
        /// <summary>
        /// Get bitmap length
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static int GetLength(this Bitmap bitmap)
        {
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            bitmap.UnlockBits(bmpData);

            return bytes;
        }
    }
}
