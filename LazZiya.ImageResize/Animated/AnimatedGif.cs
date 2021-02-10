using LazZiya.ImageResize.ColorFormats;
using LazZiya.ImageResize.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Represents an animated gif image with multiple frames
    /// </summary>
    public class AnimatedGif : IDisposable
    {
        /// <summary>
        /// Gif image as list of frames
        /// </summary>
        public IList<Image> Frames { get; set; }

        /// <summary>
        /// Set repeat count. (-1) no repeat. (0) always repeat
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// Get frames count in an animated gif
        /// </summary>
        public int FramesCount { get; }

        /// <summary>
        /// Get width and height of the image
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Get image color format
        /// </summary>
        public ImageColorFormat ImageColorFormat { get; }

        /// <summary>
        /// Get pixel format
        /// </summary>
        public PixelFormat PixelFormat { get; }

        /// <summary>
        /// Get horizontal resolution
        /// </summary>
        public float HorizontalResolution { get; }

        /// <summary>
        /// Get vertival resolution
        /// </summary>
        public float VerticalResolution { get; }

        /// <summary>
        /// Get image raw format
        /// </summary>
        public ImageFormat RawFormat { get; }

        /// <summary>
        /// Create animated gif from Image file
        /// </summary>
        /// <param name="image"></param>
        public AnimatedGif(Image image)
        {
            if (!ImageAnimator.CanAnimate(image))
                throw new BadImageFormatException("This is not an animated gif!");

            var dim = new FrameDimension(image.FrameDimensionsList[0]);
            FramesCount = image.GetFrameCount(dim);
            Size = new Size(image.Width, image.Height);
            Frames = new List<Image>();

            ImageColorFormat = ImageColorFormats.GetColorFormat((Bitmap)image);
            PixelFormat = image.PixelFormat;
            HorizontalResolution = image.HorizontalResolution;
            VerticalResolution = image.VerticalResolution;
            RawFormat = image.RawFormat;

            for (int i = 0; i < FramesCount; i++)
            {
                image.SelectActiveFrame(dim, i);
                var frame = image.Clone() as Image;
                Frames.Add(frame);
            }
        }

        /// <summary>
        /// Save animated gif
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delay">Frmae delay in milliseconds</param>
        public void SaveAs(string path, int delay = 400)
        {
            using (var fs = File.Create(path))
            {
                using (var aniGif = new GifEncoder(fs, Size.Width, Size.Height, RepeatCount))
                {
                    aniGif.FrameDelay = TimeSpan.FromMilliseconds(delay);

                    foreach (var f in Frames)
                        aniGif.AddFrame(f);
                }
            }
        }

        /// <summary>
        /// Clear frames
        /// </summary>
        public void Dispose()
        {
            Frames.Clear();
        }

        /// <summary>
        /// deconstruct
        /// </summary>
        ~AnimatedGif()
        {
            this.Dispose();
        }
    }
}
