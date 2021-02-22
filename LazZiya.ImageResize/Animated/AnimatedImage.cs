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
    /// Represents an animated image image with multiple frames
    /// </summary>
    public class AnimatedImage : IDisposable
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
        public Size Size { get; internal set; }

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
        /// private constructor
        /// </summary>
        private AnimatedImage(Image image)
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

            image.Dispose();
        }

        /// <summary>
        /// Make sure to initialize values
        /// </summary>
        public AnimatedImage(IList<Image> frames)
        {
            if (frames == null)
                throw new NullReferenceException(nameof(frames));

            var image = frames[0];

            FramesCount = frames.Count;
            Size = new Size(image.Width, image.Height);
            Frames = frames;
            ImageColorFormat = ImageColorFormats.GetColorFormat((Bitmap)image);
            PixelFormat = image.PixelFormat;
            HorizontalResolution = image.HorizontalResolution;
            VerticalResolution = image.VerticalResolution;
            RawFormat = image.RawFormat;
        }

        /// <summary>
        /// Create animated image from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static AnimatedImage FromFile(string fileName)
        {
            var img = Image.FromFile(fileName);
            return new AnimatedImage(img);
        }

        /// <summary>
        /// Create animated image from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static AnimatedImage FromStream(Stream stream)
        {
            var img = Image.FromStream(stream);
            return new AnimatedImage(img);
        }

        /// <summary>
        /// Create animated image from image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static AnimatedImage FromImage(Image image)
        {
            return new AnimatedImage(image);
        }

        /// <summary>
        /// Create animated image from bitmap
        /// </summary>
        /// <param name="hbitmap"></param>
        /// <returns></returns>
        public static AnimatedImage FromHBitmap(IntPtr hbitmap)
        {
            var img = Image.FromHbitmap(hbitmap);
            return new AnimatedImage(img);
        }

        /// <summary>
        /// Create animated image from bitmap
        /// </summary>
        /// <param name="hbitmap"></param>
        /// <param name="hpalatte"></param>
        /// <returns></returns>
        public static AnimatedImage FromHBitmap(IntPtr hbitmap, IntPtr hpalatte)
        {
            var img = Image.FromHbitmap(hbitmap, hpalatte);
            return new AnimatedImage(img);
        }

        /// <summary>
        /// Save animated gif
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delay">Frame delay in milliseconds</param>
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
        ~AnimatedImage()
        {
            this.Dispose();
        }
    }
}
