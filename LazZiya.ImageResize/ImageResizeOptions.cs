using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Define graphic options
    /// </summary>
    public class GraphicOptions
    {
        /// <summary>
        /// Composition quality. 
        /// Default: CompositingQuality.HighQuality
        /// </summary>
        public CompositingQuality CompositingQuality { get; set; } = CompositingQuality.HighQuality;

        /// <summary>
        /// Smoothing mode. 
        /// Default: SmoothingMode.HighQuality
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; } = SmoothingMode.HighQuality;

        /// <summary>
        /// Interpolation mode.
        /// Default: InterpolationMode.HighQualityBicubic
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.HighQualityBicubic;

        /// <summary>
        /// Pixel offset mode.
        /// Default: PixelOffsetMode.HighQuality
        /// </summary>
        public PixelOffsetMode PixelOffsetMode { get; set; } = PixelOffsetMode.HighQuality;

        /// <summary>
        /// Composition mode.
        /// Default: CompositingMode.SourceOver
        /// </summary>
        public CompositingMode CompositingMode { get; set; } = CompositingMode.SourceOver;

        /// <summary>
        /// Page unit.
        /// Default: GraphicsUnit.Pixel
        /// </summary>
        public GraphicsUnit PageUnit { get; set; } = GraphicsUnit.Pixel;
    }
}
