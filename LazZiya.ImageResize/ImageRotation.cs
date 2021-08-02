using System;
using System.Drawing;
using System.Linq;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Image rotation operations
    /// </summary>
    public static class ImageRotation
    {
        private const int exifOrientationId = 0x112;

        /// <summary>
        /// Keep resized image rotation as the original image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="originalImage"></param>
        /// <returns></returns>
        public static Image ReserveOriginalRotation(this Image img, Image originalImage)
        {
            if (!originalImage.PropertyIdList.Contains(exifOrientationId))
                return img;

            var rotation = originalImage.GetPropertyItem(exifOrientationId);
            int val = BitConverter.ToUInt16(rotation.Value, 0);
            var rot = RotateFlipType.RotateNoneFlipNone;

            if (val == 3 || val == 4)
                rot = RotateFlipType.Rotate180FlipNone;
            else if (val == 5 || val == 6)
                rot = RotateFlipType.Rotate90FlipNone;
            else if (val == 7 || val == 8)
                rot = RotateFlipType.Rotate270FlipNone;

            if (val == 2 || val == 4 || val == 5 || val == 7)
                rot |= RotateFlipType.RotateNoneFlipX;

            if (rot != RotateFlipType.RotateNoneFlipNone)
                img.RotateFlip(rot);

            return img;
        }

        /// <summary>
        /// Rotate and/or flip the image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rotateFlipType"></param>
        /// <returns></returns>
        public static Image RotateFlipImage(this Image img, RotateFlipType rotateFlipType)
        {
            img.RotateFlip(rotateFlipType);
            return img;
        }

        /// <summary>
        /// Rotate and/or flip the image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="condition"></param>
        /// <param name="rotateFlipType"></param>
        /// <returns></returns>
        public static Image RotateFlipImageIf(this Image img, bool condition, RotateFlipType rotateFlipType)
        {
            if(condition)
                img.RotateFlip(rotateFlipType);

            return img;
        }
    }
}
